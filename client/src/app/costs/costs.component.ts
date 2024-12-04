import { Component } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { AddCostComponent } from "./add-cost/add-cost.component";
import { GetCostComponent } from "./get-cost/get-cost.component"; 


@Component({
  selector: 'app-costs',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule, TabsModule, AddCostComponent, GetCostComponent],
  templateUrl: './costs.component.html',
  styleUrl: './costs.component.css'
})
export class CostsComponent  {
  
}
