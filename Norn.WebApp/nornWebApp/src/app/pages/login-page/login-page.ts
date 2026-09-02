import { CommonModule } from '@angular/common';
import { Component, inject, Inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { UserService } from '../../services/user-service';
import { UserModel } from '../../models/user-model';

@Component({
  selector: 'app-login-page',
  imports: [FormsModule, CommonModule],
  templateUrl: './login-page.html',
})
export class LoginPage {
  private userService = inject(UserService);
  public userName = '';
  public password = '';

  public async CreateUser() {
    const userModel = new UserModel({
      email: this.userName,
      password: this.password,
    });

    var result = await this.userService.createUser(userModel);
    console.log(result);
  }
  public async Login() {
    const userModel = new UserModel({
      email: this.userName,
      password: this.password,
    });

    var result = await this.userService.LoginUser(userModel);
    console.log(result);
  }
}
