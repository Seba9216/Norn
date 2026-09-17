import { CommonModule } from '@angular/common';
import { Component, Inject, input } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogContent } from '@angular/material/dialog';
import { MatInputModule } from '@angular/material/input';

@Component({
  selector: 'app-display-message',
  imports: [MatDialogContent,CommonModule,MatInputModule],
  templateUrl: './display-message.html',
})
export class DisplayMessage {
   constructor(
    @Inject(MAT_DIALOG_DATA) public messageToDisplay: string,
  ) {}
}
