import { CommonModule } from '@angular/common';
import {Component, OnInit} from '@angular/core';
import { FormControl, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatSelectModule } from '@angular/material/select';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { WeatherResponse, Wind } from './weather-object';

/**
 * @title Basic select
 */
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  standalone: true,
  imports:[MatSelectModule, CommonModule, FormsModule, ReactiveFormsModule]
})
export class AppComponent implements OnInit  {
    constructor(private http: HttpClient) {}
    countries : any = [];
    cities : any = [];
    city = new FormControl('', Validators.required);
    weatherResponse: WeatherResponse = {} as WeatherResponse
    
    ngOnInit():void
    {
      fetch('./assets/countries+cities.json').then(res => res.json())
    .then(jsonData => {
        jsonData.forEach((element: any) => {
            this.countries.push({id: element.id, name : element.name});
        })
    });
    }

    fillCities(value : any)
    {
        this.cities = [];
        fetch('./assets/countries+cities.json').then(res => res.json())
      .then(jsonData => {
          jsonData.forEach((element: any) => {
              if(element.id === value)
              {
                  element.cities.forEach((cit: any) =>
                  {
                    this.cities.push({name : cit.name});
                  })
              }
          })
        });
    }

    onClickSubmit(data:any) {
      var headers = new HttpHeaders()
      
      headers.append("Access-Control-Allow-Origin", "*");
      headers.append("Access-Control-Allow-Methods", "GET,HEAD,OPTIONS,POST,PUT");
      headers.append("Access-Control-Allow-Headers", "Origin, X-Requested-With, contentType,Content-Type, Accept, Authorization");
      
       this.http.get<any>('http://localhost:5000/weather-city?city=' + this.city.value, {headers} ).subscribe((data : WeatherResponse) => {
        this.weatherResponse = data;
       })
   }
}
