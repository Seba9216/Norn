import { inject, Injectable } from '@angular/core';
import { NornApi } from './norn-api';
import { UserModel } from '../models/user-model';
import { firstValueFrom } from 'rxjs';
import { PromoteUserRequest } from '../models/promote-user-request';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  private readonly api = inject(NornApi);

  public async createUser(user: UserModel) {
    return firstValueFrom(this.api.post<boolean, UserModel>('/User', user));
  }

  public async loginUser(user: UserModel) {
    return firstValueFrom(this.api.post<any, UserModel>('/User/Login', user));
  }

  public async getAllUsers() {
    return firstValueFrom(this.api.get<UserModel[]>('/User'));
  }
  public async promoteUser(user : PromoteUserRequest){
    return firstValueFrom(this.api.put<UserModel,PromoteUserRequest>('/User',user))
  }
  public async deleteUser(email : string){
        return firstValueFrom(this.api.delete('/User/' + email))
  }
}
