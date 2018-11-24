import { User } from "./user";
import { Product } from "./product";
import { PurchaseProduct } from "./purchaseProduct";

export class Purchase {
    constructor(public id: number, public dateTime: Date, public totalPrice: number, public user: User, public products: PurchaseProduct[]) {

    }
}