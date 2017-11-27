import { Component, Input, Inject, OnInit, EventEmitter, Output } from '@angular/core';
import { Http } from '@angular/http';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { Subscription } from 'rxjs/Subscription';

@Component({
    selector: 'main-info',
    templateUrl: './maininfo.component.html'
})
export class MainInfoComponent
{
    @Output() addToBasket = new EventEmitter();
    @Input() name: string;
    @Input() price: string;

    add() {
        this.addToBasket.emit();
    }
}