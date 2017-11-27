import { Component, Inject, OnInit } from '@angular/core';
import { Http } from '@angular/http';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { Subscription } from 'rxjs/Subscription';
import { BasketService } from '../../services/basket.service';
import { Product } from '../../models/product';


@Component({
    selector: 'product',
    templateUrl: './product.component.html'    
})
export class ProductComponent implements OnInit {
    ngOnInit() {
        this.route.queryParams.subscribe(
            (queryParam: any) => {
                this.id = queryParam['id'];
                this.getProduct();
            }
        );
    }

    public product = <Product>{};
    public id: number;
    private _http: Http;
    private _baseUrl: string;
    private route: ActivatedRoute;
    private basketService: BasketService;
    
    constructor(http: Http, @Inject('BASE_URL') baseUrl: string, route: ActivatedRoute, basket: BasketService) {
        //this.subscription = route.params.subscribe(params => this.id = params['id']);       
        this._http = http;
        this._baseUrl = baseUrl;
        this.route = route;
        this.basketService = basket;
        
    }   

    getProduct() {
        this._http.get(this._baseUrl + 'api/SampleData/GetProduct?productId=' + this.id)
            .subscribe(result => {
                this.product = result.json() as Product;                  
            },
            error => console.error(error)
        );        
    }   

    addToBasket() {       
        this.basketService.addProduct(this.product.id, this.product.name, this.product.description, this.product.price);
    }

}
