export class UserDetailDto {
    phone: string;
    email: string;
    firstName: string;
    lastNme: string;
    midName: string;
    adress: UserDetailAdressDto;
}

export class UserDetailAdressDto {
    city: string;
    street: string;
    house: string;
    corpus: string | null;
    room: string;
}