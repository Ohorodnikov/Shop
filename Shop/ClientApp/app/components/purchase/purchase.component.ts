import { Component, Inject, OnInit } from '@angular/core';
import { Http } from '@angular/http';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { Subscription } from 'rxjs/Subscription';
import { BasketService } from '../../services/basket.service';
import { Product } from '../../models/product';


@Component({
    selector: 'purchase',
    templateUrl: './purchase.component.html'
})
export class PurchaseComponent implements OnInit {  

    public paypalToken: string;
    public totalPrice: number = 0;
    public userName: string;

    private _http: Http;
    private _baseUrl: string;
    private route: ActivatedRoute;
    private basketService: BasketService;
    private products: Product[] = new Array();


    constructor(http: Http, @Inject('BASE_URL') baseUrl: string, route: ActivatedRoute, basket: BasketService) {
        this._http = http;
        this._baseUrl = baseUrl;
        this.route = route;
        this.basketService = basket;
    }

    async ngOnInit() {
        await this.getPaypalToken();        

        this.basketService.getProducts();
        this.basketService.products.subscribe(data => this.getPrice(data));
    }

    getPrice(data: Product[]) {
        console.log(data);
        this.products = data; 
        this.totalPrice = 0;
        for (let p of this.products) {
            this.totalPrice += parseFloat(p.price)*p.count;
        }
        console.log("price in int");
        console.log(this.totalPrice);
    }

    async getPaypalToken() {
        let response = await this._http.get(this._baseUrl + 'api/SampleData/GetToken').toPromise();
        this.paypalToken = response.text();
    }
}
