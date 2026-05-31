import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { OrdersService } from './orders.service';

@Component({
  selector: 'app-orders',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './orders.component.html'
})
export class OrdersComponent implements OnInit {

  orders: any[] = [];
  loading = false;

  filters = {
    status: '',
    client: '',
    technician: '',
    fromDate: '',
    toDate: ''
  };

  constructor(private ordersService: OrdersService) {}

  ngOnInit(): void {
    this.loadOrders();
  }

  loadOrders(): void {
    this.loading = true;

    this.ordersService.getOrders(this.buildParams()).subscribe({
      next: (data) => {
        this.orders = data;
        this.loading = false;
      },
      error: () => {
        this.loading = false;
      }
    });
  }

  buildParams() {
    const params: any = {};

    if (this.filters.status) params.status = this.filters.status;
    if (this.filters.client) params.client = this.filters.client;
    if (this.filters.technician) params.technician = this.filters.technician;
    if (this.filters.fromDate) params.fromDate = this.filters.fromDate;
    if (this.filters.toDate) params.toDate = this.filters.toDate;

    return params;
  }

  applyFilters(): void {
    this.loadOrders();
  }

  clearFilters(): void {
    this.filters = {
      status: '',
      client: '',
      technician: '',
      fromDate: '',
      toDate: ''
    };

    this.loadOrders();
  }
}