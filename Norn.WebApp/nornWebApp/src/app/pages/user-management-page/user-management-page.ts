import { ChangeDetectorRef, Component, inject, OnInit, runInInjectionContext } from '@angular/core';
import { UserModel } from '../../models/user-model';
import { UserService } from '../../services/user-service';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { PromoteUserRequest } from '../../models/promote-user-request';
import { email } from '@angular/forms/signals';

@Component({
  selector: 'app-user-management-page',
  imports: [],
  templateUrl: './user-management-page.html',
})
export class UserManagementPage implements OnInit {
  private userService = inject(UserService)
  private route = inject(ActivatedRoute);
  private cdr = inject(ChangeDetectorRef);
  
  public users: UserModel[] | null = null;
  
  ngOnInit(): void {
    const users = this.route.snapshot.data['users'] as UserModel[];
    this.users = users;
  }
  async promoteUser(user : UserModel){
    const promoteRequest = new PromoteUserRequest({
       email : user.email,
       role : "Admin"
    })
    await this.userService.promoteUser(promoteRequest);
    await this.updateList();
  }
  async updateList() {
    this.users = await this.userService.getAllUsers();
    this.cdr.detectChanges();
    
  };
  async deleteUser(user : UserModel){
   await this.userService.deleteUser(user.email); 
  await this.updateList();
  }
}
