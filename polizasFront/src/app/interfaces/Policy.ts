import { Client } from "./Clients";

export interface Policy extends PolicyCreate {
  client: Client;
  status: PolicyStatus;
}

export interface PolicyCreate{
  policyNumber: string;
  type: string;
  startDate: Date;
  endDate: Date; 
  clientId: number;
  amount: number;
}

export enum type {
  LIFE = "VIDA",
  AUTO = "AUTO",
  HOME = "HOGAR",
  HEALTH = "SALUD",
}

export enum PolicyStatus {
  Cotizada = "Cotizada",
  Autorizada = "Autorizada",
  Rechazada = "Rechazada",
}

