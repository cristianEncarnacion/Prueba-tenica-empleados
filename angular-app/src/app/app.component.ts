import { Component, inject } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { EmpleadoApiService } from './empleado-api.service';
@Component({
  selector: 'app-root',
  // imports: [RouterOutlet],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'angular-app';
  empleadoApi:any[]=[];
  empleadoService=inject(EmpleadoApiService);

  constructor(){
  this.empleadoService.get().subscribe(empleado=>{
    this.empleadoApi=empleado;
    console.log(empleado);
  });
  }
}
