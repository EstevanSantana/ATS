import { RouterModule, Routes } from '@angular/router';
import { NgModule } from '@angular/core';
import { CandidatoAppComponent } from './candidato.app.component';
import { CandidatoListarComponent } from './candidato-listar/candidato-listar.component';
import { CandidatoAdicionarComponent } from './candidato-adicionar/candidato-adicionar.component';
import { CadidatoEditarComponent } from './cadidato-editar/cadidato-editar.component';

const routes: Routes = [
  {  
    path: '', component: CandidatoAppComponent,
    children: [
      { path: 'candidatos-lista', component: CandidatoListarComponent },
      { path: 'candidato-adiciona', component: CandidatoAdicionarComponent },
      { path: 'candidato-editar/:id', component: CadidatoEditarComponent },
    ]
  }
];

@NgModule({
  imports: [ 
    RouterModule.forChild(routes)
  ],  
})
export class CandidatoRoutingModule {}
