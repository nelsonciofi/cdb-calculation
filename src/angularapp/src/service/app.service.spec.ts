import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { ApiService } from './api.service';
import { environment } from '../environments/environment';

describe('ApiService', () => {
  let service: ApiService;
  let httpTestingController: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [ApiService]
    });

    service = TestBed.inject(ApiService);
    httpTestingController = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpTestingController.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should call calculateCdb and return data', () => {
    const dummyData = { investment: { initialValue: 1000, redeemTermMonths: 12 } };
    const dummyResponse = { "gross": 1123.08, "net": 1098.47 };

    service.calculateCdb(dummyData).subscribe(response => {
      expect(response).toEqual(dummyResponse);
    });

    const req = httpTestingController.expectOne(`${environment.apiBaseUrl}/cdbCalculation`);
    expect(req.request.method).toBe('POST');
    req.flush(dummyResponse);
  });
});
