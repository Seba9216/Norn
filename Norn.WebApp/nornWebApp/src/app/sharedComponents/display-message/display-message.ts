import { CommonModule } from '@angular/common';
import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatInputModule } from '@angular/material/input';

@Component({
  selector: 'app-display-message',
  imports: [CommonModule, MatInputModule],
  templateUrl: './display-message.html',
})
export class DisplayMessage {
  constructor(@Inject(MAT_DIALOG_DATA) public messageToDisplay: string) {}
}
