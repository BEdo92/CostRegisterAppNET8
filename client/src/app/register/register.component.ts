import { Component, inject, output } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { AbstractControl, FormBuilder, FormGroup, ReactiveFormsModule, ValidatorFn, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { TextInputComponent } from '../_forms/text-input/text-input.component';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [ReactiveFormsModule, TextInputComponent],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  private accountService = inject(AccountService);
  private fb = inject(FormBuilder);
  private router = inject(Router);
  currencies: string[] = [];
  cancelRegister = output<boolean>();
  registerForm: FormGroup = new FormGroup({});
  validationErrors: string[] | undefined;

  ngOnInit(): void {
    this.loadData();
  }

  initializeForm() {
    this.registerForm = this.fb.group({
      username: ['', Validators.required],
      password: ['', [Validators.required, Validators.minLength(4),
        Validators.maxLength(15)]],
      confirmPassword: ['', [Validators.required, this.matchValues('password')]],
      email: ['', [Validators.required, Validators.email]],
      phoneNumber: ['', Validators.required],
      currency: [this.currencies, Validators.required]
    });
    this.registerForm.controls['password'].valueChanges.subscribe({
      next: () => this.registerForm.controls['confirmPassword'].updateValueAndValidity()
    });
  }

  matchValues(matchTo: string): ValidatorFn {
    return (control: AbstractControl) => {
      return control.value === control.parent?.get(matchTo)?.value ? null : {isMatching: true}; 
    }
  }

  register() {
    this.accountService.register(this.registerForm.value).subscribe({
      next: _ => this.router.navigateByUrl('/usersport'),
      error: error => this.validationErrors = error
    });
  }

  cancel() {
    this.cancelRegister.emit(false);
    console.log('cancelled');
  }

  loadData() {
    this.accountService.getCurrencies().subscribe({
      next: currency => {
        this.currencies = currency;
        console.log(this.currencies);
        this.initializeForm();
      },
      error: error => this.validationErrors = error
    });
  }
}
