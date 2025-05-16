import { Injectable } from "@angular/core";
import {
  HttpClient,
  HttpErrorResponse,
  HttpParams,
} from "@angular/common/http";
import { Observable, throwError } from "rxjs";
import { catchError } from "rxjs/operators";
import { environment } from "../../../environments/environment";
import { Policy, PolicyCreate } from "../../interfaces/Policy";

@Injectable({
  providedIn: "root",
})
export class PolicyService {
  private apiUrl = `${environment.urlBase}/policies`;

  constructor(private http: HttpClient) {}

  getPolicies(userId?: number): Observable<Policy[]> {
    let params = new HttpParams();
    if (userId != null) {
      params = params.set("userId", userId.toString());
    }
    return this.http
      .get<Policy[]>(this.apiUrl, { params })
      .pipe(catchError(this.handleError));
  }

  getPolicyById(id: string): Observable<Policy> {
    return this.http
      .get<Policy>(`${this.apiUrl}/by-id?id=${id}`)
      .pipe(catchError(this.handleError));
  }

  createPolicy(policy: PolicyCreate): Observable<Policy> {
    return this.http
      .post<Policy>(this.apiUrl + "/create", policy)
      .pipe(catchError(this.handleError));
  }

  updatePolicy(id: string, policy: Partial<PolicyCreate>): Observable<Policy> {
    return this.http
      .put<Policy>(`${this.apiUrl}/update?id=${id}`, policy)
      .pipe(catchError(this.handleError));
  }

  deletePolicy(id: string): Observable<void> {
    return this.http
      .delete<void>(`${this.apiUrl}/${id}`)
      .pipe(catchError(this.handleError));
  }

   statusPolicy(id: string, stauts: string): Observable<Policy[]> {
    let params = new HttpParams();
    params = params.set("id", id);
    params = params.set("status", stauts);
    return this.http
      .get<Policy[]>(this.apiUrl+"/status", { params })
      .pipe(catchError(this.handleError));
  }

  private handleError(error: HttpErrorResponse) {
    let errorMessage = "Ha ocurrido un error en el servidor";

    if (error.error instanceof ErrorEvent) {
      errorMessage = `Error: ${error.error.message}`;
    } else {
      console.log(error);
      
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
        case 409:
          errorMessage = error.error.errors[0];
          break;
      }
    }

    return throwError(() => new Error(errorMessage));
  }
}
