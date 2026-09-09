import { CommonModule } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { UserService } from '../../services/user-service';
import { UserModel } from '../../models/user-model';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth-service';

@Component({
  selector: 'app-login-page',
  imports: [FormsModule, CommonModule],
  templateUrl: './login-page.html',
})
export class LoginPage implements OnInit {
  private userService = inject(UserService);
  private router = inject(Router);
  private authService = inject(AuthService);
  public userName = '';
  public password = '';

  ngOnInit(): void {
    if (this.authService.isAuthenticated()) {
      this.router.navigate(['/home']);
    }
  }

  public async CreateUser() {
    if (this.userName.length != 0 && this.password.length != 0) {
      const userModel = new UserModel({
        email: this.userName,
        password: this.password,
      });
      await this.userService.createUser(userModel);
    }
  }
  public async Login() {
    const userModel = new UserModel({
      email: this.userName,
      password: this.password,
    });

    var result = await this.userService.loginUser(userModel);
    localStorage.setItem('token', result.token);
    this.router.navigate(['/home']);
  }
}
