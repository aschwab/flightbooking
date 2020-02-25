import { Component, OnInit } from '@angular/core';
import { AuthorizedService } from '../services/authorized.service';
import { Customer, CustomerService } from '../services/customer.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit{

  customer: Customer;

  constructor(private authorizedService: AuthorizedService, private customerService: CustomerService)
  {
  }

  ngOnInit(): void {
    if(this.isAuthorized())
    {
      this.loadCustomer();
    }

    this.authorizedService.loggedIn.subscribe(res => {
      if(res)
      {
        this.loadCustomer();
      }
      else
      {
        this.customer = null;
      }
    });
  }

  loadCustomer()
  {
    this.customerService.customer(this.authorizedService.getCustomerId()).subscribe(res =>
      {
        this.customer = res;
      });
  }

  isAuthorized() : boolean
  {
    return this.authorizedService.isAuthorized();
  }

  logout()
  {
    this.authorizedService.logout();
    this.customer = null;
    this.ngOnInit();
  }
}
