import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Emitters } from '../emitters/emitters';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [HttpClientModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css',
})
export class HomeComponent implements OnInit {
  message!: string;
  

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.http.get<any>('http://localhost:8000/api/user', { withCredentials: true }).subscribe(
      (response) => {
        this.message = `Hello ${response.name}!`;
        Emitters.authEmitter.emit(true);
      },
      (error) => {
        this.message = 'You are not logged in!';
        Emitters.authEmitter.emit(false);
      }
    );
  }
}
