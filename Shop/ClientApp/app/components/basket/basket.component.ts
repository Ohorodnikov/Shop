import { Component, Inject, OnInit } from '@angular/core';
import { Http } from '@angular/http';
import { BasketService } from '../../services/basket.service';
import { Product } from '../../models/product';
import { ActivatedRoute } from "@angular/router";
import 'rxjs/add/operator/toPromise';

declare var $: any;
declare var paypal: any;
declare var braintree: any;
@Component({
    selector: 'basket',
    templateUrl: './basket.component.html'

})
export class BasketComponent implements OnInit {
    async ngOnInit() {        
        this.basketService.getProducts();  
        this.basketService.products.subscribe(data => this.products = data);
        console.log("basket:");
        console.log(this.products);
        await this.getPaypalToken();
        console.log("have token");        
    }

    public products: Product[] = new Array();
    public paypalToken: string;

    private _http: Http;
    private _baseUrl: string;
    private route: ActivatedRoute;
    private basketService: BasketService;
    private product = <Product>{};

    constructor(http: Http, @Inject('BASE_URL') baseUrl: string, route: ActivatedRoute, basket: BasketService) {
        this._http = http;
        this._baseUrl = baseUrl;
        this.route = route;
        this.basketService = basket;        
    }       

    removeOne(id: number) {
        console.log(id);
        this.basketService.removeProductOneItem(id);
    }

    addOne(id: number) {
        this.basketService.addProduct(id);
    }

    removeAll(id: number) {
        this.basketService.removeProductAllItems(id);
    }

    async getPaypalToken() {
        let response = await this._http.get(this._baseUrl + 'api/SampleData/GetToken').toPromise();  
        console.log("no token");
        this.paypalToken = response.text();
    }

    async getProduct(id: number) {
        let response = await this._http.get(this._baseUrl + 'api/SampleData/GetProduct?productId=' + id).toPromise();
        this.product = response.json() as Product;
        console.log("getPtoduct");
        console.log(this.product);
            //.subscribe(result => {
            //    this.product = result.json() as Product;
            //    console.log("getPtoduct");
            //    console.log(this.product);
            //},
            //error => console.error(error)
            //);
    }  
}