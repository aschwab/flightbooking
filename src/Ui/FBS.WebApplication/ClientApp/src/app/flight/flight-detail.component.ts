import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { BookingReadService, FlightAggregate, BookingAggregate, Seat } from '../services/booking-read.service';
import { BookingWriteService, BookFlightCommand } from '../services/booking-write.service';
import { AuthorizedService } from '../services/authorized.service';
import { NotificationService } from '../services/notification.service';

@Component({
  selector: 'app-flight-detail',
  templateUrl: './flight-detail.component.html'
})
export class FlightDetailComponent implements OnInit {
  id;
  flight: FlightAggregate;
  bookings: BookingAggregate[];
  displayedColumns: string[] = ['bookingNumber', 'state', 'seatNumber'];

  constructor(private route: ActivatedRoute, private router: Router, private bookingReadService: BookingReadService, 
    private bookingWriteService: BookingWriteService, private authorizedService: AuthorizedService,
    private notificationService: NotificationService) { }

  ngOnInit() {
    this.id = this.route.snapshot.paramMap.get('id');
    this.loadDetails();

    this.authorizedService.loggedIn.subscribe(res => 
    {
      this.loadDetails();
    });
  }

  loadDetails()
  {
    if(this.id != null)
    {
      this.bookingReadService.getFlightByIdQuery(this.id).subscribe(flight => {
        this.flight = flight;

        //load bookings
        if(this.authorizedService.isAuthorized())
        {
          let customerId = this.authorizedService.getCustomerId();
          this.bookingReadService.getBookingsByFlightQuery(this.id, customerId).subscribe(bookings => {
            if(bookings.length > 0)
            {
              this.bookings = bookings;
            }
          });
        }
        else
        {
          this.bookings = null;
        }
      })
    }
  }

  isSoldOut() : boolean
  {
    return this.flight.seats.every(seat => seat.isOccupied);
  }

  bookFlight(seat: Seat)
  {
    if(this.authorizedService.isAuthorized())
    {
      let customerId = this.authorizedService.getCustomerId();
      this.bookingWriteService.bookFlightCommand(new BookFlightCommand({ id: this.id, customerId: customerId, seatNumber: seat.number }))
        .subscribe(res => {
          this.loadDetails();
          this.notificationService.notify("Booking has been requested, you probably need to refresh the page to see the current status!", "Refresh")
            .subscribe(res => {
              if(res.dismissedByAction)
              {
                this.loadDetails();
              }
            });
        }, (error) => this.notificationService.notify("Something happend while trying to book your flight, please try again!"))
    }
    else
    {
      this.notificationService.notify("You need to be logged in to request a booking!", "Login").subscribe(res => {
        if(res.dismissedByAction)
        {
          this.router.navigateByUrl("/login");
        }
      });
    }
  }
}
