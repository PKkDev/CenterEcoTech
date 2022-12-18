import { Pipe, PipeTransform } from '@angular/core';
import { RequestStatus } from './domain';

@Pipe({
  name: 'applictionStatusConverter'
})
export class ApplictionStatusConverterPipe implements PipeTransform {

  transform(value: RequestStatus): any {
    switch (value) {
      case RequestStatus.Accepted: return 'Принято';
      case RequestStatus.Done: return 'Готово';
      case RequestStatus.InProgress: return 'В процессе';
      case RequestStatus.New: return 'Новая';
    }
  }

}
