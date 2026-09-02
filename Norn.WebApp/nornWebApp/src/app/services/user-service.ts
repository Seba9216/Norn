import { inject, Injectable } from '@angular/core';
import { NornApi } from './norn-api';
import { UserModel } from '../models/user-model';
import { firstValueFrom } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  private readonly api = inject(NornApi);

  public async createUser(user: UserModel) {
    return firstValueFrom(this.api.post<boolean, UserModel>('/User', user));  }
  
  public async LoginUser(user: UserModel) {
    return firstValueFrom(this.api.post<string,UserModel>('/User/Login', user))
  }
}
