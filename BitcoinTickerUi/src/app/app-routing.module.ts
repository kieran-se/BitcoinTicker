import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { TickerComponent } from './components/ticker/ticker.component';


const routes: Routes = [
  { path: '', component: TickerComponent, pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
