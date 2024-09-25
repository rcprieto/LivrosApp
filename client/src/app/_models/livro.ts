import { User } from './user';

export interface Livro {
  id: number;
  fimLeitura: Date | null;
  inicioLeitura: Date | null;
  nome: string;
  urlCapa: string | null;
  autor: string;
  resumo: string | null;
  appUser: User | null;
  paginas: number | null;
  categoria: string | null;
}

export interface LivroDto {
  id: number;
  fimLeitura: Date | null;
  inicioLeitura: Date | null;
  nome: string;
  urlCapa: string | null;
  autor: string;
  resumo: string | null;
  appUserId: string | null;
  appUser: string | null;
  paginas: number | null;
  categoria: string | null;
}
