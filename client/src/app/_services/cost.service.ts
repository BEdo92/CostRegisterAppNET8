import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { environment } from '../../environments/environment';
import { Cost } from '../_models/cost';

@Injectable({
  providedIn: 'root'
})
export class CostService {
  private http = inject(HttpClient);
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

  getBalance() {
    return this.http.get<number>(this.baseUrl + 'balance');
  }
}
