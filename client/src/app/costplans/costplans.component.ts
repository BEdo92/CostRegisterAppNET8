import { Component } from '@angular/core';
import { AddCostplansComponent } from "./add-costplans/add-costplans.component";
import { GetCostplansComponent } from './get-costplans/get-costplans.component';
import { CommonModule } from '@angular/common';
import { TabsModule } from 'ngx-bootstrap/tabs';

@Component({
  selector: 'app-costplans',
  standalone: true,
  imports: [CommonModule, TabsModule, AddCostplansComponent, GetCostplansComponent],
  templateUrl: './costplans.component.html',
  styleUrl: './costplans.component.css'
})
export class CostplansComponent {

}
