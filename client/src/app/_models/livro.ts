import { User } from './user';

export interface Livro {
  id: number;
  fimLeitura: Date | null;
  nome: string;
  urlCapa: string | null;
  autor: string;
  resumo: string | null;
  appUser: User | null;
}
