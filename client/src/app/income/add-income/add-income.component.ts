import { Component, inject, OnInit } from '@angular/core';
import { AccountService } from '../../_services/account.service';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CostService } from '../../_services/cost.service';
import { ToastrService } from 'ngx-toastr';
import { TextInputComponent } from '../../_forms/text-input/text-input.component';
import { DatePickerComponent } from '../../_forms/date-picker/date-picker.component';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-add-income',
  standalone: true,
  imports: [ReactiveFormsModule, TextInputComponent, DatePickerComponent, CommonModule],
  templateUrl: './add-income.component.html',
  styleUrl: './add-income.component.css'
})
export class AddIncomeComponent implements OnInit {
  accountService = inject(AccountService);
  categories: string[] = [];
  private fb = inject(FormBuilder);
  private costService = inject(CostService);
  incomeForm: FormGroup = new FormGroup({});
  validationErrors: string[] | undefined;
  maxDate = new Date();
  toastr = inject(ToastrService);

  ngOnInit(): void {
    this.loadIncomeCategories();
  }

  initializeForm() {
    this.incomeForm = this.fb.group({
      date: ['', Validators.required],
      category: [this.categories, Validators.required],
      total: ['', [Validators.required, Validators.pattern('^[0-9]*$')]],
      comment: ['']
    });
  }

  saveData() {
    console.log(this.incomeForm.value);
    this.costService.saveIncome(this.incomeForm.value).subscribe({
      next: response => { 
        console.log(response);
      },
      error: error => {
        console.log(error);
        this.validationErrors = error
      }
    });
    this.toastr.success('Saved successfully');
  }

  loadIncomeCategories() {
    this.costService.getIncomeCategories().subscribe({
      next: categories => {
        this.categories = categories;
        console.log(this.categories);
        this.initializeForm();
      },
      error: error => this.validationErrors = error
    });
  }
}
