import { TestBed } from '@angular/core/testing';
import { ResolveFn } from '@angular/router';

import { bookingResolver } from './booking-resolver';

describe('bookingResolver', () => {
  const executeResolver: ResolveFn<boolean> = (...resolverParameters) =>
    TestBed.runInInjectionContext(() => bookingResolver(...resolverParameters));

  beforeEach(() => {
    TestBed.configureTestingModule({});
  });

  it('should be created', () => {
    expect(executeResolver).toBeTruthy();
  });
});
