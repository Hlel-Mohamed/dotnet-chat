import {HttpClient, HttpClientModule} from '@angular/common/http';
import {Component, OnInit} from '@angular/core';
import {Emitters} from '../emitters/emitters';
import {CommonModule} from '@angular/common';
import {FormsModule} from '@angular/forms';
import Pusher from 'pusher-js';

/**
 * HomeComponent is a component that handles the home page of the application.
 * It includes methods for initializing the component and submitting messages.
 */
@Component({
  selector: 'app-home',
  standalone: true,
  imports: [FormsModule, HttpClientModule, CommonModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css',
})
export class HomeComponent implements OnInit {
  name!: string;
  message = '';
  messages: any[] = [];
  authenticated = false;

  /**
   * The constructor for the HomeComponent class.
   * @param {HttpClient} http - An instance of HttpClient for making HTTP requests.
   */
  constructor(private http: HttpClient) {
  }

  /**
   * ngOnInit is a lifecycle hook that is called after data-bound properties of a directive are initialized.
   * In this method, we make a GET request to the /api/user endpoint and subscribe to the response.
   * We also initialize Pusher for real-time functionality and subscribe to the 'chat' channel.
   */
  ngOnInit(): void {
    this.http.get<any>('http://localhost:8000/api/user', {withCredentials: true}).subscribe(
      (response) => {
        this.name = response.name;
        this.authenticated = true;
        Emitters.authEmitter.emit(true);
      },
      (error) => {
        this.name = 'Test';
        Emitters.authEmitter.emit(false);
      }
    );
    Pusher.logToConsole = true;

    const pusher = new Pusher('95e40c80d534b8b68d50', {
      cluster: 'eu'
    });

    const channel = pusher.subscribe('chat');
    channel.bind('message', (data: any) => {
      this.messages.push(data);
    });
  }

  /**
   * submit is a method that makes a POST request to the /chat/message endpoint with the current username and message.
   * After the request is made, it resets the message input field.
   */
  submit(): void {
    this.http.post('http://localhost:8000/chat/message', {
      username: this.name,
      message: this.message,
    }).subscribe(() => {
      this.message = '';
    });
  }
}
