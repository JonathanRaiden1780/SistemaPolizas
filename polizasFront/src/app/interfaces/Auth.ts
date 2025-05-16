export interface User {
  id?: number;
  username: string;
  role: UserRole;
}

export interface SignIn {
  user: User;
  token?: string;
}

export enum UserRole {
  ADMIN = "Admin",
  BROKER = "Broker",
  CLIENT = "Client",
}
