import { Component, Inject, OnInit } from '@angular/core';
import { Http } from '@angular/http';
import { BasketService } from '../../services/basket.service';

@Component({
    selector: 'my-header',
    templateUrl: './header.component.html'   

})
export class HeaderComponent {    
    public productCount: number;
    constructor(http: Http, @Inject('BASE_URL') baseUrl: string, basket: BasketService) {
        http.get(baseUrl + 'api/SampleData/Categories')
            .subscribe(result => {
               
            }, error => console.log(error));
        basket.productsCount.subscribe(val => {
            this.productCount = val;
        });
    }
}