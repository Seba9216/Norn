import { ChangeDetectorRef, Component, inject, OnInit } from '@angular/core';
import { CreateOrganisationModel } from '../../models/create-organisation-model';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { CreateOrUpdateOrganisation } from '../../CreateOrUpdateModels/create-or-update-organisation/create-or-update-organisation';
import { CommonModule } from '@angular/common';
import { OrganisationService } from '../../services/organisation-service';
import { OrganisationModel } from '../../models/organisation-model';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-administration-page',
  standalone: true,
  imports: [CommonModule, MatDialogModule],
  templateUrl: './administration-page.html',
})
export class AdministrationPage implements OnInit {
  private route = inject(ActivatedRoute);
  private cdr = inject(ChangeDetectorRef);

  constructor(private dialog: MatDialog) {}
  ngOnInit(): void {
    console.log(this.route.snapshot.data);
    this.organisations = this.route.snapshot.data['organisations'] as OrganisationModel[];
  }
  private organisationService = inject(OrganisationService);
  public organisations: OrganisationModel[] = [];

  async CreateOrganisation() {
    const dialogRef = this.dialog.open(CreateOrUpdateOrganisation, {
      data: null,
    });
    dialogRef.afterClosed().subscribe(async (result) => {
      console.log(result.organisation);
      await this.organisationService.createOrginsation(result.organisation);
      await this.updateList();

    });
  }

  async updateOrganisation(organisation: CreateOrganisationModel) {
    const dialogRef = this.dialog.open(CreateOrUpdateOrganisation, {
      data: organisation,
    });

    dialogRef.afterClosed().subscribe(async (result) => {
      await this.organisationService.updateOrganisation(result.organisation);
      await this.updateList();

    });
  }

  async updateList() {
    this.organisations = await this.organisationService.getAllOrganisations();
    this.cdr.detectChanges();
  }
}
