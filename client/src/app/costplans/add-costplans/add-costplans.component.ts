import { Component, inject, OnInit } from '@angular/core';
import { AccountService } from '../../_services/account.service';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CostService } from '../../_services/cost.service';
import { ToastrService } from 'ngx-toastr';
import { TextInputComponent } from "../../_forms/text-input/text-input.component";
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-add-costplans',
  standalone: true,
  imports: [ReactiveFormsModule, TextInputComponent, CommonModule],
  templateUrl: './add-costplans.component.html',
  styleUrl: './add-costplans.component.css'
})
export class AddCostplansComponent implements OnInit {
  accountService = inject(AccountService);
  categories: string[] = [];
  private fb = inject(FormBuilder);
  private costService = inject(CostService);
  costForm: FormGroup = new FormGroup({});
  validationErrors: string[] | undefined;
  toastr = inject(ToastrService);

  ngOnInit(): void {
    this.loadCostCategories();
  }

  initializeForm() {
    this.costForm = this.fb.group({
      category: [this.categories, Validators.required],
      total: ['', [Validators.required, Validators.pattern('^[0-9]*$')]],
      comment: ['']
    });
  }

  saveData() {
    console.log(this.costForm.value);
    this.costService.saveCostPlan(this.costForm.value).subscribe({
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
