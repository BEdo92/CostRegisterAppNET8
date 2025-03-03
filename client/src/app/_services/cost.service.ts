import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { AccountService } from './account.service';
import { environment } from '../../environments/environment';
import { Cost } from '../_models/cost';

@Injectable({
  providedIn: 'root'
})
export class CostService {
  private http = inject(HttpClient);
  private accountService = inject(AccountService);
  baseUrl = environment.apiUrl;

  getCostCategories() {
    return this.http.get<string[]>(this.baseUrl + 'costcategory');
  }

  getIncomeCategories() {
    return this.http.get<string[]>(this.baseUrl + 'incomecategory');
  }

  saveCost(model: any) {
    return this.http.post(this.baseUrl + 'cost', model);
  }

  saveIncome(model: any) {
    return this.http.post(this.baseUrl + 'income', model);
  }

  saveCostPlan(model: any) {
    return this.http.post(this.baseUrl + 'costplan', model);
  }

  getCosts() {
    return this.http.get<Cost[]>(this.baseUrl + 'cost/all');
  }
}
