import { Component, OnInit, TemplateRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import { MASKS, NgBrazilValidators } from 'ng-brazil';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

import { Candidato } from '../models/candidato';
import { Curriculo } from '../models/curriculo';
import { Endereco } from '../models/endereco';
import { CandidatoService } from '../services/candidato.service';
import { CurriculoService } from '../services/curriculo.service';
import { EnderecoService } from '../services/endereco.service';

@Component({
  selector: 'app-cadidato-editar',
  templateUrl: './cadidato-editar.component.html'
})
export class CadidatoEditarComponent implements OnInit {

  public idCandidato: string;
  public candidato: Candidato;
  public endereco: Endereco;
  public curriculo: Curriculo;

  public isView: boolean = false;
  public isViewTbEndereco: boolean = true;
  public isViewTbCurriculo: boolean = true;
  public MASKS = MASKS;
  public modalRef: BsModalRef;

  public formResult: string = '';
  public cadastroForm: FormGroup;
  public enderecoForm: FormGroup;
  public curriculoForm: FormGroup;
  
  constructor(
    private candidatoService: CandidatoService,
    private enderecoService: EnderecoService,
    private curriculoService: CurriculoService,
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private modalService: BsModalService,
    private toastr: ToastrService,
    private router: Router,
    private spinner: NgxSpinnerService
  ) { }

  ngOnInit(): void {
    this.spinner.show();

    this.idCandidato = this.route.snapshot.params['id'];
    this.candidatoService.obterPorId(this.idCandidato)
      .subscribe(
        candidato => {
          this.candidato = candidato;
          this.cadastroForm.patchValue(candidato);
          if (this.candidato.curriculo == null) {
            this.isViewTbCurriculo = false;
          } else {
            this.curriculoForm.patchValue(candidato.curriculo);
          }
          if (this.candidato.endereco == null) {
            this.isViewTbEndereco = false;
          } else {
            this.enderecoForm.patchValue(candidato.endereco);
          }
        }
    );

    this.carregaForms();    

    setTimeout(() => {
      this.spinner.hide();
    }, 1000);
  }

  public carregaForms() {
    this.cadastroForm = this.fb.group({
      id: '',
      nome: ['', [Validators.required]],
      sobreNome: ['', [Validators.required]],
      dataNascimento: ['', [Validators.required]],
      cpf: ['', [Validators.required, NgBrazilValidators.cpf]],
      telefone: ['', [Validators.required, NgBrazilValidators.telefone]],
    });
    this.curriculoForm = this.fb.group({
      id: '',
      curriculoImagem: '',
      faculdade: ['', [Validators.required]],
      curso: ['', [Validators.required]],
      conclusao: ['', [Validators.required]],
      candidatoId: ''
    });
    this.enderecoForm = this.fb.group({
      id: '',
      cep: ['', [<any>Validators.required, <any>NgBrazilValidators.cep]],
      rua: ['', [Validators.required]],
      numero: ['', [Validators.required]],
      bairro: ['', [Validators.required]],
      cidade: ['', [Validators.required]],
      estado: ['', [Validators.required]],
      candidatoId: ''
    });
  }

  public editarCandidato() {
    if (this.cadastroForm.dirty && this.cadastroForm.valid) {
      this.candidato = Object.assign({}, this.candidato, this.cadastroForm.value);
      this.candidatoService.alterar(this.candidato.id, this.candidato)
      .subscribe(
        sucesso => { this.sucesso(this.candidato) },
        error => { this.error() }
      )
    }
  }

  public editarEndereco() {
    if (this.enderecoForm.dirty && this.enderecoForm.valid) {
      this.endereco = Object.assign({}, this.endereco, this.enderecoForm.value);

      this.endereco.id = this.candidato.endereco.id;
      this.enderecoService.alterarEndereco(this.candidato.endereco.id, this.endereco)
        .subscribe(
          () => this.sucessoEndereco(this.endereco),
          error => { this.error() }
        );
    }
  }

  public editarCurriculo() {
    if (this.curriculoForm.dirty && this.curriculoForm.valid) {
      this.curriculo = Object.assign({}, this.curriculo, this.curriculoForm.value);

      this.curriculo.id = this.candidato.curriculo.id;
      this.curriculoService.alterarCurriculo(this.candidato.curriculo.id, this.curriculo)
        .subscribe(
          () => this.sucessoCurriculo(this.curriculo),
          error => { this.error() }
        );
    }
  }

  public deletarCandidato() {
    if (this.candidato.endereco === null || this.candidato.curriculo === null) {
      this.candidatoService.deletar(this.idCandidato)
       .subscribe(
          response => { this.sucessoDeletar() },
          error => { this.error(); }
        );
    } else {
      this.isView = true;
    }    
  }

  public deletarCandidatoEndereco() {
    this.enderecoService.deletarEndereco(this.candidato.endereco.id)
       .subscribe(
          response => { this.sucessoDeletarEndereco() },
          error => { this.error(); }
        );
    this.modalRef.hide();
  }

  public deletarCandidatoCurriculo() {
    this.curriculoService.deletarCurriculo(this.candidato.curriculo.id)
       .subscribe(
          response => { this.sucessoDeletarCurriculo() },
          error => { this.error(); }
        );
    this.modalRef.hide();
  }

  public sucesso(candidato: Candidato){
    this.toastr.success('Atualização com sucesso!', 'Sucesso!');
    this.candidato = candidato;
  }

  public sucessoDeletar() {
    this.modalRef.hide();
    let toast = this.toastr.success('Candidato deletado com sucesso!', 'Sucesso!');
    if (toast) {
      toast.onHidden.subscribe(() => {
        this.router.navigate(['/candidato/candidatos-lista']);
      });
    }
  }
  
  public sucessoDeletarEndereco() {
    this.toastr.success('Endereço deletado com sucesso!', 'Sucesso!');
    this.isViewTbEndereco = false;
    this.modalRef.hide();
  }
    
  public sucessoDeletarCurriculo() {
    this.toastr.success('Curriculo deletado com sucesso!', 'Sucesso!');
    this.isViewTbCurriculo = false;
    this.modalRef.hide();
  }
  
  public sucessoEndereco(endereco: Endereco){
    this.toastr.success('Endereço atualizado com sucesso!', 'Sucesso!');
    this.candidato.endereco = endereco;
    this.modalRef.hide();
  }
  
  public sucessoCurriculo(curriculo: Curriculo){
    this.toastr.success('Curriculo atualizado com sucesso!', 'Sucesso!');
    this.candidato.curriculo = curriculo;
    this.modalRef.hide();
  }

  public error() {
    this.toastr.error('Atenção! Ocorreu um erro.');
  }

  public openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(
      template,
      Object.assign({}, { class: 'gray modal-lg' })
    );
  }

  public openModalDeletar(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(
      template,
      Object.assign({}, { class: 'modal-md' })
    );
  }
}
