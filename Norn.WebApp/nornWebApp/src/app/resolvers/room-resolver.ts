import { ResolveFn } from '@angular/router';
import { RoomModel } from '../models/room-model';
import { RoomService } from '../services/room-service';
import { inject } from '@angular/core';

export const RoomResolver: ResolveFn<RoomModel[] | null> = (route, state) => {
  const roomService = inject(RoomService);
  const roomresult = roomService.getAllRooms();
  return roomresult;
};
