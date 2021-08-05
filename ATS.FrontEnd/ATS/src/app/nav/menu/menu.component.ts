import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html'
})
export class MenuComponent implements OnInit {

  public isCollapse: boolean;

  constructor() { 
    this.isCollapse = true;
  }
  ngOnInit() {
  }

}
