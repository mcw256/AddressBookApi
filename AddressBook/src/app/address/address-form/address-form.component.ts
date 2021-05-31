import { Component, OnInit } from '@angular/core';
import { AddressService } from 'src/app/shared/address.service';
import { NgForm } from '@angular/forms'
import { Address } from 'src/app/shared/address.model';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-address-form',
  templateUrl: './address-form.component.html',
  styles: [
  ]
})
export class AddressFormComponent implements OnInit {

  constructor(public service: AddressService, private toastr:ToastrService) { }

  ngOnInit(): void {
  }

  onSubmit(form: NgForm){
    if(this.service.formData.id == '00000000-0000-0000-0000-000000000000')
    this.insertRecord(form);
    else
    this.updateReocrd(form);

  }

  insertRecord(form: NgForm){
    this.service.postAddress().subscribe(
      res => {
          this.resetForm(form);
          this.service.refreshList();
          this.toastr.success('Added new address');
      },
      err => { console.log(err);

      });
  }

  updateReocrd(form: NgForm){
    this.service.putAddress().subscribe(
      res => {
          this.resetForm(form);
          this.service.refreshList();
          this.toastr.info('Updated address');
      },
      err => { console.log(err);

      });

  }

  resetForm(form: NgForm)
  {
    form.form.reset();
    this.service.formData = new Address();
  }

}
