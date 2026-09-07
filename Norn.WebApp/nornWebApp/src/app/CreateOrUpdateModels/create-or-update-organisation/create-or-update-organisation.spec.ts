import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateOrUpdateOrganisation } from './create-or-update-organisation';

describe('CreateOrUpdateOrganisation', () => {
  let component: CreateOrUpdateOrganisation;
  let fixture: ComponentFixture<CreateOrUpdateOrganisation>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CreateOrUpdateOrganisation],
    }).compileComponents();

    fixture = TestBed.createComponent(CreateOrUpdateOrganisation);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
