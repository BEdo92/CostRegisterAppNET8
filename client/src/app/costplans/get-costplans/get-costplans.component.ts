import { AfterViewInit, Component, inject, OnInit, ViewChild } from '@angular/core';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { CostService } from '../../_services/cost.service';
import { MatSort, MatSortModule, Sort } from '@angular/material/sort';
import { SelectionModel } from '@angular/cdk/collections';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { FormsModule } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { CostPlan } from '../../_models/costplan';

@Component({
  selector: 'app-get-costplans',
  standalone: true,
  imports: [MatTableModule, MatPaginatorModule, MatSortModule, MatCheckboxModule, FormsModule],
  templateUrl: './get-costplans.component.html',
  styleUrl: './get-costplans.component.css'
})
export class GetCostplansComponent {
    displayedColumns: string[] = ['select', "category", "total", "comment"];
    private costService = inject(CostService)
    elementData: CostPlan[] | undefined;
    dataSource = new MatTableDataSource<CostPlan>;
    selection: SelectionModel<CostPlan>;
    toastr = inject(ToastrService);
  
    constructor() {
      const initialSelection: CostPlan[] | undefined = [];
      const allowMultiSelect = true;
      this.selection = new SelectionModel<CostPlan>(allowMultiSelect, initialSelection);
    }
  
    ngOnInit(): void {
      this.loadCosts();
    }
  
    @ViewChild(MatPaginator)
    paginator!: MatPaginator;
    @ViewChild(MatSort)
    sort!: MatSort;
  
    ngAfterViewInit() {
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
    }
  
    loadCosts() {
      this.costService.getCostplans().subscribe({
        next: cp => {
          this.elementData = cp;
          this.dataSource.data = this.elementData; 
          console.log(this.elementData);
        },
        error: error => console.log(error)
      });
    }
  
    isAllSelected() {
      const numSelected = this.selection.selected.length;
      const numRows = this.dataSource.data.length;
      return numSelected == numRows;
    }
  
    toggleAllRows() {
      this.isAllSelected() ?
        this.selection.clear() :
        this.dataSource.data.forEach(row => this.selection.select(row));
    }
  
  deleteSelected() {
    const idsToDelete = this.selection.selected.map(cost => cost.id);
    this.costService.deleteCostplans(idsToDelete).subscribe({
      next: () => {
      this.loadCosts();
      this.selection.clear();
      this.toastr.success('Data deleted successfully');
      },
      error: error => console.log(error)
    });
  }
}
