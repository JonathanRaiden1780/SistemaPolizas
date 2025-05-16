import { Injectable } from "@angular/core";
import { HttpClient, HttpErrorResponse } from "@angular/common/http";
import { Observable, throwError } from "rxjs";
import { catchError } from "rxjs/operators";
import { environment } from "../../../environments/environment";
import { Client } from "../../interfaces/Clients";

@Injectable({
  providedIn: "root",
})
export class ClientsService {
  private apiUrl = `${environment.urlBase}/clients`;

  constructor(private http: HttpClient) {}

  getClients(): Observable<Client[]> {
    return this.http.get<Client[]>(this.apiUrl).pipe(catchError(this.handleError));
  }

  getClientsById(id: string): Observable<Client> {
    return this.http.get<Client>(`${this.apiUrl}/by-id?id=${id}`).pipe(catchError(this.handleError));
  }
 
  getClientsByUser(id: string): Observable<Client> {
    return this.http.get<Client>(`${this.apiUrl}/by-user?id=${id}`).pipe(catchError(this.handleError));
  }

  createClients(Clients: Client): Observable<number> {
    return this.http.post<number>(this.apiUrl+'/create', Clients).pipe(catchError(this.handleError));
  }

  updateClients(id: string, Clients: Partial<Client>): Observable<Client> {
    return this.http.put<Client>(`${this.apiUrl}/${id}`, Clients).pipe(catchError(this.handleError));
  }

  deleteClients(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`).pipe(catchError(this.handleError));
  }

  private handleError(error: HttpErrorResponse) {
    let errorMessage = "Ha ocurrido un error en el servidor";

    if (error.error instanceof ErrorEvent) {
      errorMessage = `Error: ${error.error.message}`;
    } else {
      switch (error.status) {
        case 404:
          errorMessage = "La póliza no fue encontrada";
          break;
        case 400:
          errorMessage = "Datos de póliza inválidos";
          break;
        case 403:
          errorMessage = "No tiene permisos para realizar esta acción";
          break;
        case 401:
          errorMessage = "No autorizado";
          break;
      }
    }

    return throwError(() => new Error(errorMessage));
  }
}
