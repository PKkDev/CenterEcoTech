import { Pipe, PipeTransform } from '@angular/core';
import { NameCounter } from './domain';

@Pipe({
  name: 'indicationsTypeConverter'
})
export class IndicationsTypeConverterPipe implements PipeTransform {

  transform(value: NameCounter): any {
    switch (value) {
      case NameCounter.Hotwater: return 'Горячая вода';
      case NameCounter.Coldwater: return 'Холодная вода';
      case NameCounter.Gas: return 'Газ';
    }
  }

}
