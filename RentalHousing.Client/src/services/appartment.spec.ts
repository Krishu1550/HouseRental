import { TestBed } from '@angular/core/testing';

import { Appartment } from './appartment';

describe('Appartment', () => {
  let service: Appartment;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(Appartment);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
