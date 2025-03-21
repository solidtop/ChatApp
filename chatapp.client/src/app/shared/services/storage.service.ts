import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class StorageService {
  getLocal<T>(key: string) {
    return JSON.parse(localStorage.getItem(key) as string) as T;
  }

  setLocal(key: string, value: Object) {
    localStorage.setItem(key, JSON.stringify(value));
  }

  getSession<T>(key: string) {
    return JSON.parse(sessionStorage.getItem(key) as string) as T;
  }

  setSession(key: string, value: Object) {
    sessionStorage.setItem(key, JSON.stringify(value));
  }
}