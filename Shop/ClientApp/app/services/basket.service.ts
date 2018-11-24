import { Product } from '../models/product';
import { Injectable, Component, Directive, Inject } from '@angular/core';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import 'rxjs/Rx';
import { ActivatedRoute } from "@angular/router";
import { Http } from '@angular/http';
import 'rxjs/add/operator/toPromise';
import { ReplaySubject } from "rxjs/ReplaySubject";

@Injectable()
export class BasketService {

    private _http: Http;
    private _baseUrl: string;
    private route: ActivatedRoute;
    private basketService: BasketService;
    private _products: Product[] = new Array();

    constructor(http: Http, @Inject('BASE_URL') baseUrl: string, route: ActivatedRoute) {
        this._http = http;
        this._baseUrl = baseUrl;
        this.route = route;
    }

    public productsCount: BehaviorSubject<number> = new BehaviorSubject<number>(0);
    public products: ReplaySubject<Product[]> = new ReplaySubject(); 
    
    async getProducts() {
        await this._http.get(this._baseUrl + 'api/SampleData/GetBasket').subscribe(data => {            
            this.products.next(data.json() as Product[]);
        });
    }
    
    async addProduct(id: number) {        
        let path = this._baseUrl + 'api/SampleData/AddProductToBasket?id=' + id;
        console.log(path);
        await this._http.get(path).subscribe(data => {
            this.productsCount.next(data.json() as number);
            this.getProducts();
        });
    }

    async removeProductOneItem(id: number) {
        let path = this._baseUrl + 'api/SampleData/RemoveProductOneItemFromBasket?id=' + id;
        console.log(path);
        await this._http.get(path).subscribe(data => {
            this.productsCount.next(data.json() as number);
            this.getProducts();
        });
    }

    async removeProductAllItems(id: number) {
        let path = this._baseUrl + 'api/SampleData/RemoveProductAllItemsFromBasket?id=' + id;
        await this._http.get(path).subscribe(data => {
            this.productsCount.next(data.json() as number);
            this.getProducts();
        });
    }

    
}