export enum RequestStatus {
  New = 1,
  Accepted = 2,
  InProgress = 3,
  Done = 4
}

export class ClientRequestsDto {
  date: string;
  theme: string;
  message: string;
  cLientName: string;
  cLientAdress: string;
  status: RequestStatus;
}
