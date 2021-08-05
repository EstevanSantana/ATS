import { formatDate } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { Candidato } from '../models/candidato';
import { CandidatoService } from '../services/candidato.service';

@Component({
  selector: 'app-candidato-listar',
  templateUrl: './candidato-listar.component.html'
})
export class CandidatoListarComponent implements OnInit {

  public candidatos: Candidato[];
  public p : any = 1 ;

  constructor(
    private candidatoService: CandidatoService,
    private spinner: NgxSpinnerService
  ) { }

  ngOnInit(): void {
    this.spinner.show();

    this.candidatoService.listar()
      .subscribe(
        response => { this.candidatos = response;
          console.log(response)
        },
        error => { console.log(error) }
      );

      setTimeout(() => {
        this.spinner.hide();
      }, 2500);
  }

  public gerar() {
    this.downloadFile(this.candidatos, 'lista-de candidatos-' + formatDate(Date.now(), 'd/M/yyyy', 'en-US'));
  }

    public downloadFile(data: any, filename='data') {
      let csvData = this.ConvertToCSV(data, ['id', 'nome', 'sobreNome', 'dataNascimento', 'telefone', 'cpf']);
      let blob = new Blob(['\ufeff' + csvData], { type: 'text/csv;charset=utf-8;' });
      let dwldLink = document.createElement("a");
      let url = URL.createObjectURL(blob);
      let isSafariBrowser = navigator.userAgent.indexOf('Safari') != -1 && navigator.userAgent.indexOf('Chrome') == -1;
      if (isSafariBrowser) {  
          dwldLink.setAttribute("target", "_blank");
      }
      dwldLink.setAttribute("href", url);
      dwldLink.setAttribute("download", filename + ".csv");
      dwldLink.style.visibility = "hidden";
      document.body.appendChild(dwldLink);
      dwldLink.click();
      document.body.removeChild(dwldLink);
    }

    public ConvertToCSV(objArray: any, headerList: any) {
      let array = typeof objArray != 'object' ? JSON.parse(objArray) : objArray;
      let str = '';
      let row = ';';

      for (let index in headerList) {
          row += headerList[index] + ';';
      }

      row = row.slice(0, -1);
      str += row + '\r\n';
      for (let i = 0; i < array.length; i++) {
          let line = (i+1)+'';
          for (let index in headerList) {
              let head = headerList[index];

              line += ';' + array[i][head];
          }
          str += line + '\r\n';
      }
      return str;
  }
}


