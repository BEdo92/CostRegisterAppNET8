import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { AccountService } from './account.service';
import { environment } from '../../environments/environment';

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
}
