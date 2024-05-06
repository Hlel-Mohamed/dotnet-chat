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
 * LoginComponent is a component that handles the login page of the application.
 * It includes methods for initializing the component and submitting the login form.
 */
@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule, ReactiveFormsModule, HttpClientModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
})
export class LoginComponent implements OnInit {
  form!: FormGroup;

  /**
   * The constructor for the LoginComponent class.
   * @param {FormBuilder} formBuilder - An instance of FormBuilder for creating the login form.
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
   * In this method, we create a new FormGroup instance for the login form.
   */
  ngOnInit(): void {
    this.form = this.formBuilder.group({
      email: '',
      password: '',
    });
  }

  /**
   * submit is a method that makes a POST request to the /api/login endpoint with the form data.
   * After the request is made, it navigates to the home page.
   */
  submit(): void {
    this.http
      .post('http://localhost:8000/api/login', this.form.getRawValue(), {
        withCredentials: true,
      })
      .subscribe(() => {
        this.router.navigate(['/']);
      });
  }
}
