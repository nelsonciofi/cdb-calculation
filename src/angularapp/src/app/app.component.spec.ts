import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { FormsModule } from '@angular/forms';
import { of } from 'rxjs/internal/observable/of';
import { throwError } from 'rxjs';
import { ApiService } from '../service/api.service';
import { AppComponent } from './app.component';

describe('AppComponent', () => {
  let mockApiService: jasmine.SpyObj<ApiService>;
  let fixture: ComponentFixture<AppComponent>;
  let app: AppComponent;

  beforeEach(async () => {
    mockApiService = jasmine.createSpyObj('ApiService', ['calculateCdb', 'subscribe']);

    await TestBed.configureTestingModule({
      imports: [FormsModule, HttpClientTestingModule],
      declarations: [AppComponent],
      providers: [{ provide: ApiService, useValue: mockApiService }]
    }).compileComponents();

    fixture = TestBed.createComponent(AppComponent);
    app = fixture.componentInstance;
  });

  it('should create the app', () => {
    expect(app).toBeTruthy();
  });

  it(`should have as title 'cdbCalculation'`, () => {
    expect(app.title).toEqual('cdbCalculation');
  });

  it('should initialize investment object with default values', () => {
    expect(app.investment.initialValue).toEqual(0);
    expect(app.investment.redeemTermMonths).toEqual(0);
  });

  it('should call onInitialValueInput and set initialValue to 0 when input value is negative', () => {
    const inputElement = fixture.nativeElement.querySelector('#initialValue');
    inputElement.value = '-100';
    inputElement.dispatchEvent(new Event('input'));
    expect(app.investment.initialValue).toEqual(0);
  });

  it('should call onInitialValueInput and set initialValue to 0 when input value is weird', () => {
    const inputElement = fixture.nativeElement.querySelector('#initialValue');
    inputElement.value = '0-1';
    inputElement.dispatchEvent(new Event('input'));
    expect(app.investment.initialValue).toEqual(0);
  });

  it('should call onRedeemTermMonthsInput and set redeemTermMonths to 0 when input value is negative', () => {
    const inputElement = fixture.nativeElement.querySelector('#redeemTermMonths');
    inputElement.value = '-10';
    inputElement.dispatchEvent(new Event('input'));
    expect(app.investment.redeemTermMonths).toEqual(0);
  });

  it('should call onRedeemTermMonthsInput and set redeemTermMonths to 0 when input value is weird', () => {
    const inputElement = fixture.nativeElement.querySelector('#redeemTermMonths');
    inputElement.value = '0-0';
    inputElement.dispatchEvent(new Event('input'));
    expect(app.investment.redeemTermMonths).toEqual(0);
  });

  it('should handle successful API response and set income', waitForAsync(async () => {
    const response = { gross: 1000, net: 900 };
    app.investment = { initialValue: -1, redeemTermMonths: -1 };
    mockApiService.calculateCdb.and.returnValue(of(response))

    const submitButton = fixture.nativeElement.querySelector('button');
    submitButton.click();
    await fixture.whenStable();
    fixture.detectChanges();

    expect(app.errorMessage).toBe('');
    expect(app.income?.gross).toEqual(response.gross);
  }));

  it('should handle API error response and set error message', waitForAsync(async () => {
    const errorResponse = {
      error: {
        isValid: false,
        errors: [
          { propertyName: 'InitialValue', message: 'Invalid initial value.' },
          { propertyName: 'RedeemTermMonths', message: 'Invalid redeem term.' }
        ]
      }
    };
    mockApiService.calculateCdb.and.returnValue(throwError(() => errorResponse));
    app.investment = { initialValue: 1000, redeemTermMonths: 12 };

    fixture.detectChanges();

    const submitButton = fixture.nativeElement.querySelector('button');
    submitButton.click();
    await fixture.whenStable();
    fixture.detectChanges();

    const expectedErrorMessage = 'Invalid initial value. Invalid redeem term.';
    expect(app.errorMessage).toBe(expectedErrorMessage);
    expect(app.income).toBeUndefined();

  }));

  it('should handle API server failure and set error message', waitForAsync(async () => {
    const errorResponse = {
      error: {
        "nothing":"to say"       
      }
    };
    mockApiService.calculateCdb.and.returnValue(throwError(() => errorResponse));
    app.investment = { initialValue: 1000, redeemTermMonths: 12 };

    fixture.detectChanges();

    const submitButton = fixture.nativeElement.querySelector('button');
    submitButton.click();
    await fixture.whenStable();
    fixture.detectChanges();

    const expectedErrorMessage = 'O serviço responsável por calcular o CDB parece estar passando por problemas. Tente novamente mais tarde.';
    expect(app.errorMessage).toBe(expectedErrorMessage);
    expect(app.income).toBeUndefined();
  }));
});
