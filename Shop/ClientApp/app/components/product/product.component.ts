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

    public product = <Product>{};
    public _id: number;
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

    ngOnInit() {
        this.route.queryParams.subscribe(
            (queryParam: any) => {
                this._id = queryParam['id'];
                this.getProduct();
                
            }
        );
    }

    getProduct() {
        this._http.get(this._baseUrl + 'api/SampleData/GetProduct?productId=' + this._id)
            .subscribe(result => {
                this.product = result.json() as Product; 
                console.log(this.product);
            },
            error => console.error(error)
        );        
    }   

    addToBasket() {     
        console.log("Add to basket: " + this._id);
        this.basketService.addProduct(this.product.id);
    }

}
