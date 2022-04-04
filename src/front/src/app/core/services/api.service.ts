import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CoreModule } from '../core.module';

@Injectable({
  providedIn: CoreModule
})
export class ApiService {
  private readonly baseUrl = "/api";

    constructor(private readonly httpClient: HttpClient) {
    }

    public get<T>(path: string, params: HttpParams | undefined = undefined): Observable<T> {
        return this.httpClient.get<T>(
            this.getFullUrl(path),
            {
                params: params
            }
        );
    }

    public post<T>(path: string, body: unknown = {}): Observable<T> {
        return this.httpClient.post<T>(
            this.getFullUrl(path),
            body
        )
    }

    public put<T>(path: string, body: unknown = {}): Observable<T> {
        return this.httpClient.put<T>(
            this.getFullUrl(path),
            body
        );
    }

    private getFullUrl(url: string): string {
        return this.baseUrl + "/" + url;
    }
}
