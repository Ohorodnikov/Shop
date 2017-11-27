import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';

@Component({
    selector: 'category-menu',
    templateUrl: './categorymenu.component.html',
    styleUrls: ['./categorymenu.component.css']

})
export class CategoryMenuComponent {
    public menu: Menu[];

    constructor(http: Http, @Inject('BASE_URL') baseUrl: string) {
        http.get(baseUrl + 'api/SampleData/Categories')
            .subscribe(result => {
                this.menu = result.json() as Menu[];
            }, error => console.log(error));
    }
}

interface Menu {
    name: string
}