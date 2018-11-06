import { Component, OnInit } from '@angular/core';
import { WeatherService } from './weather.service';
import { Forecast } from './model/OpenWeatherMap';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  listeReduiteArrayFind: Forecast[];
  listeReduiteAssociativeArray: Forecast[];
  listeOriginale: Forecast[];
  //objectKeys est utilisé pour itérer sur l'associative array par ses clés. Voir template.
  private objectKeys = Object.keys;
  private tempsExecutionArrayFind: number;
  private tempsExecutionAssociativeArray: number;
  constructor(private service: WeatherService) {

  }
  ngOnInit(): void {

    const reducerUtilisantFind = (accumulator: Array<Forecast>, currentValue: Forecast) => {
      const clePrevisionActuelle = this.obtenirCleDate(this.convertirUnixTimeEnDate(currentValue.dt));
      if (!accumulator.find(previsionDejaDansAccumulateur => this.obtenirCleDate(new Date(this.convertirUnixTimeEnDate(previsionDejaDansAccumulateur.dt))) === clePrevisionActuelle)) {
        accumulator.push(currentValue);
      }
      return accumulator;
    };

    const reducerUtilisantAssociativeArray = (accumulator: any, previsionActuelle: Forecast) => {
      const clePrevisionActuelle = this.obtenirCleDate(this.convertirUnixTimeEnDate(previsionActuelle.dt));
      if (!accumulator[clePrevisionActuelle]) {
        accumulator[clePrevisionActuelle] = previsionActuelle;
      }
      return accumulator;
    }

    this.service.chargerPrevisionsMeteo().subscribe(s => {
      this.listeOriginale = s.list;
      this.tempsExecutionArrayFind = this.executerNFoisEtRetournerMoyenneTempsExecution(() => this.listeReduiteArrayFind = s.list.reduce(reducerUtilisantFind, []));
      this.tempsExecutionAssociativeArray = this.executerNFoisEtRetournerMoyenneTempsExecution(() => this.listeReduiteAssociativeArray = s.list.reduce(reducerUtilisantAssociativeArray, {}));
    });
  }

  private executerNFoisEtRetournerMoyenneTempsExecution(fonctionAInvoquer, nbrExecutions: number = 10): number{
    let tempsExecutionTotal=0;
    for(let i=0;i<nbrExecutions;i++){
      tempsExecutionTotal+=this.mesurerTempsExecution(fonctionAInvoquer);
    }
    return tempsExecutionTotal/nbrExecutions;
  }

  private mesurerTempsExecution(fonctionAInvoquer): number {
    const tempsAvant = performance.now();
    fonctionAInvoquer();
    const tempsApres = performance.now();
    return tempsApres - tempsAvant;
  }



  private obtenirCleDate(date: Date): string {
    return date.toISOString().split('T')[0];
  }

  private convertirUnixTimeEnDate(unixTime: number): Date {
    return new Date(unixTime * 1000);
  }
  title = 'WeatherApp';
}
