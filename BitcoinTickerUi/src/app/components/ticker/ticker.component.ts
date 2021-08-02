import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Observable, of } from 'rxjs';
import { flatMap, map, mergeMap } from 'rxjs/operators';
import { ICurrencyDetail } from '../../interfaces/currency-detail.interface';
import { BlockChainService } from '../../services/blockchain.service';

@Component({
  selector: 'app-ticker',
  templateUrl: './ticker.component.html',
  styleUrls: ['./ticker.component.css']
})
export class TickerComponent implements OnInit {

  allCurrencyInfo$: Observable<ICurrencyDetail>;
  investmentForm: FormGroup;
  investmentValue: number = 1000;
  currencies: any;
  investment: number = 0;

  constructor(
    private blockChainService: BlockChainService,
    private formBuilder: FormBuilder
  ) {
  }

  ngOnInit(): void {

    this.investmentForm = this.formBuilder.group({
      currencies: ['',[ Validators.required]],
      investmentValue: ['', [Validators.required, Validators.max(999999), Validators.min(1)]]
    });

    this.allCurrencyInfo$ = this.blockChainService.getAllCurrencyInfo();

    this.allCurrencyInfo$.pipe(
    ).subscribe((data) => {
      this.currencies = data;
    });

    this.investmentForm.setValue({
      currencies: 'GBP',
      investmentValue: 1000
    });
  }

  get f() {
    return this.investmentForm.controls;
  }

  submit() {

    if (this.investmentForm.invalid) {
      return;
    }

    let data = {
      currency: this.f.currencies.value,
      value: this.f.investmentValue.value
    }

    this.blockChainService.convert(data).pipe(
    ).subscribe((data) => {
      this.investment = data;
    });
  }

}
