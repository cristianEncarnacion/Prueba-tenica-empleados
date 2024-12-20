import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../environments/environment';
import { Observable } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class EmpleadoApiService {

  constructor() { }
  private http=inject(HttpClient);
  private apiUrl=environment.apiUrl + "/api/Empleado";

  public get(): Observable<any>{
    return this.http.get(this.apiUrl);
  }
}
