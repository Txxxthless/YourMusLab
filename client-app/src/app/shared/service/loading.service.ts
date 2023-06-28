import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class LoadingService {
  isLoading = new BehaviorSubject<boolean>(false);

  constructor() {}

  beginLoading() {
    if (!this.isLoading.value) {
      this.isLoading.next(true);
    }
  }

  endLoading() {
    if (this.isLoading.value) {
      this.isLoading.next(false);
    }
  }
}
