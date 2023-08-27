import { Component } from '@angular/core';
import { ApiService } from '../service/api.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

export class AppComponent {
  public investment: Investment = { initialValue: 0, redeemTermMonths: 0 };
  public income?: Income;  
  public errorMessage: string = '';
  public carregando: boolean = false;
  constructor(private apiService: ApiService) { }

  title = 'cdbCalculation';

  submitForm() {
    this.errorMessage = '';
    this.income = undefined;
    this.carregando = true;

    this.apiService.calculateCdb({ investment: this.investment }).subscribe({
      next: (response: any) => {
        this.carregando = false;
        const income = response as Income;
        if (!income) {
          this.errorMessage = 'Ocorreu um resultado inesperado. Tente novamente.';
        } else {
          this.income = income;
        }
      },
      error: (error: any) => {
        this.carregando = false;
        if (error?.error?.errors) {
          this.errorMessage = error.error.errors.map((e: Error) => e.message).join(' ');
        } else {
          this.errorMessage = 'O serviço responsável por calcular o CDB parece estar passando por problemas. Tente novamente mais tarde.';
        }
      }
    });
  }

  onInitialValueInput(event: any) {
    const inputValue = event.target.value;
    if (!inputValue || inputValue < 0) {
      this.investment.initialValue = 0;
    }
  }

  onRedeemTermMonthsInput(event: any) {
    const inputValue = event.target.value;
    if (!inputValue || inputValue < 0) {
      this.investment.redeemTermMonths = 0;
    }
  }
}

interface Error {
  propertyName: string;
  message: string;
}

interface Investment {
  initialValue: number;
  redeemTermMonths: number;
}

interface Income {
  gross: number;
  net: number;
}

