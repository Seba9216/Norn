import { inject, Injectable } from '@angular/core';
import { NornApi } from './norn-api';
import { UserModel } from '../models/user-model';
import { firstValueFrom } from 'rxjs';
import { PromoteUserRequest } from '../models/promote-user-request';
import { BaseService } from './base-service';

@Injectable({
  providedIn: 'root',
})
export class UserService extends BaseService {
  public override apiPath: string = '/User';
  private readonly api = inject(NornApi);

  public async createUser(user: UserModel) {
    return firstValueFrom(this.api.post<boolean, UserModel>(this.apiPath, user));
  }

  public async loginUser(user: UserModel) {
    return firstValueFrom(this.api.post<any, UserModel>(this.apiPath + '/Login', user));
  }

  public async getAllUsers() {
    return firstValueFrom(this.api.get<UserModel[]>(this.apiPath));
  }
  public async promoteUser(user: PromoteUserRequest) {
    return firstValueFrom(this.api.put<UserModel, PromoteUserRequest>(this.apiPath, user));
  }
  public async deleteUser(email: string) {
    return firstValueFrom(this.api.delete(this.apiPath + '/' + email));
  }
  public async getUserIdFromEmail(email: string) {
    return firstValueFrom(this.api.get<number>(this.apiPath + '/' + email));
  }
}
