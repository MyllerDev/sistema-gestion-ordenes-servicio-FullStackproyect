import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ClientsService } from './clients.service';

@Component({
  selector: 'app-clients',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './clients.component.html'
})
export class ClientsComponent implements OnInit {

  clients: any[] = [];
  loading = false;

  form = {
    id: 0,
    fullName: '',
    document: '',
    address: '',
    phone: ''
  };

  constructor(private clientsService: ClientsService) {}

  ngOnInit(): void {
    this.loadClients();
  }

  loadClients(): void {
    this.loading = true;

    this.clientsService.getClients().subscribe({
      next: (data) => {
        this.clients = data;
        this.loading = false;
      },
      error: () => {
        this.loading = false;
      }
    });
  }

  save(): void {
    if (this.form.id === 0) {
      this.clientsService.createClient(this.form).subscribe(() => {
        this.loadClients();
        this.clear();
      });
    } else {
      this.clientsService.updateClient(this.form.id, this.form).subscribe(() => {
        this.loadClients();
        this.clear();
      });
    }
  }

  edit(client: any): void {
    this.form = { ...client };
  }

  delete(id: number): void {
    this.clientsService.deleteClient(id).subscribe(() => {
      this.loadClients();
    });
  }

  clear(): void {
    this.form = {
      id: 0,
      fullName: '',
      document: '',
      address: '',
      phone: ''
    };
  }
}