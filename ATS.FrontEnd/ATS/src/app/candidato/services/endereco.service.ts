import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { Endereco } from '../models/endereco';

//  const baseUrl = 'http://localhost:5000/api/candidato';
const baseUrl = 'https://localhost:44376/api/candidato';

@Injectable({
  providedIn: 'root'
})
export class EnderecoService {

  constructor(
    private httpClient: HttpClient
  ) { }
  
  public alterarEndereco(id: string, endereco: Endereco): Observable<Endereco> {
    return this.httpClient.put<Endereco>(`${baseUrl}/endereco/${id}`, endereco);
  }
  
  public deletarEndereco(id: string): Observable<Endereco> {
    return this.httpClient.delete<Endereco>(`${baseUrl}/endereco/${id}`)
  }
}
