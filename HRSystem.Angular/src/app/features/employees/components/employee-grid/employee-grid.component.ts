import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Employee } from '../../../../core/models/employee.model';

@Component({
  selector: 'app-employee-grid',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './employee-grid.component.html',
  styleUrls: ['./employee-grid.component.scss']
})
export class EmployeeGridComponent {

  @Input() employees:   Employee[] = [];
  @Input() totalPages   = 0;
  @Input() currentPage  = 1;

  @Output() employeeSelected = new EventEmitter<Employee>();
  @Output() pageChanged      = new EventEmitter<number>();
  @Output() openVacations    = new EventEmitter<Employee>();

  selectedId: number | null = null;

  selectEmployee(emp: Employee) {
    this.selectedId = emp.employeeId;
    this.employeeSelected.emit(emp);
  }

  goPage(page: number) {
    if (page < 1 || page > this.totalPages) return;
    this.pageChanged.emit(page);
  }

  get pages(): number[] {
    return Array.from({ length: this.totalPages }, (_, i) => i + 1);
  }
}