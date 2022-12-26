export class UserDetailDto {
    firstName: string;
    lastNme: string;
    midName: string;
    adress: UserDetailAdressDto;
    myDate: 'yyyy-mm-dd'
}

export class UserDetailAdressDto {
    city: string;
    street: string;
    house: string;
    corpus: string | null;
    room: string;
}

export class newRequestQuery {
  date: string;
  pastvalue: string;
  currentvalue: string;
  cLientName: string;
  cLientAdress: string;
  Name: NameCounter;
}

export enum NameCounter {
  Hotwater = 1,
  Coldwater = 2,
  Gas = 3
}
