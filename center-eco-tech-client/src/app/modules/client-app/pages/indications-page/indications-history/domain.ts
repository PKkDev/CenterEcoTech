export enum NameCounter {
  Hotwater = 1,
  Coldwater = 2,
  Gas = 3
}

export class MeasurementRequestDto {
  date: string;
  pastvalue: string;
  currentvalue: string;
  cLientName: string;
  cLientAdress: string;
  Name: NameCounter;
}
