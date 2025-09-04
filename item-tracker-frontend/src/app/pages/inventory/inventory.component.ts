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

    onSubmit(): void {
        let httpOptions = {
            headers: new HttpHeaders({
                Authorization: 'auth-token',
                'Content-Type': 'application/json'
            })
        }

        this.httpClient.post(environment.apiUrl, this.inventoryData, httpOptions).subscribe(
            {
                next: v => console.log(v),
                error: e => console.log(e),
                complete: () => {
                    alert('Form Submited' + JSON.stringify(this.inventoryData));
                    this.inventoryDetails();
                    this.inventoryData = {
                        productId: 0,
                        productName: "",
                        stockAvailable: 0,
                        reorderStock: 0
                    }
                },
            }
        );
    }
}
