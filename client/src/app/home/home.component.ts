import { Component, inject, OnInit } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { RegisterComponent } from "../register/register.component";
import { CommonModule, TitleCasePipe } from '@angular/common';
import { CostService } from '../_services/cost.service';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule, RegisterComponent, TitleCasePipe],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit {
  accountService = inject(AccountService);
  costService = inject(CostService);
  registerMode = false;
  balanceData: any;

  registerToggle() {
    this.registerMode = !this.registerMode;
  }

  cancelRegisterMode(event: boolean) {
    this.registerMode = event;
  }

  ngOnInit(): void {
    this.getBalance();
  }

  getBalance() {
    this.costService.getBalance().subscribe({
      next: balance => {
        this.balanceData = balance;
      },
      error: error => console.log(error)
    });
  }
}
