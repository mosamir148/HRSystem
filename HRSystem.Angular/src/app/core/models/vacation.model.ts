export interface Vacation {
  vacationId:   number;
  employeeId:   number;
  vacationType: string;
  startDate:    string;
  endDate:      string;
  durationDays: number;
}

export interface VacationCreate {
  vacationType: string;
  startDate:    string;
  durationDays: number;
}

export const VACATION_TYPES = [
  'سنوية', 'مرضية', 'عارضة', 'بدون أجر', 'وضع', 'حج'
];

export const QUALIFICATIONS = [
  'دون الثانوية', 'ثانوية عامة', 'دبلوم',
  'بكالوريوس', 'ماجستير', 'دكتوراه'
];