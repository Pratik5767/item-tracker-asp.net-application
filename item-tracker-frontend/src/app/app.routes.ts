import { Routes } from '@angular/router';
import { HomeComponent } from './pages/home/home.component';
import { CustomerComponent } from './pages/customer/customer.component';
import { InventoryComponent } from './pages/inventory/inventory.component';
import { BillComponent } from './pages/bill/bill.component';

export const routes: Routes = [
    { path: '', redirectTo: 'home', pathMatch: 'full' },
    { path: 'home', component: HomeComponent },
    { path: 'inventory', component: InventoryComponent },
    { path: 'customer', component: CustomerComponent },
    { path: 'bill', component: BillComponent }
];
