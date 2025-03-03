import { Component, inject, OnInit } from '@angular/core';
import { CostService } from '../../_services/cost.service';
import { Cost } from '../../_models/cost';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-get-cost',
  standalone: true,
  imports: [],
  templateUrl: './get-cost.component.html',
  styleUrl: './get-cost.component.css'
})
export class GetCostComponent implements OnInit  {
  costs: Cost[] = []
  toastr = inject(ToastrService)
  private costService = inject(CostService)

  ngOnInit(): void {
    this.loadCostCategories();
  }

  loadCostCategories() {
    this.costService.getCosts().subscribe({
      next: costs => {
        this.costs = costs;
        console.log(this.costs);
      },
      error: error => this.toastr.error(error)
    });
  }

  filterData() {
    
  }
}
