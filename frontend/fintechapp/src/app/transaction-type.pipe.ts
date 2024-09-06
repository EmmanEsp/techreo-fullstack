import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'transactionType',
  standalone: true 
})
export class TransactionTypePipe implements PipeTransform {

  transform(value: string): string {
    switch (value.toLowerCase()) {
      case 'deposit':
        return 'Deposito';
      case 'withdraw':
        return 'Retiro';
      default:
        return value;
    }
  }
}
