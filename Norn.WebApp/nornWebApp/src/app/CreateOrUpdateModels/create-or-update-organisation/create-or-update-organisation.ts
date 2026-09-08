import { CommonModule } from '@angular/common';
import { ChangeDetectorRef, Component, Inject, inject, OnInit } from '@angular/core';
import {
  MAT_DIALOG_DATA,
  MatDialog,
  MatDialogContent,
  MatDialogModule,
  MatDialogRef,
} from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { CreateOrganisationModel } from '../../models/create-organisation-model';
import { FormsModule } from '@angular/forms';
import { CreateOrUpdateOrganisationData } from './create-or-update-organisation-data';
import { RoomsRelation } from '../../models/rooms-relation';
import { MatSelectModule } from '@angular/material/select';


@Component({
  selector: 'app-create-or-update-organisation',
  standalone: true,
  imports: [
    CommonModule,
    MatDialogModule,
    MatButtonModule,
    MatDialogContent,
    MatFormFieldModule,
    MatSelectModule,
    MatInputModule,
    FormsModule,
  ],
  templateUrl: './create-or-update-organisation.html',
})
export class CreateOrUpdateOrganisation implements OnInit {
  constructor(
    @Inject(MAT_DIALOG_DATA) public organisationToUpdate: CreateOrUpdateOrganisationData,
  ) {}
  ngOnInit(): void {
      
      this.roomsToAdd = this.organisationToUpdate.rooms;
    if (this.organisationToUpdate.organisationToUpdate != null) {
      this.organisation = this.organisationToUpdate.organisationToUpdate;
      if(this.organisationToUpdate.currentRoomsIds != null){
      console.log(this.organisationToUpdate); 
      this.organisation.roomIds = this.organisationToUpdate.currentRoomsIds;
      }
    }
  }
  public organisation: CreateOrganisationModel = new CreateOrganisationModel({
    name: '',
    roomIds: [],
  });
  public roomsToAdd : RoomsRelation[] = [];
  readonly dialogRef = inject(MatDialogRef<CreateOrUpdateOrganisation>);
  close() {
    this.dialogRef.close();
  }
  save() {
    this.dialogRef.close({
      organisation: this.organisation,
    });
  }
}
