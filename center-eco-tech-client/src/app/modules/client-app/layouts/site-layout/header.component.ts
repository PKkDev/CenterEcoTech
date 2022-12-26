import { Component } from '@angular/core';
import { AuthService } from 'src/app/modules/authorize/services/auth.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent {
  public isMenuOpen = false;

  constructor(private authService: AuthService) {
  }

  toggleMenu(): void {
    this.isMenuOpen = !this.isMenuOpen;
  }

  public logOut() {
    this.authService.logOut();
  }

}
