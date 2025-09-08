import { CommonModule } from '@angular/common';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { environment } from '../../../environment';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
    selector: 'app-customer-detail-dialog',
    imports: [FormsModule, CommonModule],
    templateUrl: './customer-detail-dialog.component.html',
    styleUrl: './customer-detail-dialog.component.css'
})
export class CustomerDetailDialogComponent {
    private modalService = inject(NgbActiveModal);
    private httpClient = inject(HttpClient);

    CustomerDetails = {
        CustomerId: '',
        FirstName: '',
        LastName: '',
        Email: '',
        Mobile: '',
        RegistrationDate: ''
    }

    onSubmit() {
        let httpOptions = {
            headers: new HttpHeaders({
                Authorization: 'auth-token',
                'Content-Type': 'application/json'
            })
        }

        this.httpClient.post(`${environment.apiUrl}/Customer`, this.CustomerDetails, httpOptions).subscribe({
            next: v => console.log(v),
            error: e => console.log(e),
            complete: () => {
                alert('Customer added successfully');
                this.modalService.close({ event: 'close' });
            }
        })
    }
}
