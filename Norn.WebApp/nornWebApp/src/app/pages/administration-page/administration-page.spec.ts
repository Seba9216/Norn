import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdministrationPage } from './administration-page';

describe('AdministrationPage', () => {
  let component: AdministrationPage;
  let fixture: ComponentFixture<AdministrationPage>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AdministrationPage],
    }).compileComponents();

    fixture = TestBed.createComponent(AdministrationPage);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
