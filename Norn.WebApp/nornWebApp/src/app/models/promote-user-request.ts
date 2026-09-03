export class PromoteUserRequest {
  public email: string = '';
  public role: string = '';

    constructor(data?: Partial<PromoteUserRequest>) {
    Object.assign(this, data);
  }
}
