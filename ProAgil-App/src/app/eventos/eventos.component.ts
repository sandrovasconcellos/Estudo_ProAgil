import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.css']
})
export class EventosComponent implements OnInit {

  // eventos: any = [
  //   {
  //     EventoId: 1,
  //     Tema: 'Angular',
  //     Local: 'São Gonçalo',
  //   },
  //   {
  //     EventoId: 2,
  //     Tema: '.NET',
  //     Local: 'Niteroi',
  //   },
  //   {
  //     EventoId: 3,
  //     Tema: 'Angular .NET',
  //     Local: 'Rio de Janeiro',
  //   }
  // ];

  eventos: any;
  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.getEventos();
  }

  getEventos() {
    this.http.get('http://localhost:5000/api/WeatherForecast').subscribe(response => {
      this.eventos = response;
    }, error => {
      console.log(error);
    });
  }
}
