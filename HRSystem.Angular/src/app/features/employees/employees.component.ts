import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Employee } from '../../core/models/employee.model';
import { EmployeeService } from '../../core/services/employee.service';
import { EmployeeFormComponent } from './components/employee-form/employee-form.component';
import { EmployeeGridComponent } from './components/employee-grid/employee-grid.component';
import { VacationModalComponent } from './components/vacation-modal/vacation-modal.component';

@Component({
  selector: 'app-employees',
  standalone: true,
  imports: [
    CommonModule,
    EmployeeFormComponent,
    EmployeeGridComponent,
    VacationModalComponent
  ],
  templateUrl: './employees.component.html',
  styleUrls: ['./employees.component.scss']
})
export class EmployeesComponent implements OnInit {

  employees:   Employee[] = [];
  totalCount   = 0;
  totalPages   = 0;
  currentPage  = 1;
  readonly PAGE_SIZE = 5;

  selectedEmployee: Employee | null = null;
  showVacationModal = false;

  constructor(private employeeService: EmployeeService) {}

  ngOnInit() {
    this.loadEmployees();
  }

  loadEmployees() {
    this.employeeService.getAll(this.currentPage, this.PAGE_SIZE)
      .subscribe(res => {
        this.employees  = res.items;
        this.totalCount = res.totalCount;
        this.totalPages = res.totalPages;
      });
  }

  onPageChange(page: number) {
    this.currentPage = page;
    this.loadEmployees();
  }

  onEmployeeSelected(emp: Employee) {
    this.selectedEmployee = emp;
  }

  onEmployeeSaved() {
    this.loadEmployees();
  }

  onOpenVacations(emp: Employee) {
    this.selectedEmployee = emp;
    this.showVacationModal = true;
  }

  onVacationModalClosed() {
    this.showVacationModal = false;
    this.loadEmployees();
  }
}