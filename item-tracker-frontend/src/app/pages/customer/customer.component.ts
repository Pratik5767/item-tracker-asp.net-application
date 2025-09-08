import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { CustomerDetailDialogComponent } from '../customer-detail-dialog/customer-detail-dialog.component';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environment';

@Component({
    selector: 'app-customer',
    imports: [CommonModule],
    templateUrl: './customer.component.html',
    styleUrl: './customer.component.css'
})
export class CustomerComponent {

    private modelSerivce = inject(NgbModal);
    private httpClient = inject(HttpClient);
    customerDetails: any;

    ngOnInit(): void {
        this.getCustomerDetails();
    }

    openModal() {
        this.modelSerivce.open(CustomerDetailDialogComponent).result.then((data) => {
            if (data.event == 'close') {
                this.getCustomerDetails();
            }
        })
    }

    getCustomerDetails() {
        this.httpClient.get(`${environment.apiUrl}/Customer`).subscribe(result => {
            this.customerDetails = result;
            console.log(result);
        });
    }

    onDelete(customerId: any): void {
        const isDelete = confirm('Do you want to delete?');
        if (isDelete) {
            this.httpClient.delete(`${environment.apiUrl}/Customer?CustomerId=${customerId}`).subscribe(data => {
                this.getCustomerDetails();
            })
        }
    }
}
