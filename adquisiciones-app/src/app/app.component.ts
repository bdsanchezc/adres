import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { LoaderComponent } from "./shared/components/loader/loader.component";
import { ModalComponent } from "./shared/components/modal/modal.component";

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, LoaderComponent, ModalComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'adquisiciones-app';
}
