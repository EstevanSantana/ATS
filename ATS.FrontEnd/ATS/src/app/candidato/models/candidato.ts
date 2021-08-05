import { Curriculo } from "./curriculo";
import { Endereco } from "./endereco";

export class Candidato {
    id: string;
    nome: string;
    sobreNome: string;
    dataNascimento: Date;
    telefone: string;
    cpf: string;
    endereco: Endereco;
    curriculo: Curriculo;
}
