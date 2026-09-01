import { TestBed } from '@angular/core/testing';

import { NornApi } from './norn-api';

describe('NornApi', () => {
  let service: NornApi;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(NornApi);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
