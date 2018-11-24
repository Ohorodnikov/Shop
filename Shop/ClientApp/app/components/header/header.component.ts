import { Component, Inject, OnInit } from '@angular/core';
import { Http } from '@angular/http';
import { BasketService } from '../../services/basket.service';
import { Router, ActivatedRoute, Params } from '@angular/router';

@Component({
    selector: 'my-header',
    templateUrl: './header.component.html'   

})
export class HeaderComponent {    
    public productCount: number;
    public searchCriteria: string;
    public email = "";
    public password = "";
    public confirmPassword = "";
    public error = "";

    private _http: Http;
    private _baseUrl: string;
    public result: string;
    private _route: Router;

    constructor(http: Http, @Inject('BASE_URL') baseUrl: string, basket: BasketService, route: Router) {
        this._http = http;
        this._baseUrl = baseUrl;
        this._route = route;
        http.get(baseUrl + 'api/SampleData/Categories')
            .subscribe(result => {
               
            }, error => console.log(error));
        basket.productsCount.subscribe(val => {
            this.productCount = val;
        });
    }

    register() {
        this.error = "";
        this.result = "";
        if (this.password != this.confirmPassword) {
            this.error = "Passwords are not equal!";
            return;
        }
        console.log("reg");
        this._http.get(this._baseUrl + 'api/admin/register?email=' + this.email + '&password=' + this.password)
            .subscribe(
                result => {
                    var success = result.json() as boolean;
                    if (success) {
                        this.result = "You have been successfully registred!";
                        //this._route.navigate(["/admin"]);
                    }
                    else {
                        this.result = "Error";
                    }
                }
            );
    }

    login() {
        this.result = "";
        this.error = "";
        this._http.get(this._baseUrl + 'api/admin/login?email=' + this.email + '&password=' + this.password)
            .subscribe(
                result => {
                    var roles = result.json() as string[];
                    var rolesAsSrting = roles.toString();
                    if (rolesAsSrting.includes("Admin")) {                        
                        this._route.navigate(["/admin"]);
                    }
                    else if (rolesAsSrting.includes("User")) {
                        this.result = "You are log in";
                    } 
                    else {
                        this.result = "Error";
                    }
                }
            );
    }
}