import { Component, OnInit } from '@angular/core';
import { RouterModule } from '@angular/router';
import { Emitters } from '../emitters/emitters';
import { CommonModule } from '@angular/common';
import { HttpClient, HttpClientModule } from '@angular/common/http';

/**
 * NavComponent is a component that handles the navigation bar of the application.
 * It includes methods for initializing the component and logging out.
 */
@Component({
  selector: 'app-nav',
  standalone: true,
  imports: [RouterModule, CommonModule, HttpClientModule],
  templateUrl: './nav.component.html',
  styleUrl: './nav.component.css',
})
export class NavComponent implements OnInit {
  authenticated = false;

  /**
   * The constructor for the NavComponent class.
   * @param {HttpClient} http - An instance of HttpClient for making HTTP requests.
   */
  constructor(private http: HttpClient) {}

  /**
   * ngOnInit is a lifecycle hook that is called after data-bound properties of a directive are initialized.
   * In this method, we subscribe to the authEmitter to update the authenticated state.
   */
  ngOnInit(): void {
    Emitters.authEmitter.subscribe((auth: boolean) => {
      this.authenticated = auth;
    });
  }

  /**
   * logout is a method that makes a POST request to the /api/logout endpoint to log out the user.
   * After the request is made, it sets the authenticated state to false.
   */
  logout(): void {
    this.http
      .post('http://localhost:8000/api/logout', {}, { withCredentials: true })
      .subscribe(() => {
        this.authenticated = false;
      });
  }
}
