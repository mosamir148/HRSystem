import { Component, Input, Output, EventEmitter, OnChanges } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Employee, EmployeeCreate, EmployeeUpdate } from '../../../../core/models/employee.model';
import { QUALIFICATIONS } from '../../../../core/models/vacation.model';
import { EmployeeService } from '../../../../core/services/employee.service';

@Component({
  selector: 'app-employee-form',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './employee-form.component.html',
  styleUrls: ['./employee-form.component.scss']
})
export class EmployeeFormComponent implements OnChanges {

  @Input()  selectedEmployee: Employee | null = null;
  @Output() employeeSaved  = new EventEmitter<void>();
  @Output() openVacations  = new EventEmitter<Employee>();

  qualifications = QUALIFICATIONS;

  empCode  = '';
  empName  = '';
  empBirth = '';
  empQual  = '';

  alertMsg  = '';
  alertType = 'danger';

  constructor(private employeeService: EmployeeService) {}

  ngOnChanges() {
    if (this.selectedEmployee) {
      this.empCode  = this.selectedEmployee.employeeCode;
      this.empName  = this.selectedEmployee.fullName;
      this.empBirth = this.selectedEmployee.birthDate ?? '';
      this.empQual  = this.selectedEmployee.qualification ?? '';
    }
  }

  save() {
    if (!this.empCode || !this.empName)
      return this.showAlert('الرجاء إدخال رقم الموظف والاسم');

    const dto: EmployeeCreate = {
      employeeCode : this.empCode,
      fullName     : this.empName,
      birthDate    : this.empBirth || undefined,
      qualification: this.empQual  || undefined
    };

    this.employeeService.create(dto).subscribe({
      next: () => {
        this.showAlert('تم الحفظ بنجاح ✔', 'success');
        this.clear();
        this.employeeSaved.emit();
      },
      error: (e) => this.showAlert(e.error?.message ?? 'خطأ في الحفظ')
    });
  }

  update() {
    if (!this.selectedEmployee)
      return this.showAlert('الرجاء تحديد موظف من الجدول');

    const dto: EmployeeUpdate = {
      fullName     : this.empName,
      birthDate    : this.empBirth || undefined,
      qualification: this.empQual  || undefined
    };

    this.employeeService.update(this.selectedEmployee.employeeId, dto).subscribe({
      next: () => {
        this.showAlert('تم التعديل بنجاح ✔', 'success');
        this.employeeSaved.emit();
      },
      error: (e) => this.showAlert(e.error?.message ?? 'خطأ في التعديل')
    });
  }

  delete() {
    if (!this.selectedEmployee)
      return this.showAlert('الرجاء تحديد موظف من الجدول');

    if (!confirm(`حذف "${this.selectedEmployee.fullName}"؟`)) return;

    this.employeeService.delete(this.selectedEmployee.employeeId).subscribe({
      next: () => {
        this.clear();
        this.employeeSaved.emit();
      },
      error: () => this.showAlert('خطأ في الحذف')
    });
  }

  openVacationsModal() {
    if (!this.selectedEmployee)
      return this.showAlert('الرجاء تحديد موظف من الجدول');
    this.openVacations.emit(this.selectedEmployee);
  }

  clear() {
    this.empCode = this.empName = this.empBirth = this.empQual = '';
    this.selectedEmployee = null;
  }

  showAlert(msg: string, type = 'danger') {
    this.alertMsg  = msg;
    this.alertType = type;
    setTimeout(() => this.alertMsg = '', 4000);
  }
}