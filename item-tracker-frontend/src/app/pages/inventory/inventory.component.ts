import { CommonModule } from '@angular/common';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { environment } from '../../../environment';

@Component({
    selector: 'app-inventory',
    imports: [FormsModule, CommonModule],
    templateUrl: './inventory.component.html',
    styleUrl: './inventory.component.css'
})
export class InventoryComponent {
    httpClient = inject(HttpClient);
    inventoryDto: any;
    isUpdate: boolean = false;

    inventoryData = {
        productId: 0,
        productName: "",
        stockAvailable: 0,
        reorderStock: 0
    }

    ngOnInit(): void {
        this.inventoryDetails();
    }

    inventoryDetails(): void {
        this.httpClient.get(environment.apiUrl).subscribe(data => {
            this.inventoryDto = data;
        });
        this.inventoryData = {
            productId: 0,
            productName: "",
            stockAvailable: 0,
            reorderStock: 0
        }
        this.isUpdate = false;
    }

    onDelete(productId: any): void {
        const isDelete = confirm('Do you want to delete?');
        if (isDelete) {
            this.httpClient.delete(`${environment.apiUrl}?ProductId=${productId}`).subscribe(data => {
                console.log(data);
                this.inventoryDetails();
            })
        }
    }

    onEdit(inventory: any) {
        this.inventoryData.productId = inventory.ProductId;
        this.inventoryData.productName = inventory.ProductName;
        this.inventoryData.stockAvailable = inventory.StockAvailable;
        this.inventoryData.reorderStock = inventory.ReorderStock;

        this.isUpdate = true;
    }

    onSubmit(): void {
        let httpOptions = {
            headers: new HttpHeaders({
                Authorization: 'auth-token',
                'Content-Type': 'application/json'
            })
        }

        if (!this.isUpdate) {
            this.httpClient.post(environment.apiUrl, this.inventoryData, httpOptions).subscribe(
                {
                    next: v => console.log(v),
                    error: e => console.log(e),
                    complete: () => {
                        alert('Form Submited' + JSON.stringify(this.inventoryData));
                        this.inventoryDetails();
                    },
                }
            );
        } else {
            this.httpClient.put(environment.apiUrl, this.inventoryData, httpOptions).subscribe(
                {
                    next: v => console.log(v),
                    error: e => console.log(e),
                    complete: () => {
                        alert('Form Updated' + JSON.stringify(this.inventoryData));
                        this.inventoryDetails();
                    },
                }
            );
        }
    }
}
