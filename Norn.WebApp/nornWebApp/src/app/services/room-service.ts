import { inject, Injectable } from '@angular/core';
import { BaseService } from './base-service';
import { NornApi } from './norn-api';
import { CreateOrUpdateRoom } from '../CreateOrUpdateModels/create-or-update-room/create-or-update-room';
import { firstValueFrom } from 'rxjs';
import { RoomModel } from '../models/room-model';
import { CreateOrUpdateRoomModel } from '../models/create-or-update-room-model';

@Injectable({
  providedIn: 'root',
})
export class RoomService extends BaseService {
  private readonly api = inject(NornApi);
  public override apiPath: string = '/room';

  public async createRoom(request: CreateOrUpdateRoom) {
    return firstValueFrom(
      this.api.post<CreateOrUpdateRoom, CreateOrUpdateRoom>(this.apiPath, request),
    );
  }
  public async getAllRooms() {
    return firstValueFrom(this.api.get<CreateOrUpdateRoomModel[]>(this.apiPath));
  }
  public async getAllRelatedOrgs(id : number){
    return firstValueFrom(this.api.get<number[]>(this.apiPath + '/Related/' + id));
  }
  public async updateRoom(request : CreateOrUpdateRoomModel){
    return firstValueFrom(this.api.put<CreateOrUpdateRoomModel,CreateOrUpdateRoomModel>(this.apiPath,request))
  }
  public async deleteRoom(id : number){
    return firstValueFrom(this.api.delete<boolean>(this.apiPath + '/' + id))
  }
}
