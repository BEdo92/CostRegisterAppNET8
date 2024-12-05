import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { AddIncomeComponent } from './add-income/add-income.component';
import { GetIncomeComponent } from './get-income/get-income.component';

@Component({
  selector: 'app-income',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule, TabsModule, AddIncomeComponent, GetIncomeComponent],
  templateUrl: './income.component.html',
  styleUrl: './income.component.css'
})
export class IncomeComponent {

}
