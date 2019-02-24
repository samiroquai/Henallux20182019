import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { RootObject, Forecast } from './model/OpenWeatherMap';
import { Observable } from 'rxjs';
import {map} from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class WeatherService {

  constructor(private http:HttpClient) { }

  chargerPrevisionsMeteo():Observable<Forecast[]>{
    // L'appel est ici "stubbé". Pour quelle raison? Afin de ne pas dépendre du service OpenWeatherMap et de l'API Key qu'il faut lui communiquer. Le contenu retourné est celui repris dans le fichier json inclus à cette solution.
    return this.http.get<RootObject>("./assets/fakedata.json")
    // Pourquoi projeter le résultat qui est déjà de type Forecast vers une nouvelle instance de Forecast?
    // Parce que lorsqu'une classe est instanciée par désérialisation, son prototype ne contient pas 
    // les méthodes d'instance déclarées dans la classe. Pour contourner ce problème, l'appel à Object.assign est possible
    .pipe(map(d=>d.list.map(forecast=>Object.assign(new Forecast(), forecast))));
  } 
}
