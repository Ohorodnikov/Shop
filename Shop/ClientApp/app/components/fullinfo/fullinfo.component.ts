import { Component, Input, Inject, OnInit } from '@angular/core';
import { Http } from '@angular/http';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { Subscription } from 'rxjs/Subscription';

@Component({
    selector: 'full-info',
    templateUrl: './fullinfo.component.html'
})
export class FullInfoComponent {
    @Input() name: string;
    @Input() description: string;
}