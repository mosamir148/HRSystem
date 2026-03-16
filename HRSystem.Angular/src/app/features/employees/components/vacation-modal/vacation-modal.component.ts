import { Component, Input, Output, EventEmitter, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Employee } from '../../../../core/models/employee.model';
import { Vacation, VacationCreate, VACATION_TYPES } from '../../../../core/models/vacation.model';
import { VacationService } from '../../../../core/services/vacation.service';

@Component({
  selector: 'app-vacation-modal',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './vacation-modal.component.html',
  styleUrls: ['./vacation-modal.component.scss']
})
export class VacationModalComponent implements OnInit {

  @Input()  employee!: Employee;
  @Output() closed = new EventEmitter<void>();

  vacations:    Vacation[] = [];
  vacationTypes = VACATION_TYPES;

  newType  = '';
  newStart = '';
  newDays  = 1;

  alertMsg  = '';
  alertType = 'danger';

  constructor(private vacationService: VacationService) {}

  ngOnInit() {
    this.loadVacations();
  }

  loadVacations() {
    this.vacationService.getByEmployee(this.employee.employeeId)
      .subscribe({
        next: (v) => this.vacations = v,
        error: () => this.showAlert('خطأ في تحميل الإجازات')
      });
  }

  addVacation() {
    if (!this.newType)       return this.showAlert('الرجاء اختيار نوع الإجازة');
    if (!this.newStart)      return this.showAlert('الرجاء تحديد تاريخ البداية');
    if (this.newDays < 1)    return this.showAlert('المدة لا تقل عن يوم');
    if (this.newDays > 30)   return this.showAlert('المدة لا تتجاوز 30 يوماً');

    const dto: VacationCreate = {
      vacationType: this.newType,
      startDate:    this.newStart,
      durationDays: this.newDays
    };

    this.vacationService.create(this.employee.employeeId, dto).subscribe({
      next: () => {
        this.showAlert('تم الحفظ بنجاح ✔', 'success');
        this.newType = this.newStart = '';
        this.newDays = 1;
        this.loadVacations();
      },
      error: (e) => this.showAlert(e.error?.message ?? 'خطأ في الحفظ')
    });
  }

  deleteVacation(vacationId: number) {
    if (!confirm('حذف هذه الإجازة؟')) return;
    this.vacationService.delete(this.employee.employeeId, vacationId).subscribe({
      next: () => this.loadVacations(),
      error: () => this.showAlert('خطأ في الحذف')
    });
  }

  get totalDays(): number {
    return this.vacations.reduce((s, v) => s + v.durationDays, 0);
  }

  close() {
    this.closed.emit();
  }

  showAlert(msg: string, type = 'danger') {
    this.alertMsg  = msg;
    this.alertType = type;
    setTimeout(() => this.alertMsg = '', 4000);
  }
}