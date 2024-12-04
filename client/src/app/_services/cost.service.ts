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

  getCategories() {
    return this.http.get<string[]>(this.baseUrl + 'costcategory');
  }

  saveCost(model: any) {
    return this.http.post(this.baseUrl + 'cost', model);
  }
}
