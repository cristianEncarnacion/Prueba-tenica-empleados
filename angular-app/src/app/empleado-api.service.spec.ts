import { TestBed } from '@angular/core/testing';

import { EmpleadoApiService } from './empleado-api.service';

describe('EmpleadoApiService', () => {
  let service: EmpleadoApiService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(EmpleadoApiService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
