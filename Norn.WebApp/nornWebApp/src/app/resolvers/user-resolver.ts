import { ResolveFn } from '@angular/router';
import { UserModel } from '../models/user-model';
import { inject } from '@angular/core';
import { UserService } from '../services/user-service';

export const userResolver: ResolveFn<UserModel[] | null> = (route, state) => {
  const userService = inject(UserService);
  const usersResult = userService.getAllUsers();
  console.log(usersResult);
  return usersResult;
};
