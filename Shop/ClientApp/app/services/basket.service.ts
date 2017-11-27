import { Product } from '../models/product';
import { Injectable, Component, Directive } from '@angular/core';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import 'rxjs/Rx';

@Injectable()
export class BasketService {

    private data: Product[] = new Array();

    public productsCount: BehaviorSubject<number> = new BehaviorSubject<number>(0);

    addProduct(id: number, name: string, description: string, price: string) {
        console.log(this.data);
        this.data.push(new Product(id, name, description, price));
        this.productsCount.next(this.data.length);

    }

    getAllProducts(): Product[] {        
        return this.data;
    }
}