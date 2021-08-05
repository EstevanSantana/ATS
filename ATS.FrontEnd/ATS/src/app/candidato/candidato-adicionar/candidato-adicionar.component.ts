import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

import { MASKS, NgBrazilValidators } from 'ng-brazil';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

import { Candidato } from '../models/candidato';
import { Curriculo } from '../models/curriculo';
import { CandidatoService } from '../services/candidato.service';

@Component({
  selector: 'app-candidato-adicionar',
  templateUrl: './candidato-adicionar.component.html'
})
export class CandidatoAdicionarComponent implements OnInit {

  public imageBase64: any;
  public imagemPreview: any;
  public imagemNome: string;

  public MASKS = MASKS;
  public cadastroForm: FormGroup;
  public formResult: string = '';
  public candidato: Candidato;
  public curriculoModel: Curriculo;

  constructor(
    private fb: FormBuilder,
    private candidatoService: CandidatoService,
    private router: Router,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService
  ) { }

  ngOnInit(): void {
    this.cadastroForm = this.fb.group({
      nome: ['', [Validators.required, Validators.maxLength(20), Validators.minLength(2)]],
      sobreNome: ['', [Validators.required, Validators.maxLength(80), Validators.minLength(2)]],
      dataNascimento: ['', [Validators.required]],
      cpf: ['', [Validators.required, NgBrazilValidators.cpf]],
      telefone: ['', [Validators.required, NgBrazilValidators.telefone]],

      endereco: this.fb.group({
        cep: ['', [<any>Validators.required, <any>NgBrazilValidators.cep]],
        rua: ['', [Validators.required]],
        numero: ['', [Validators.required]],
        bairro: ['', [Validators.required]],
        cidade: ['', [Validators.required]],
        estado: ['', [Validators.required]]
      }),

      curriculo: this.fb.group({
        imagemUpload: ['', [Validators.required]],
        faculdade: ['', [Validators.required]],
        curso: ['', [Validators.required]],
        conclusao: ['', [Validators.required]],
      })
    })
  }

  public add() {
    if (this.cadastroForm.dirty && this.cadastroForm.valid) {
      this.candidato = Object.assign({}, this.candidato, this.cadastroForm.value);
      this.formResult = JSON.stringify(this.cadastroForm.value);

      if (this.imageBase64) {
        this.candidato.curriculo.imagemUpload = this.imageBase64;
        this.candidato.curriculo.curriculoImagem = this.imagemNome;
      }

      this.candidatoService.adicionar(this.candidato)
        .subscribe(
          response => { this.sucesso(response) },
          error => { this.error(error) }
      );
    }else {
      this.formResult = "Opa! erro";
    }
  }

  public sucesso(response: any){
    this.cadastroForm.reset();

    let toast = this.toastr.success('Cadastro com sucesso!', 'Sucesso!');
    if (toast) {
      toast.onHidden.subscribe(() => {
        this.router.navigate(['/candidato/candidatos-lista']);
      });
    }
  }

  public error(respose: any){
    this.toastr.error('Atenção! Ocorreu um erro.');
    console.log(respose);
  }

  public upload(file: any) {
    this.imagemNome = file[0].name;

    var reader = new FileReader();
    reader.onload = this.manipularReader.bind(this);
    reader.readAsBinaryString(file[0]);
  }

  public manipularReader(readerEvt: any) {
    var binaryString = readerEvt.target.result;
    this.imageBase64 = btoa(binaryString);
    this.imagemPreview = "data:image/jpeg;base64," + this.imageBase64;
  }
}
