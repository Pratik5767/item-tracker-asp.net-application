import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { CustomerDetailDialogComponent } from '../customer-detail-dialog/customer-detail-dialog.component';

@Component({
    selector: 'app-customer',
    imports: [CommonModule],
    templateUrl: './customer.component.html',
    styleUrl: './customer.component.css'
})
export class CustomerComponent {

    private modelSerivce = inject(NgbModal);

    openModal() {
        this.modelSerivce.open(CustomerDetailDialogComponent);
    }

}
