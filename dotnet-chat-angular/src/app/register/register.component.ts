import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
} from '@angular/forms';
import { Router } from '@angular/router';

/**
 * RegisterComponent is a component that handles the registration page of the application.
 * It includes methods for initializing the component and submitting the registration form.
 */
@Component({
  selector: 'app-register',
  standalone: true,
  imports: [FormsModule, ReactiveFormsModule, HttpClientModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css',
})
export class RegisterComponent implements OnInit {
  form!: FormGroup;

  /**
   * The constructor for the RegisterComponent class.
   * @param {FormBuilder} formBuilder - An instance of FormBuilder for creating the registration form.
   * @param {HttpClient} http - An instance of HttpClient for making HTTP requests.
   * @param {Router} router - An instance of Router for navigating to different routes.
   */
  constructor(
    private formBuilder: FormBuilder,
    private http: HttpClient,
    private router: Router
  ) {}

  /**
   * ngOnInit is a lifecycle hook that is called after data-bound properties of a directive are initialized.
   * In this method, we create a new FormGroup instance for the registration form.
   */
  ngOnInit(): void {
    this.form = this.formBuilder.group({
      name: '',
      email: '',
      password: '',
    });
  }

  /**
   * submit is a method that makes a POST request to the /api/register endpoint with the form data.
   * After the request is made, it navigates to the login page.
   */
  submit(): void {
    this.http
      .post('http://localhost:8000/api/register', this.form.getRawValue())
      .subscribe(() => {
        this.router.navigate(['/login']);
      });
  }
}
