import { Injectable, EventEmitter } from '@angular/core';
import { AuthorizationService } from './authorization.service';
import { Md5 } from 'ts-md5';
import { CustomerService, Customer } from './customer.service';

const AUHTORIZED_CUSTOMER_ID = "CustomerId";

@Injectable({
  providedIn: 'root'
})
export class AuthorizedService {

  loggedIn: EventEmitter<any>;

  constructor(private authorizationService: AuthorizationService, private md5: Md5) 
  { 
    this.loggedIn = new EventEmitter<any>();
  }

  authorize(userName: string, password: string)
  {
    if(!this.isAuthorized())
    {
      let hashedPassword = Md5.hashStr(password);
      this.authorizationService.authorize(userName, hashedPassword.toString()).subscribe(res => {

        if(res != null)
        {
          sessionStorage.setItem(AUHTORIZED_CUSTOMER_ID, res);
          this.loggedIn.emit(true);
        }
        else
        {
          this.loggedIn.emit(null);
        }
      });
    }
  }

  logout()
  {
    sessionStorage.setItem(AUHTORIZED_CUSTOMER_ID, null);
    this.loggedIn.emit(false);
  }

  isAuthorized() : boolean
  {
    return this.getCustomerId() != null && this.getCustomerId() != "null";
  }

  getCustomerId()
  {
    return sessionStorage.getItem(AUHTORIZED_CUSTOMER_ID);
  }
}
