import { Component, OnInit } from '@angular/core';
import { WeatherService } from './weather.service';
import { Forecast } from './model/OpenWeatherMap';
import { Accumulator } from './model/Accumulator';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  listeReduiteArrayFind: Forecast[];
  listeReduiteAssociativeArray: Forecast[];
  listeReduiteClasseAccumulation: Forecast[];
  listeOriginale: Forecast[];
  //objectKeys est utilisé pour itérer sur l'associative array par ses clés. Voir template.
  private objectKeys = Object.keys;
  private tempsExecutionArrayFind: number;
  private tempsExecutionAssociativeArray: number;
  private tempsExecutionClasseAccumulation: number;
  constructor(private service: WeatherService) {

  }

  ngOnInit(): void {

    const reducerUtilisantClasseDAccumulation = (accumulator: Accumulator, previsionActuelle: Forecast) => {
      const currentDate=previsionActuelle.obtenirCleDate();
      if(accumulator.date!==currentDate){
        accumulator.forecasts.push(previsionActuelle);
        accumulator.date=currentDate;
      }
      return accumulator;
    };

    const reducerUtilisantFind = (accumulator: Array<Forecast>, previsionActuelle: Forecast) => {
      const clePrevisionActuelle = previsionActuelle.obtenirCleDate();
      if (!accumulator.find(previsionDejaDansAccumulateur => previsionDejaDansAccumulateur.obtenirCleDate() === clePrevisionActuelle)) {
        accumulator.push(previsionActuelle);
      }
      return accumulator;
    };

    const reducerUtilisantAssociativeArray = (accumulator: any, previsionActuelle: Forecast) => {
      const clePrevisionActuelle = previsionActuelle.obtenirCleDate();
      if (!accumulator[clePrevisionActuelle]) {
        accumulator[clePrevisionActuelle] = previsionActuelle;
      }
      return accumulator;
    }

    this.service.chargerPrevisionsMeteo()
    .subscribe(liste => {
      this.listeOriginale = liste;
      this.tempsExecutionArrayFind = this.executerNFoisEtRetournerMoyenneTempsExecution(() => this.listeReduiteArrayFind = liste.reduce(reducerUtilisantFind, []));
      this.tempsExecutionAssociativeArray = this.executerNFoisEtRetournerMoyenneTempsExecution(() => this.listeReduiteAssociativeArray = liste.reduce(reducerUtilisantAssociativeArray, {}));
      this.tempsExecutionClasseAccumulation=this.executerNFoisEtRetournerMoyenneTempsExecution(()=>
      this.listeReduiteClasseAccumulation=liste.reduce(reducerUtilisantClasseDAccumulation, {date: null, forecasts: []}).forecasts);
    });
  }

  private executerNFoisEtRetournerMoyenneTempsExecution(fonctionAInvoquer, nbrExecutions: number = 100): number {
    let tempsExecutionTotal = 0;
    for (let i = 0; i < nbrExecutions; i++) {
      tempsExecutionTotal += this.mesurerTempsExecution(fonctionAInvoquer);
    }
    return tempsExecutionTotal / nbrExecutions;
  }

  private mesurerTempsExecution(fonctionAInvoquer): number {
    const tempsAvant = performance.now();
    fonctionAInvoquer();
    const tempsApres = performance.now();
    return tempsApres - tempsAvant;
  }



  
  title = 'WeatherApp';
}
