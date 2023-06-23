import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class LoadingService {
  isLoading = new BehaviorSubject<boolean>(false);

  constructor() {}

  beginLoading() {
    this.isLoading.next(true);
  }

  endLoading() {
    this.isLoading.next(false);
  }
}
