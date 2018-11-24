import { Component, Inject, OnInit } from '@angular/core';
import { Http } from '@angular/http';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { Subscription } from 'rxjs/Subscription';
import { Purchase } from "../../models/purchase";
import { PurchaseProduct } from "../../models/purchaseProduct";

@Component({
    selector: 'purchaseinfo',
    templateUrl: './purchaseinfo.component.html'
})
export class PurchaseInfoComponent {
    public id: number;
    private _http: Http;
    private _baseUrl: string;

    public purchase: Purchase;

    constructor(http: Http, @Inject('BASE_URL') baseUrl: string, route: ActivatedRoute) {
        //this.subscription = route.params.subscribe(params => this.id = params['id']);       
        console.log("Admin purchase Constructor");
        this._http = http;
        this._baseUrl = baseUrl;
        route.queryParams.subscribe(
            (queryParam: any) => {
                this.id = queryParam['id'];
                //this.currPage = queryParam['page'];
                this.getPurchase();
            }
        );      
       
    }

    getPurchase() {
        this._http.get(this._baseUrl + "api/Admin/GetPurchase?purchaseId=" + this.id)
            .subscribe(result => {
                console.log("get purhase");
                console.log(result);
                this.purchase = result.json() as Purchase;
                console.log(this.purchase);
            });
    }

}
