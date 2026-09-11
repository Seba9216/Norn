import { inject, Injectable } from '@angular/core';
import { NornApi } from './norn-api';
import { BaseService } from './base-service';
import { TimeInterval } from '../models/time-interval';
import { firstValueFrom } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class TimeIntervalService extends BaseService {
  private readonly api = inject(NornApi);
  public override apiPath: string = '/TimeInterval';

  public async GetTimeIntervalsRelatedToRoom(roomId: number) {
    return firstValueFrom(this.api.get<TimeInterval[]>(this.apiPath + '/' + roomId));
  }
  public async GetIdByTimeAndRoomID(timeInterval: TimeInterval) {
    return firstValueFrom(
      this.api.get<number>(
        this.apiPath + '/' + timeInterval.roomId + '/' + timeInterval.from + '/' + timeInterval.to,
      ),
    );
  }
}
