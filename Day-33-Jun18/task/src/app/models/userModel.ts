

export class UserModel {
    constructor(public firstName: string = '', public lastName: string = '', public gender: string = '', public role: string = '', public address: Address = new Address()) {

    }
}

export class Address{
    constructor(public state: string = ''){}
}