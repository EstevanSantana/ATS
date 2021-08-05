// ANGULAR MODULOS
import { NgModule, CUSTOM_ELEMENTS_SCHEMA  } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
//OUTROS
import { NgBrazil } from 'ng-brazil'
import { TextMaskModule } from 'angular2-text-mask';
import { ModalModule } from 'ngx-bootstrap/modal';
import { NgxPaginationModule } from 'ngx-pagination';
import { NgxSpinnerModule } from "ngx-spinner";
//COMPONENTES
import { CandidatoListarComponent } from './candidato-listar/candidato-listar.component';
import { CandidatoAdicionarComponent } from './candidato-adicionar/candidato-adicionar.component';
import { CadidatoEditarComponent } from './cadidato-editar/cadidato-editar.component';
// ROTAS E APP
import { CandidatoAppComponent } from './candidato.app.component';
import { CandidatoRoutingModule } from './candidato.route';
// SERVICES
import { CandidatoService } from './services/candidato.service';
import { EnderecoService } from './services/endereco.service';
import { CurriculoService } from './services/curriculo.service';

@NgModule({
    declarations: [
        CandidatoAppComponent,
        CandidatoListarComponent,
        CandidatoAdicionarComponent,
        CadidatoEditarComponent
    ],
    imports: [
        CandidatoRoutingModule,
        CommonModule,
        RouterModule,
        FormsModule,
        ReactiveFormsModule,
        HttpClientModule,
        TextMaskModule,
        NgBrazil,
        ModalModule.forRoot(),
        NgxPaginationModule,
        NgxSpinnerModule
    ],
    providers: [
        CandidatoService,
        EnderecoService,
        CurriculoService
    ],
    schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class CandidatoModule {}
