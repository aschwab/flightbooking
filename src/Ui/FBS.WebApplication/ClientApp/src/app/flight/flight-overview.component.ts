import { Component, OnInit } from '@angular/core';
import { FlightControlService, Flight } from '../services/flightcontrol.service';
import { FlightAggregate, BookingReadService, GetAllFlightsQuery } from '../services/booking-read.service';
import { ReleaseFlightCommand, Location, BookingWriteService } from '../services/booking-write.service';
import { NotificationService } from '../services/notification.service';

@Component({
  selector: 'app-flight-overview',
  templateUrl: './flight-overview.component.html',
})
export class FlightOverviewComponent implements OnInit {
  
  flights: FlightAggregate[];
  progressValue: number = 0;
  
  constructor(private flightControlService: FlightControlService, private bookingWriteService: BookingWriteService,
    private bookingReadService: BookingReadService, private notificationService: NotificationService)
  {
  }

  ngOnInit(): void {
    this.loadDetails();
  }

  loadDetails()
  {
    this.bookingReadService.getAllFlightsQuery(new GetAllFlightsQuery())
    .subscribe(flights => {    
      this.flights = flights.filter(f => f.from != null);
  });
  }

  releaseFlights()
  {
    //generate booking flights from flightcontrol flights
    this.flightControlService.flightAll().subscribe(flights => {

      if(flights != null)
      {
        let releaseFlightCommands = flights.map(flight => {

          let releaseFlightCommand = new ReleaseFlightCommand();
          releaseFlightCommand.id = flight.id;
          releaseFlightCommand.capacity = flight.plane.capacity;
          releaseFlightCommand.planeModel = flight.plane.model;
          releaseFlightCommand.date = flight.date;
          releaseFlightCommand.gate = flight.gate;
          releaseFlightCommand.duration = flight.duration;
          releaseFlightCommand.number = flight.number;

          releaseFlightCommand.from = new Location({
            code: flight.from.code,
            name: flight.from.name
          });

          releaseFlightCommand.to = new Location({
            code: flight.to.code,
            name: flight.to.name
          });

          return releaseFlightCommand;
        });

        releaseFlightCommands.forEach(cmd => {
          this.bookingWriteService.releaseFlightCommand(cmd).subscribe(res => {
            this.progressValue += (101 / releaseFlightCommands.length);
            console.log(this.progressValue);

            if(this.progressValue >= 100)
            {
              //completely reload component
              this.ngOnInit();
            }
          }, (error) => this.notificationService.notify("Could not release Flight " + cmd.number + ", please try again!"));
        });

      }
    }, (error) => this.notificationService.notify("There was an error reaching the FlightControl API."));
  }
}
