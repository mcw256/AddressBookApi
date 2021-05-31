import { Injectable } from '@angular/core';
import { Address } from './address.model';
import { HttpClient} from '@angular/common/http'
import { ToastrService } from 'ngx-toastr';
import { PageOfAddresses } from './page-of-addresses.model';

@Injectable({
  providedIn: 'root'
})
export class AddressService {

  constructor(private http:HttpClient, private toastr:ToastrService) { }

  formData: Address = new Address();
  list: Address[] = [];
  pageOfAddresses: PageOfAddresses;

  readonly baseURL = 'https://localhost:44332/api/Address'
  
  postAddress(){
    return this.http.post(this.baseURL, this.formData)
  }

  putAddress(){
    return this.http.put(`${this.baseURL}/${this.formData.id}`, this.formData)
  }

  refreshList(){
    this.http.get<PageOfAddresses>(this.baseURL).subscribe( (returnedValue) => {this.pageOfAddresses = returnedValue } )

  }

  deleteAddress(id: string) {
    return this.http.delete(`${this.baseURL}/${id}`);
  }
  
}
