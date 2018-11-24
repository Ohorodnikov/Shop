import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';
import { BrowserModule } from '@angular/platform-browser';

import { MarkdownModule } from 'angular2-markdown';

import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { CategoryMenuComponent } from './components/categorymenu/categorymenu.component';
import { ProductsComponent } from './components/products/products.component';
import { ProductComponent } from './components/product/product.component';
import { MainInfoComponent } from './components/maininfo/maininfo.component';
import { FullInfoComponent } from './components/fullinfo/fullinfo.component';
import { HeaderComponent } from './components/header/header.component';
import { BasketComponent } from './components/basket/basket.component';
import { PurchaseComponent } from './components/purchase/purchase.component';
import { SearchComponent } from './components/search/search.component';
import { AdminMainComponent } from './components/admin_main/adminmain.component';
import { PurchaseInfoComponent } from './components/admin_purchaseinfo/purchaseinfo.component';
import { DiscussComponent } from './components/discuss/discuss.component';
import { CommentComponent } from './components/comment/comment.component';

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        HomeComponent,
        CategoryMenuComponent,
        ProductsComponent,
        ProductComponent,
        MainInfoComponent,
        FullInfoComponent,
        HeaderComponent,
        BasketComponent,
        PurchaseComponent,
        SearchComponent,
        AdminMainComponent,
        PurchaseInfoComponent,
        DiscussComponent,
        CommentComponent
    ],
    imports: [
        BrowserModule,
        MarkdownModule,
        CommonModule,
        HttpModule,
        FormsModule,
        
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'admin', component: AdminMainComponent },
            { path: 'admin/home', redirectTo: 'admin' },
            { path: 'admin/purchase', component: PurchaseInfoComponent},
            { path: 'home', component: HomeComponent },
            { path: 'products', component: ProductsComponent },
            { path: 'product', component: ProductComponent },
            { path: 'basket', component: BasketComponent },
            { path: 'purchase', component: PurchaseComponent },
            { path: 'search', component: SearchComponent},
            { path: '**', redirectTo: 'home' }
        ])
    ]
})
export class AppModuleShared {
}
