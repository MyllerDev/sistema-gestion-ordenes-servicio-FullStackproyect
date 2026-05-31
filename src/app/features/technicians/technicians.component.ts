import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { TechniciansService } from './technicians.service';

@Component({
  selector: 'app-technicians',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './technicians.component.html'
})
export class TechniciansComponent implements OnInit {

  technicians: any[] = [];
  loading = false;

  form = {
    id: 0,
    fullName: '',
    phone: '',
    specialty: ''
  };

  constructor(private techniciansService: TechniciansService) {}

  ngOnInit(): void {
    this.loadTechnicians();
  }

  loadTechnicians(): void {
    this.loading = true;

    this.techniciansService.getTechnicians().subscribe({
      next: (data) => {
        this.technicians = data;
        this.loading = false;
      },
      error: () => {
        this.loading = false;
      }
    });
  }

  save(): void {
    if (this.form.id === 0) {
      this.techniciansService.createTechnician(this.form).subscribe(() => {
        this.loadTechnicians();
        this.clear();
      });
    } else {
      this.techniciansService.updateTechnician(this.form.id, this.form).subscribe(() => {
        this.loadTechnicians();
        this.clear();
      });
    }
  }

  edit(t: any): void {
    this.form = { ...t };
  }

  delete(id: number): void {
    this.techniciansService.deleteTechnician(id).subscribe(() => {
      this.loadTechnicians();
    });
  }

  clear(): void {
    this.form = {
      id: 0,
      fullName: '',
      phone: '',
      specialty: ''
    };
  }
}