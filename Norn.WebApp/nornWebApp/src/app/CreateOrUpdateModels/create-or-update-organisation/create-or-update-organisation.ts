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
import { Action } from '../../enums/action';

@Component({
  selector: 'app-create-or-update-organisation',
  standalone: true,
  imports: [
    CommonModule,
    MatDialogModule,
    MatButtonModule,
    MatDialogContent,
    MatFormFieldModule,
    MatInputModule,
    FormsModule,
  ],
  templateUrl: './create-or-update-organisation.html',
})
export class CreateOrUpdateOrganisation implements OnInit {
  private actionToPerfom: Action = Action.Create;
  constructor(
    @Inject(MAT_DIALOG_DATA) public organisationToUpdate: CreateOrganisationModel | null,
  ) {}
  ngOnInit(): void {
    if (this.organisationToUpdate != null) {
      this.organisation = this.organisationToUpdate;
    }
  }
  public organisation: CreateOrganisationModel = new CreateOrganisationModel({
    name: '',
    roomIds: [],
  });

  readonly dialogRef = inject(MatDialogRef<CreateOrUpdateOrganisation>);
  close() {
    this.dialogRef.close();
  }
  save() {
    this.dialogRef.close({
      action: this.actionToPerfom,
      organisation: this.organisation,
    });
  }
}
