import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DisplayMessage } from './display-message';

describe('DisplayMessage', () => {
  let component: DisplayMessage;
  let fixture: ComponentFixture<DisplayMessage>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DisplayMessage],
    }).compileComponents();

    fixture = TestBed.createComponent(DisplayMessage);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
