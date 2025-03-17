import { Component, inject, OnInit } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { RegisterComponent } from "../register/register.component";
import { TitleCasePipe } from '@angular/common';
import { CostService } from '../_services/cost.service';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [RegisterComponent, TitleCasePipe],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit {
  accountService = inject(AccountService);
  costService = inject(CostService);
  registerMode = false;
  balance : number = 0;
  balanceWithCosplans : number = 0;

  registerToggle() {
    this.registerMode = !this.registerMode;
  }

  cancelRegisterMode(event: boolean) {
    this.registerMode = event;
  }

  ngOnInit(): void {
    this.getBalance();
    this.getBalanceWithCostplans();
  }

  getBalance() {
    this.costService.getBalance().subscribe({
      next: balance => {
        this.balance = balance;
      },
      error: error => console.log(error)
    });
  }

  getBalanceWithCostplans() {
    this.costService.getBalanceWithCosplans().subscribe({
      next: balance => {
        this.balanceWithCosplans = balance;
      },
      error: error => console.log(error)
    });
  }
}
