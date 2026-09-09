import { CommonModule } from '@angular/common';
import { Component, inject, Inject, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import {
  MAT_DIALOG_DATA,
  MatDialogContent,
  MatDialogModule,
  MatDialogRef,
} from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { CreateOrUpdateRoomModel } from '../../models/create-or-update-room-model';
import { TimeLease } from '../../enums/timeLease';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatSelectModule } from '@angular/material/select';
import { CreateOrUpdateOrganisation } from '../create-or-update-organisation/create-or-update-organisation';
import { CreateOrUpdateRoomData } from './create-or-update-room-data';
import { OrganisationsRelation } from '../../models/organisations-relation';

@Component({
  selector: 'app-create-or-update-room',
  imports: [
    CommonModule,
    MatDialogModule,
    MatButtonModule,
    MatDialogContent,
    MatCheckboxModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    FormsModule,
  ],
  templateUrl: './create-or-update-room.html',
})
export class CreateOrUpdateRoom implements OnInit {
  constructor(@Inject(MAT_DIALOG_DATA) public data: CreateOrUpdateRoomData) {
    if (data != null) {
      this.orgsToAdd = this.data.organisations;

      if (data.request != null) {
        this.room = data.request;
        if (data.currentOrganisationIds != null) {
          this.room.organisationIds = this.data.currentOrganisationIds;
        }
      }
    }
  }

  timeLeaseOptions = Object.keys(TimeLease)
    .filter((key) => isNaN(Number(key)))
    .map((key) => ({
      label: key,
      value: TimeLease[key as keyof typeof TimeLease],
    }));
  public room: CreateOrUpdateRoomModel = {
    id: 0,
    monday: true,
    tuesday: true,
    wensday: true,
    thursday: true,
    friday: true,
    saturday: true,
    sunday: true,
    fromHour: 0,
    toHour: 24,
    timeLease: TimeLease.Hours,
    increment: 1,
    organisationIds: [],
    name: '',
  };
  ngOnInit(): void {}
  public orgsToAdd: OrganisationsRelation[] = [];

  readonly dialogRef = inject(MatDialogRef<CreateOrUpdateOrganisation>);
  close() {
    this.dialogRef.close();
  }
  save() {
    this.dialogRef.close({
      room: this.room,
    });
  }
}
