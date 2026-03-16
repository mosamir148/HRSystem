import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Vacation, VacationCreate } from '../models/vacation.model';

@Injectable({ providedIn: 'root' })
export class VacationService {
private readonly apiUrl = 'https://localhost:7091/api/employees';

  constructor(private http: HttpClient) {}

  getByEmployee(employeeId: number): Observable<Vacation[]> {
    return this.http.get<Vacation[]>(`${this.apiUrl}/${employeeId}/vacations`);
  }

  create(employeeId: number, dto: VacationCreate): Observable<Vacation> {
    return this.http.post<Vacation>(`${this.apiUrl}/${employeeId}/vacations`, dto);
  }

  delete(employeeId: number, vacationId: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${employeeId}/vacations/${vacationId}`);
  }
}