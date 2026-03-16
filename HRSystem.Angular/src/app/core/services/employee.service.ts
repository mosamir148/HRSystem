import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Employee, EmployeeCreate, EmployeeUpdate, PagedResult } from '../models/employee.model';

@Injectable({ providedIn: 'root' })
export class EmployeeService {
private readonly apiUrl = 'https://localhost:7091/api/employees';

  constructor(private http: HttpClient) {}

  getAll(page = 1, pageSize = 5): Observable<PagedResult<Employee>> {
    const params = new HttpParams()
      .set('page', page)
      .set('pageSize', pageSize);
    return this.http.get<PagedResult<Employee>>(this.apiUrl, { params });
  }

  getById(id: number): Observable<Employee> {
    return this.http.get<Employee>(`${this.apiUrl}/${id}`);
  }

  create(dto: EmployeeCreate): Observable<Employee> {
    return this.http.post<Employee>(this.apiUrl, dto);
  }

  update(id: number, dto: EmployeeUpdate): Observable<Employee> {
    return this.http.put<Employee>(`${this.apiUrl}/${id}`, dto);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}