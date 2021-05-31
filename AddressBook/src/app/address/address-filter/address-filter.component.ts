import { Component, OnInit } from '@angular/core';
import { AddressService } from 'src/app/shared/address.service';

@Component({
  selector: 'app-address-filter',
  templateUrl: './address-filter.component.html',
  styleUrls: ['./address-filter.component.css']
})
export class AddressFilterComponent implements OnInit {

  constructor(private service: AddressService ) { }

  onFilterClick(){
    

  }

  ngOnInit(): void {
  }

}
