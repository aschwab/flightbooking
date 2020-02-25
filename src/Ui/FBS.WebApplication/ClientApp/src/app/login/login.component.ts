import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { AuthorizedService } from '../services/authorized.service';
import { Router } from '@angular/router';
import { NotificationService } from '../services/notification.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html'
})
export class LoginComponent implements OnInit {

  form: FormGroup = new FormGroup({
    username: new FormControl(''),
    password: new FormControl(''),
  });

  constructor(private authorizedService: AuthorizedService, private router: Router, private notificationService: NotificationService)
  {

  }
  
  ngOnInit() {
    if(this.authorizedService.isAuthorized())
    {
      this.router.navigateByUrl("/");
    }

    this.authorizedService.loggedIn.subscribe(res => {
      if(res)
      {
        //login was successful
        this.notificationService.notify("Successfully logged in!");
        this.router.navigateByUrl("/");
      }
      else if(res == null)
      {
        this.notificationService.notify("Login unsuccessful, please try again!");
      }
    });
  }

  submit() {
    if (this.form.valid) {
      this.authorizedService.authorize(this.form.get("username").value, this.form.get("password").value);
    }
    else
    {
      this.notificationService.notify("Input is incorrect, please try again!");
    }
  }
}