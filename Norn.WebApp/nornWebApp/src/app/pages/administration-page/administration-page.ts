import { ChangeDetectorRef, Component, inject, OnInit } from '@angular/core';
import { CreateOrganisationModel } from '../../models/create-organisation-model';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { CreateOrUpdateOrganisation } from '../../CreateOrUpdateModels/create-or-update-organisation/create-or-update-organisation';
import { CommonModule } from '@angular/common';
import { OrganisationService } from '../../services/organisation-service';
import { OrganisationModel } from '../../models/organisation-model';
import { ActivatedRoute } from '@angular/router';
import { CreateOrUpdateRoom } from '../../CreateOrUpdateModels/create-or-update-room/create-or-update-room';
import { RoomService } from '../../services/room-service';
import { RoomModel } from '../../models/room-model';
import { CreateOrUpdateOrganisationData } from '../../CreateOrUpdateModels/create-or-update-organisation/create-or-update-organisation-data';
import { CreateOrUpdateRoomModel } from '../../models/create-or-update-room-model';
import { CreateOrUpdateRoomData } from '../../CreateOrUpdateModels/create-or-update-room/create-or-update-room-data';

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
    this.organisations = this.route.snapshot.data['organisations'] as OrganisationModel[];
    this.rooms = this.route.snapshot.data['rooms'] as CreateOrUpdateRoomModel[];
  }
  private organisationService = inject(OrganisationService);
  private roomService = inject(RoomService);
  public organisations: OrganisationModel[] = [];
  public rooms: CreateOrUpdateRoomModel[] = [];

  async CreateOrganisation() {
    const dialogRef = this.dialog.open(CreateOrUpdateOrganisation, {
      data: new CreateOrUpdateOrganisationData({
        organisationToUpdate: null,
        rooms: this.rooms,
      }),
    });
    dialogRef.afterClosed().subscribe(async (result) => {
      console.log(result.organisation);
      await this.organisationService.createOrginsation(result.organisation);
      await this.updateList();
    });
  }

  async updateOrganisation(organisation: CreateOrganisationModel) {
    const ids = await this.organisationService.getRelatedRooms(organisation.id);
    const dialogRef = this.dialog.open(CreateOrUpdateOrganisation, {
      data: new CreateOrUpdateOrganisationData({
        organisationToUpdate: organisation,
        rooms: this.rooms,
        currentRoomsIds: ids,
      }),
    });

    dialogRef.afterClosed().subscribe(async (result) => {
      if (result != undefined) {
        await this.organisationService.updateOrganisation(result.organisation);
        await this.updateList();
      }
    });
  }
  async deleteOrganisation(id: number) {
    await this.organisationService.deleteOrganisation(id);
    await this.updateList();
  }

  async updateList() {
    this.organisations = await this.organisationService.getAllOrganisations();
    this.rooms = await this.roomService.getAllRooms();
    this.cdr.detectChanges();
  }
  async createRoom() {
    const dialogRef = this.dialog
      .open(CreateOrUpdateRoom, {
        data : new CreateOrUpdateRoomData({
          request : null,
          currentOrganisationIds : [],  
          organisations : this.organisations,
        }) ,
        width: '800px',
      })
      .afterClosed()
      .subscribe(async (result) => {
        if (result != undefined) {
          await this.roomService.createRoom(result.room);
          this.updateList();
        }
      });
  }
  async updateRoom(room : CreateOrUpdateRoomModel){
    const orgIds = await this.roomService.getAllRelatedOrgs(room.id);
    console.log(orgIds);
    const dialogRef = this.dialog.open(CreateOrUpdateRoom, {
        data: new CreateOrUpdateRoomData ({
           request : room,
           organisations : this.organisations,
           currentOrganisationIds : orgIds
        }),
        width: '800px',
      }).afterClosed().subscribe(async (result) => {
        if(result !== undefined){
          await this.roomService.updateRoom(room);
          this.updateList();
        }
      })
  }

  async deleteRoom(id : number){
    await this.roomService.deleteRoom(id);
    await this.updateList();
  }
}
