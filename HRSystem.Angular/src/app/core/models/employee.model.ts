export interface Employee {
  employeeId:       number;
  employeeCode:     string;
  fullName:         string;
  birthDate?:       string;
  qualification?:   string;
  totalVacationDays: number;
}

export interface EmployeeCreate {
  employeeCode:    string;
  fullName:        string;
  birthDate?:      string;
  qualification?:  string;
}

export interface EmployeeUpdate {
  fullName:        string;
  birthDate?:      string;
  qualification?:  string;
}

export interface PagedResult<T> {
  items:        T[];
  totalCount:   number;
  totalPages:   number;
  currentPage:  number;
  pageSize:     number;
}