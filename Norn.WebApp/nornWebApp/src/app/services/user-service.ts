import { inject, Injectable } from '@angular/core';
import { NornApi } from './norn-api';
import { UserModel } from '../models/user-model';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  private readonly api = inject(NornApi);

  createUser(user: UserModel) {
    return this.api.post<UserModel, UserModel>('/users', user);
  }
}
