import { Component, inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { CostService } from '../../_services/cost.service';
import { TextInputComponent } from '../../_forms/text-input/text-input.component';
import { DatePickerComponent } from '../../_forms/date-picker/date-picker.component';
import { AccountService } from '../../_services/account.service';
import { ToastrService } from 'ngx-toastr';
import { CostPlan } from '../../_models/costplan';


@Component({
  selector: 'app-add-cost',
  standalone: true,
  imports: [ReactiveFormsModule, FormsModule, TextInputComponent, DatePickerComponent, CommonModule],
  templateUrl: './add-cost.component.html',
  styleUrl: './add-cost.component.css'
})
export class AddCostComponent implements OnInit {
  accountService = inject(AccountService);
  categories: string[] = [];
  costPlans: CostPlan[] = [];
  selectedCostPlan: any;
  private fb = inject(FormBuilder);
  private costService = inject(CostService);
  costForm: FormGroup = new FormGroup({});
  validationErrors: string[] | undefined;
  maxDate = new Date();
  hasCostPlan = false;
  toastr = inject(ToastrService);

  ngOnInit(): void {
    this.loadData();
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
        if (this.selectedCostPlan) {
          console.log(this.selectedCostPlan.id)
            this.costService.deleteCostplans([this.selectedCostPlan.id]).subscribe({
            next: () => {
              this.toastr.success('Cost plan deleted successfully');
              this.loadData();
            },
            error: () => this.toastr.error('Failed to delete cost plan')
            });
        }
      },
      error: error => {
        console.log(error);
        this.validationErrors = error
      }
    });
  }

  loadData() {
    this.costService.getCostplans().subscribe({
      next: costPlans => {
        this.costPlans = costPlans;
        console.log(this.costPlans);
        this.hasCostPlan = this.costPlans.length > 0;
      },
      error: error => this.toastr.error(error)
    });
    this.costService.getCostCategories().subscribe({
      next: categories => {
        this.categories = categories;
        console.log(this.categories);
        this.initializeForm();
      },
      error: error => this.validationErrors = error
    });
  }

  onCostPlanChange($event: any) {
    const selectedValue = $event.target.value;
    const selectedCostPlan = this.costPlans.find(plan => plan.id === +selectedValue);
    console.log('Selected Cost Plan:', selectedCostPlan);
    this.selectedCostPlan = selectedCostPlan;
    if (selectedCostPlan) {
        this.costForm.patchValue({
            date: '',
            category: selectedCostPlan.category,
            total: selectedCostPlan.total,
            comment: selectedCostPlan.comment
        });
    }
  }
}
