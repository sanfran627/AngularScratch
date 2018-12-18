import { Component, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ActivatedRoute, ActivatedRouteSnapshot } from "@angular/router";

@Component({
  selector: 'app-list-users',
  templateUrl: './list-users.component.html'
})
export class ListUsersComponent {
  public users: User[];
  public error: string;
  private _route: ActivatedRoute;
  private _http: HttpClient;
  private _baseUrl: string;

  constructor(private route: ActivatedRoute, http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this._route = route;
    this._http = http;
    this._baseUrl = baseUrl
  }

  ngOnInit() {
    const add: string = this.route.snapshot.queryParamMap.get('add');
    this._http.get<User[]>(this._baseUrl + `api/SampleData/users?add=${add}`).subscribe(result => {
      this.users = result;
    }, err => {
      console.log(err);
      this.error = err.message
    });
  }
}

interface User {
  userId: string;
  firstName: string;
  lastName: string;
  title: string;
  age: number;
}
