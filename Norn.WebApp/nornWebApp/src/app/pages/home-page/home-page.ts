import { CommonModule } from '@angular/common';
import { OrganisationModel } from '../../models/organisation-model';

import { MatSelectModule } from '@angular/material/select';
import { ActivatedRoute } from '@angular/router';
import { CreateOrUpdateRoomModel } from '../../models/create-or-update-room-model';
import { OrganisationService } from '../../services/organisation-service';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { TimeIntervalService } from '../../services/time-interval-service';
import { TimeInterval } from '../../models/time-interval';
import {
  AfterViewInit,
  ChangeDetectorRef,
  Component,
  inject,
  OnInit,
  ViewChild,
} from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { BookingService } from '../../services/booking-service';
import { CreateBookingRequest } from '../../models/create-booking-request';
import { AuthService } from '../../services/auth-service';
import { UserService } from '../../services/user-service';

@Component({
  selector: 'app-home-page',
  imports: [
    CommonModule,
    MatSelectModule,
    MatTableModule,
    MatPaginatorModule,
    MatButtonModule,
    MatCardModule,
  ],
  templateUrl: './home-page.html',
})
export class HomePage implements OnInit {
  private authService = inject(AuthService);
  private userService = inject(UserService);
  private route = inject(ActivatedRoute);
  private organisationService = inject(OrganisationService);
  private timeIntervalService = inject(TimeIntervalService);
  private bookingService = inject(BookingService);

  public organisations: OrganisationModel[] = [];
  public rooms: CreateOrUpdateRoomModel[] = [];
  public currentRooms: CreateOrUpdateRoomModel[] = [];
  selectedOrganisationId: number | null = null;
  selectedRoomId: number | null = null;
  selectedInterval: TimeInterval | null = null;
  timeIntervals: TimeInterval[] = [];
  weeks: { start: Date; days: any[] }[] = [];
  currentWeekIndex = 0;

  ngOnInit(): void {
    this.organisations = this.route.snapshot.data['organisations'] as OrganisationModel[];
    this.rooms = this.route.snapshot.data['rooms'] as CreateOrUpdateRoomModel[];
  }

  async loadRooms(orgId: number) {
    const currentRoomsIds = await this.organisationService.getRelatedRooms(orgId);
    this.currentRooms = this.rooms.filter((room) => currentRoomsIds.some((id) => id === room.id));
  }

  async loadTimeIntervals(roomId: number) {
    const times = await this.timeIntervalService.GetTimeIntervalsRelatedToRoom(roomId);
    console.log(times);
    this.timeIntervals = times;
    this.buildWeeks();
  }
  selectInterval(interval: TimeInterval) {
    this.selectedInterval = interval;
  }
  async MakeBooking(interval: TimeInterval) {
    const mail = this.authService.getEmail();
    console.log(mail);
    if (mail != null) {
      const intervaldId = await this.timeIntervalService.GetIdByTimeAndRoomID(interval);
      console.log(intervaldId);
      const userId = await this.userService.getUserIdFromEmail(mail);
      const bookingRequest = new CreateBookingRequest({
        userId: userId,
        timeIntervalId: intervaldId,
        roomId: interval.roomId,
      });

      await this.bookingService.createBooking(bookingRequest);
    }
  }

  private buildWeeks() {
    const weekMap = new Map<string, any[]>();

    this.timeIntervals.forEach((interval) => {
      const date = new Date(interval.from);
      const day = date.getUTCDay();
      const diff = day === 0 ? -6 : 1 - day;

      const monday = new Date(date);
      monday.setUTCDate(monday.getUTCDate() + diff);

      const weekKey = monday.toISOString().split('T')[0];
      const dateKey = date.toISOString().split('T')[0];

      if (!weekMap.has(weekKey)) {
        weekMap.set(weekKey, []);
      }

      let week = weekMap.get(weekKey)!;

      let dayEntry = week.find((d) => d.date.toISOString().split('T')[0] === dateKey);

      if (!dayEntry) {
        dayEntry = {
          date,
          intervals: [],
        };
        week.push(dayEntry);
      }

      dayEntry.intervals.push(interval);
    });

    this.weeks = [];

    for (const [weekStart, days] of weekMap) {
      this.weeks.push({
        start: new Date(`${weekStart}T00:00:00Z`),
        days,
      });
    }
  }

  get currentWeek() {
    return this.weeks[this.currentWeekIndex];
  }

  nextWeek() {
    if (this.currentWeekIndex < this.weeks.length - 1) {
      this.currentWeekIndex++;
    }
  }

  previousWeek() {
    if (this.currentWeekIndex > 0) {
      this.currentWeekIndex--;
    }
  }
}
