import { Component, inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { CostService } from '../../_services/cost.service';
import { TextInputComponent } from '../../_forms/text-input/text-input.component';
import { DatePickerComponent } from '../../_forms/date-picker/date-picker.component';
import { AccountService } from '../../_services/account.service';
import { ToastrService } from 'ngx-toastr';


@Component({
  selector: 'app-add-cost',
  standalone: true,
  imports: [ReactiveFormsModule, TextInputComponent, DatePickerComponent, CommonModule],
  templateUrl: './add-cost.component.html',
  styleUrl: './add-cost.component.css'
})
export class AddCostComponent implements OnInit {
  accountService = inject(AccountService);
  categories: string[] = [];
  private fb = inject(FormBuilder);
  private costService = inject(CostService);
  costForm: FormGroup = new FormGroup({});
  validationErrors: string[] | undefined;
  maxDate = new Date();
  toastr = inject(ToastrService);

  ngOnInit(): void {
    this.loadCostCategories();
  }

  initializeForm() {
    this.costForm = this.fb.group({
      date: ['', Validators.required],
      category: [this.categories, Validators.required],
      total: ['', [Validators.required, Validators.pattern('^[0-9]*$')]],
      comment: ['']
    });
  }

  saveData() {
    console.log(this.costForm.value);
    this.costService.saveCost(this.costForm.value).subscribe({
      next: response => { 
        console.log(response);
        this.toastr.success('Saved successfully');
      },
      error: error => {
        console.log(error);
        this.validationErrors = error
      }
    });
  }

  loadCostCategories() {
    this.costService.getCostCategories().subscribe({
      next: categories => {
        this.categories = categories;
        console.log(this.categories);
        this.initializeForm();
      },
      error: error => this.validationErrors = error
    });
  }
}
