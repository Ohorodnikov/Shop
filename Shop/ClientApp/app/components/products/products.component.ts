import { Component, Inject, OnInit } from '@angular/core';
import { Http } from '@angular/http';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { Subscription } from 'rxjs/Subscription';

@Component({
    selector: 'products',
    templateUrl: './products.component.html'
})
export class ProductsComponent {
    
    public products: Product[];
    public id: number;
    private _http: Http;
    private _baseUrl: string;
    private sortedById: boolean;
    private currPage: number = 1;

    constructor(http: Http, @Inject('BASE_URL') baseUrl: string, route: ActivatedRoute)
    {            
        //this.subscription = route.params.subscribe(params => this.id = params['id']);       
        console.log("Constructor");
        this._http = http;
        this._baseUrl = baseUrl;
        route.queryParams.subscribe(
            (queryParam: any) => {
                this.id = queryParam['id'];
                this.currPage = queryParam['page'];
                this.getProducts();
            }
        );              
    }

    getProducts() {
        this._http.get(this._baseUrl + 'api/SampleData/Products?categoryId=' + this.id + '&page=' + this.currPage + '&itemsPerPage=' + 9)
            .subscribe(result => {
                this.products = result.json() as Product[];

                this.products.sort((p1, p2) =>
                {
                    let name1 = p1.name;
                    let name2 = p2.name;
                    let result = name1.localeCompare(name2);
                    this.sortedById = false;
                    return result;
                });
            },
            error => console.error(error)
            ); 
    }

    nextPage() {
        if (this.products.length == 9) {
            this.currPage++;
            this.getProducts();
        }     
    }

    prevPage() {
        if (this.currPage != 1) {
            this.currPage--;
            this.getProducts();
        }
        
    }

    sort() {
        console.log("Sort");
        if (this.sortedById) {
            this.products.sort((p1, p2) => {
                let name1 = p1.name;
                let name2 = p2.name;
                let result = name1.localeCompare(name2);
                this.sortedById = false;
                return result;
            });
        }
        else {
            this.products.sort((p1, p2) => {
                this.sortedById = true;
                return p1.id - p2.id;
            });
        }
        
    }

}

interface Product {
    id: number;
    name: string;
    description: string;
    price: string;
    
}
