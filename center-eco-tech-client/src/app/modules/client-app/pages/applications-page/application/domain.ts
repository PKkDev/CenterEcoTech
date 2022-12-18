export class UserDetailDto {
    phone: string;
    email: string;
    firstName: string;
    lastNme: string;
    midName: string;
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
