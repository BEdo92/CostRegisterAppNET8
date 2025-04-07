import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'monthName',
  standalone: true
})
export class MonthNamePipe implements PipeTransform {
  private readonly monthNames = [
    'January', 'February', 'March', 'April', 'May', 'June',
    'July', 'August', 'September', 'October', 'November', 'December'
  ];

  transform(month: number): string {
    if (month < 1 || month > 12) {
      return 'Invalid Month';
    }
    return this.monthNames[month - 1];
  }

}
