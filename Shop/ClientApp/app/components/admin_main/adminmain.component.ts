import { Component, Inject, OnInit } from '@angular/core';
import { Http } from '@angular/http';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { Subscription } from 'rxjs/Subscription';
import { Purchase } from "../../models/purchase";

@Component({
    selector: 'adminmain',
    templateUrl: './adminmain.component.html'
})
export class AdminMainComponent {
    public id: number;
    private _http: Http;
    private _baseUrl: string;
    private _router: Router;

    public purchases: Purchase[];

    constructor(http: Http, @Inject('BASE_URL') baseUrl: string, route: ActivatedRoute, router: Router) {
        //this.subscription = route.params.subscribe(params => this.id = params['id']);       
        console.log("Admin Constructor");
        this._http = http;
        this._baseUrl = baseUrl;
        this._router = router;
        this.getPurchases();
    }

    getPurchases() {
        this._http.get(this._baseUrl + "api/Admin/GetAllPurchases")
            .subscribe(result => {
                console.log(result);
                if (result.statusText == "No Content") {
                    console.log("anauth");
                    this._router.navigate(["/"]);
                }
                this.purchases = result.json() as Purchase[];
            });
    }

}
