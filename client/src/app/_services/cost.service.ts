import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { Cost } from '../_models/cost';
import { CostPlan } from '../_models/costplan';

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

  getIncome() {
    return this.http.get<Cost[]>(this.baseUrl + 'income/all');
  }

  getCostplans() {
    return this.http.get<CostPlan[]>(this.baseUrl + 'costplan/all');
  }

  getBalance() {
    return this.http.get<number>(this.baseUrl + 'balance');
  }

  getBalanceWithCosplans() {
    return this.http.get<number>(this.baseUrl + 'balance/withplan');
  }

  deleteCosts(ids: number[]) {
    return this.http.request('delete', this.baseUrl + 'cost', { body: ids });
  }

  deleteIncome(ids: number[]) {
    return this.http.request('delete', this.baseUrl + 'income', { body: ids });
  }

  deleteCostplans(ids: number[]) {
    return this.http.request('delete', this.baseUrl + 'costplan', { body: ids });
  }
}
