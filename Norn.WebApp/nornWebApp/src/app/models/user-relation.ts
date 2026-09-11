export class UserRelation {
  public id: number = 0;
  public email: string = '';
  constructor(data?: Partial<UserRelation>) {
    Object.assign(this, data);
  }
}
