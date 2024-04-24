import { Component, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { NavigationEnd, Route, Router, RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  // standalone: true,
  // imports: [RouterOutlet],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit,OnChanges{
  title = 'Final_V2';
  LoggedIn:boolean = true;
  NotAlreadyLogged:boolean=true;
  

  constructor(private router: Router) {
    this.checkLocalStorage();
  }

  ngOnInit() {
    this.updateLoggedInState(); 
    this.router.events.subscribe((event) => {
      if (event instanceof NavigationEnd) {
        this.updateLoggedInState();
        this.checkLocalStorage();
      }
    });
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['NotAlreadyLogged']) {
      console.log('NotAlreadyLogged changed:', this.NotAlreadyLogged);
    }
  }

  private checkLocalStorage() {
    this.NotAlreadyLogged = localStorage.getItem('token') === null;
  }

  public LogOut(){
    localStorage.removeItem("token");
    this.router.navigate(['/']);
  }

  private updateLoggedInState() {
    this.LoggedIn = this.router.url === '/';
  }

  public SignUpClicked() {
    this.LoggedIn = false;
  }

  public LogInClicked() {
    this.LoggedIn = true;
  }
}
