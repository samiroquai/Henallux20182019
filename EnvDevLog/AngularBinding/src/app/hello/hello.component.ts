import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-hello',
  templateUrl: './hello.component.html',
  styleUrls: ['./hello.component.css']
})
export class HelloComponent implements OnInit {

  private styleToApply='style2';
  mustApplyStyle1=false;
  mustApplyStyle2=false;
  helloMessage:string;
  constructor() { }

  ngOnInit() {
  }

  changeStyleToApply(newStyle: string){
    this.styleToApply=newStyle;
    this.mustApplyStyle1=newStyle==='style1';
    this.mustApplyStyle2=newStyle==='style2';
    console.log("new style: "+newStyle);
  }

}
