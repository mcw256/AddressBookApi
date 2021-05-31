import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Address } from '../shared/address.model';
import { AddressService } from '../shared/address.service';

@Component({
  selector: 'app-address',
  templateUrl: './address.component.html',
  styles: [
  ]
})
export class AddressComponent implements OnInit {

  constructor(public service: AddressService, private toastr: ToastrService) { }

  ngOnInit(): void {
    this.service.refreshList();
  }

  populateForm(selectedRecord: Address){
    this.service.formData = Object.assign({}, selectedRecord);

  }

  onDelete(id: string) {
      this.service.deleteAddress(id)
        .subscribe(
          res => {
            this.service.refreshList();
            this.toastr.error("Deleted successfully");
          },
          err => { console.log(err) }
        )
    
  }

}
