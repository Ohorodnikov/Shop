import { Component, Inject, OnInit } from '@angular/core';
import { Http } from '@angular/http';
import { BasketService } from '../../services/basket.service';
import { Product } from '../../models/product';
import { ActivatedRoute } from "@angular/router";
import 'rxjs/add/operator/toPromise';

@Component({
    selector: 'basket',
    templateUrl: './basket.component.html'

})
export class BasketComponent implements OnInit {
    async ngOnInit() {
        let products = this.basketService.getAllProducts();
        for (let p of products) {
            await this.getProduct(p.id);
            setTimeout(100);
            console.log("product");
            console.log(this.product);
            this.products.push(this.product);            
        }

        console.log(this.products);
    }

    public products: Product[] = new Array();

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