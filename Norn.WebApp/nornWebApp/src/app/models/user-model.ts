export class UserModel {
  public email: string = '';
  public password: string = '';
  public role: string = '';

  constructor(data?: Partial<UserModel>) {
    Object.assign(this, data);
  }
}
