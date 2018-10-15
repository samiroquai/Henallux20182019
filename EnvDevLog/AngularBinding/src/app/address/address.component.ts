import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { FormControl, FormBuilder, Validators, FormGroup } from '@angular/forms';
import { Address } from '../model/adress';


@Component({
  selector: 'app-address',
  templateUrl: './address.component.html',
  styleUrls: ['./address.component.css']
})
export class AddressComponent implements OnInit {

  @Input() address: Address;
  @Output() addressChange:  EventEmitter<Address>=new EventEmitter<Address>();
  addressForm: FormGroup;
  constructor() { }

  ngOnInit() {
    this.addressForm=new FormBuilder().group(
      {
        street:new FormControl(null, [Validators.required, Validators.minLength(5)]),
        city: new FormControl(null, [Validators.required]),
        postalCode: new FormControl(null, [Validators.required])
      }
    );
    this.addressForm.patchValue(this.address);
    this.addressForm.valueChanges.subscribe(a=>this.addressChange.emit(this.addressForm.value));
  }

}
