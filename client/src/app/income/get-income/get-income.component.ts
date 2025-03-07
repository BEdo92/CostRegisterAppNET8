import { LiveAnnouncer } from '@angular/cdk/a11y';
import { AfterViewInit, Component, inject, OnInit, ViewChild } from '@angular/core';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { Cost } from '../../_models/cost';
import { CostService } from '../../_services/cost.service';
import { MatSort, MatSortModule, Sort } from '@angular/material/sort';
import { SelectionModel } from '@angular/cdk/collections';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { FormsModule } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';


@Component({
  selector: 'app-get-income',
  standalone: true,
  imports: [MatTableModule, MatPaginatorModule, MatSortModule, MatCheckboxModule, FormsModule],
  templateUrl: './get-income.component.html',
  styleUrl: './get-income.component.css'
})
export class GetIncomeComponent {
  displayedColumns: string[] = ['select', "date", "category", "total", "comment"];
  private costService = inject(CostService)
  private liveAnnouncer = inject(LiveAnnouncer);
  elementData: Cost[] | undefined;
  dataSource = new MatTableDataSource<Cost>;
  selection: SelectionModel<Cost>;
  toastr = inject(ToastrService);

  constructor() {
    const initialSelection: Cost[] | undefined = [];
    const allowMultiSelect = true;
    this.selection = new SelectionModel<Cost>(allowMultiSelect, initialSelection);
  }

  ngOnInit(): void {
    this.loadIncome();
  }

  @ViewChild(MatPaginator)
  paginator!: MatPaginator;
  @ViewChild(MatSort)
  sort!: MatSort;

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  loadIncome() {
    this.costService.getIncome().subscribe({
      next: income => {
        this.elementData = income;
        this.dataSource.data = this.elementData; 
        console.log(this.elementData);
      },
      error: error => console.log(error)
    });
  }

  announceSortChange(sortState: Sort) {
    if (sortState.direction) {
      this.liveAnnouncer.announce(`Sorted ${sortState.direction}ending`);
    } else {
      this.liveAnnouncer.announce('Sorting cleared');
    }
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
    this.costService.deleteIncome(idsToDelete).subscribe({
      next: () => {
      this.loadIncome();
      this.selection.clear();
      this.toastr.success('Data deleted successfully');
      },
      error: error => console.log(error)
    });
  }
}
