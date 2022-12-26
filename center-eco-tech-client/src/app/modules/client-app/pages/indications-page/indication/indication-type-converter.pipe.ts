import { Pipe, PipeTransform } from '@angular/core';
import { NameCounter } from './domain';

@Pipe({
  name: 'indicationTypeConverter'
})
export class IndicationTypeConverterPipe implements PipeTransform {

  transform(value: NameCounter): any {
    switch (value) {
      case NameCounter.Hotwater: return 'Горячая вода';
      case NameCounter.Coldwater: return 'Холодная вода';
      case NameCounter.Gas: return 'Газ';
    }
  }

}
