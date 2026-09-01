import { CanActivateFn } from '@angular/router';

export const authGuardGuard: CanActivateFn = (route, state) => {
  console.log(route);
  console.log(state);
  return true;
};
