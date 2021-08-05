import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { Candidato } from '../models/candidato';

// const baseUrl = 'http://localhost:5000/api/candidato';
const baseUrl = 'https://localhost:44376/api/candidato';

@Injectable({
  providedIn: 'root'
})
export class CandidatoService {

  constructor(
    private httpClient: HttpClient
  ) { }

  public adicionar(candidato: Candidato): Observable<Candidato> {
    return this.httpClient.post<Candidato>(baseUrl, candidato);
  }

  public listar(): Observable<Candidato[]>{
    return this.httpClient.get<Candidato[]>(baseUrl);
  }

  public obterPorId(id: string): Observable<Candidato> {
    return this.httpClient.get<Candidato>(`${baseUrl}/${id}`);
  }

  public alterar(id: string, candidato: Candidato): Observable<Candidato> {
    return this.httpClient.put<Candidato>(`${baseUrl}/${id}`, candidato);
  }

  public deletar(id: string): Observable<Candidato> {
    return this.httpClient.delete<Candidato>(`${baseUrl}/${id}`)
  }
}
