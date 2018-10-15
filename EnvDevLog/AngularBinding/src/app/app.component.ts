import { Component } from '@angular/core';
import { Client } from './model/client';
import { Address } from './model/adress';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'IG3';
  client: Client;

  /**
   *
   */
  constructor() {
    
    this.client=new Client();

    this.client.fullName="John Doe";
    this.client.shippingAddress=new Address();
    this.client.shippingAddress.city="Arlon";
    this.client.shippingAddress.postalCode="6700";
    this.client.shippingAddress.street="Rue des cerisiers, 16";

    this.client.invoicingAddress=new Address();
    this.client.invoicingAddress.city="Arlon";
    this.client.invoicingAddress.postalCode="6700";
    this.client.invoicingAddress.street="Rue des cerisiers, 16";


  }
}
