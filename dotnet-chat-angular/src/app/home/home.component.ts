import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Emitters } from '../emitters/emitters';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import Pusher from 'pusher-js';

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

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.http.get<any>('http://localhost:8000/api/user', { withCredentials: true }).subscribe(
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

  submit(): void {
    this.http.post('http://localhost:8000/chat/message', {
      username: this.name,
      message: this.message,
    }).subscribe(() => {
      this.message = '';
    });
  }
}
