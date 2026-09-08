import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateOrUpdateRoom } from './create-or-update-room';

describe('CreateOrUpdateRoom', () => {
  let component: CreateOrUpdateRoom;
  let fixture: ComponentFixture<CreateOrUpdateRoom>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CreateOrUpdateRoom],
    }).compileComponents();

    fixture = TestBed.createComponent(CreateOrUpdateRoom);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
