import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { CostsComponent } from './costs/costs.component';
import { IncomeComponent } from './income/income.component';
import { CostplansComponent } from './costplans/costplans.component';
import { authGuard } from './_guards/auth.guard';

export const routes: Routes = [
    {path: '', component: HomeComponent}, // the home page
    {
        path: '',
         runGuardsAndResolvers: 'always',
         canActivate: [authGuard], // adding route guard
         children: [
            {path: 'costs', component: CostsComponent},
            {path: 'income', component: IncomeComponent},
            {path: 'costplans', component: CostplansComponent},
         ]
    },
    {path: '**', component: HomeComponent, pathMatch: 'full'} // if no matching route found
];
