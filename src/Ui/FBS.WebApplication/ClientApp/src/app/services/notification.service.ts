import { Injectable, Injector } from '@angular/core';
import { Observable } from 'rxjs';
import { Overlay } from '@angular/cdk/overlay';
import { MatSnackBar, MatSnackBarDismiss, MatSnackBarHorizontalPosition, MatSnackBarVerticalPosition } from '@angular/material';

@Injectable({
    providedIn: 'root'
})
export class NotificationService {

    constructor(private snackBar: MatSnackBar) {
    }

    public notify(message: string, action: string = null, time = 6000): Observable<MatSnackBarDismiss> {
        return this.snackBar.open(message, action, {
            duration: time
        }).afterDismissed();
    }
}    