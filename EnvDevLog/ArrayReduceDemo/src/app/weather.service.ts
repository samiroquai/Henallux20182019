import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { RootObject } from './model/OpenWeatherMap';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class WeatherService {

  constructor(private http:HttpClient) { }

  chargerPrevisionsMeteo():Observable<RootObject>{
    return this.http.get<RootObject>("./assets/fakedata.json");
  } 
}
