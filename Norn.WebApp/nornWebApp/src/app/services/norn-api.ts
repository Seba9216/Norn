import { HttpClient, HttpHeaders } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class NornApi {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = 'http://localhost:8080';
  get<T>(endpoint: string) {
    return this.http.get<T>(`${this.baseUrl}${endpoint}`);
  }

  post<TResponse, TRequest>(endpoint: string, body: TRequest) {
    return this.http.post<TResponse>(`${this.baseUrl}${endpoint}`, body);
  }

  put<TResponse, TRequest>(endpoint: string, body: TRequest) {
    return this.http.put<TResponse>(`${this.baseUrl}${endpoint}`, body);
  }

  delete<T>(endpoint: string) {
    return this.http.delete<T>(`${this.baseUrl}${endpoint}`);
  }
}
