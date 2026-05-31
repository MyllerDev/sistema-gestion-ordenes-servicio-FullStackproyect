export interface ServiceOrder {
  id: number;
  createdDate: Date;
  status: string;
  description: string;
  technicianId: number;
  clientId: number;
}