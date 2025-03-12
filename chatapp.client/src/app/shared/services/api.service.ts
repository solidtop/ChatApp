import { HttpClient, HttpHeaders } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export abstract class ApiService {
  protected readonly http = inject(HttpClient);
  protected readonly apiUrl = environment.apiUrl;
  protected readonly headers = new HttpHeaders({
    'Content-Type': 'application/json',
  });
}
