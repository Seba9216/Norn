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
  private route = inject(ActivatedRoute);
  private organisationService = inject(OrganisationService);
  private timeIntervalService = inject(TimeIntervalService);

  public organisations: OrganisationModel[] = [];
  public rooms: CreateOrUpdateRoomModel[] = [];
  public currentRooms: CreateOrUpdateRoomModel[] = [];
  selectedOrganisationId: number | null = null;
  selectedRoomId: number | null = null; 
  selectedInterval : TimeInterval | null = null; 
  ngOnInit(): void {
    this.organisations = this.route.snapshot.data['organisations'] as OrganisationModel[];
    this.rooms = this.route.snapshot.data['rooms'] as CreateOrUpdateRoomModel[];
  }

  async loadRooms(orgId: number) {
    const currentRoomsIds = await this.organisationService.getRelatedRooms(orgId);
    this.currentRooms = this.rooms.filter((room) => currentRoomsIds.some((id) => id === room.id));
  }
  
  allIntervals: TimeInterval[] = [];
  weeks: { start: Date; days: any[] }[] = [];
  currentWeekIndex = 0;

  async loadTimeIntervals(roomId: number) {
    const times = await this.timeIntervalService.GetTimeIntervalsRelatedToRoom(roomId);

    this.allIntervals = times;
    this.buildWeeks();
  }
  selectInterval(interval : TimeInterval){
    console.log(interval);
      this.selectedInterval = interval;
  }
  async MakeBooking(interval : TimeInterval){
    
  }

  private buildWeeks() {
    const groupedDays = new Map<string, TimeInterval[]>();

    this.allIntervals.forEach((interval) => {
      const date = new Date(interval.from);

      // UTC date key: YYYY-MM-DD
      const dateKey = date.toISOString().split('T')[0];

      if (!groupedDays.has(dateKey)) {
        groupedDays.set(dateKey, []);
      }

      groupedDays.get(dateKey)!.push(interval);
    });

    const allDates = [...groupedDays.keys()]
      .map((d) => new Date(`${d}T00:00:00Z`))
      .sort((a, b) => a.getTime() - b.getTime());

    if (!allDates.length) {
      return;
    }

    const weekMap = new Map<string, any[]>();

    allDates.forEach((date) => {
      const monday = new Date(date);

      const day = monday.getUTCDay();
      const diff = day === 0 ? -6 : 1 - day;

      monday.setUTCDate(monday.getUTCDate() + diff);

      const weekKey = monday.toISOString().split('T')[0];

      if (!weekMap.has(weekKey)) {
        weekMap.set(weekKey, []);
      }

      weekMap.get(weekKey)!.push({
        date,
        intervals: groupedDays.get(date.toISOString().split('T')[0]) ?? [],
      });
    });

    console.log(weekMap);

    this.weeks = [...weekMap.entries()].map(([weekStart, days]) => ({
      start: new Date(`${weekStart}T00:00:00Z`),
      days,
    }));
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
