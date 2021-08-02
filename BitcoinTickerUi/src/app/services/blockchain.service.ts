import { HttpClient, HttpResponse } from '@angular/common/http';
import { Injectable } from "@angular/core";
import { Observable, Subject, timer } from 'rxjs';
import { switchMap, tap, share, retry, takeUntil, map, toArray } from 'rxjs/operators';
import { ICurrencyDetail } from '../interfaces/currency-detail.interface';

@Injectable()
export class BlockChainService {
  constructor(private httpClient: HttpClient) {

    this.allCurrencyInfo$ = timer(1, 3000).pipe(
      switchMap(() => this.httpClient.get<ICurrencyDetail>('https://localhost:44325/v1/Bitcoin/info')),
      retry(),
      share(),
      takeUntil(this.stopPolling)
    );

  }

  private allCurrencyInfo$: Observable<ICurrencyDetail>;
  private stopPolling = new Subject();

  getAllCurrencyInfo(): Observable<ICurrencyDetail> {
    return this.allCurrencyInfo$;
  }

  convert(body: Object): Observable<number> {
    return this.httpClient.post<number>('https://localhost:44325/v1/Bitcoin/convert', body);
  }

  ngOnDestroy() {
    this.stopPolling.next();
  }
}
