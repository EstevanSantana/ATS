import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Curriculo } from '../models/curriculo';

// const baseUrl = 'http://localhost:5000/api/candidato';
const baseUrl = 'https://localhost:44376/api/candidato';

@Injectable({
  providedIn: 'root'
})
export class CurriculoService {

  constructor(
    private httpClient: HttpClient
  ) { }
  
  public alterarCurriculo(id: string, curriculo: Curriculo): Observable<Curriculo> {
    return this.httpClient.put<Curriculo>(`${baseUrl}/curriculo/${id}`, curriculo);
  }

  public deletarCurriculo(id: string): Observable<Curriculo> {
    return this.httpClient.delete<Curriculo>(`${baseUrl}/curriculo/${id}`)
  }
}
