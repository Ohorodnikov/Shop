import { Http } from '@angular/http';
import { Product } from '../../models/product';
import { ActivatedRoute } from "@angular/router";
import 'rxjs/add/operator/toPromise';
import { Component, Input, Inject, OnInit, EventEmitter, Output } from '@angular/core';
import { Comment } from '../../models/comment';

@Component({
    selector: 'discuss',
    templateUrl: './discuss.component.html'

})
export class DiscussComponent implements OnInit {
    async ngOnInit() {
        this.getAllComments();

    }

    @Input() productId: string;

    private _http: Http;
    private _baseUrl: string;
    private route: ActivatedRoute;

    public message: string;
    public error: string;

    public comments: Comment[];

    constructor(http: Http, @Inject('BASE_URL') baseUrl: string, route: ActivatedRoute) {
        this._http = http;
        this._baseUrl = baseUrl;
        this.route = route;
    }

    getAllComments() {
        console.log(this._baseUrl + 'api/SampleData/getcomments?productId=' + this.productId);
        
        this._http.get(this._baseUrl + 'api/SampleData/getcomments?productId=' + this.productId).subscribe(result => {
            this.comments = result.json() as Comment[];              
        });        
    }

    addComment() {
        this.error = "";
        
        this._http.get(this._baseUrl + 'api/SampleData/addcomment?productId=' + this.productId + "&message=" + this.message)
            .subscribe(result => {
                var ans = result.text();                
                if (ans.includes("ok")) {
                    this.getAllComments();
                } else {
                    this.error = ans;
                }
        });
    }

    //async getProduct(id: number) {
    //    let response = await this._http.get(this._baseUrl + 'api/SampleData/GetProduct?productId=' + id).toPromise();
    //    this.product = response.json() as Product;
    //    console.log("getPtoduct");
    //    console.log(this.product);
    //    //.subscribe(result => {
    //    //    this.product = result.json() as Product;
    //    //    console.log("getPtoduct");
    //    //    console.log(this.product);
    //    //},
    //    //error => console.error(error)
    //    //);
    //}
}