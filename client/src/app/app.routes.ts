import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { CostsComponent } from './costs/costs.component';
import { IncomeComponent } from './income/income.component';
import { CostplansComponent } from './costplans/costplans.component';
import { authGuard } from './_guards/auth.guard';
import { UserEditComponent } from './user-edit/user-edit.component';
import { preventUnsavedChangesGuard } from './_guards/prevent-unsaved-changes.guard';
import { NotFoundComponent } from './errors/not-found/not-found.component';
import { ServerErrorComponent } from './errors/server-error/server-error.component';

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
            {path: 'useredit', component: UserEditComponent, canDeactivate: [preventUnsavedChangesGuard]},
         ]
    },
    {path: 'not-found', component: NotFoundComponent, pathMatch: 'full'},
    {path: 'server-error', component: ServerErrorComponent, pathMatch: 'full'},
    {path: '**', component: HomeComponent, pathMatch: 'full'} // if no matching route found
];
