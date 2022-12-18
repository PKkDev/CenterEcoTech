export class UserDetailDto {
    Date: string;
    Status: RequestStatus;
    Theme: string;
    FIO: CLientName;
    adress: UserDetailAdressDto;
    theme: string;
    message: string;
    myDate: 'yyyy-mm-dd'
}

export class UserDetailAdressDto {
    city: string;
    street: string;
    house: string;
    corpus: string | null;
    room: string;
}

export class CLientName {
  MidName: string;
  FirstName: string;
  LastNme: string;
}

export enum RequestStatus {
  New = 1,
  Accepted = 2,
  InProgress = 3,
  Done = 4
}
