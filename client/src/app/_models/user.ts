export interface User {
  id: string;
  userName: string;
  token: string;
  email: string;
  password: string;
  passwordAntigo: string;
  confirmPassword: string;
  roles: string[];
}

export interface UserEdit {
  id: string;
  userName: string;
  email: string;
  password: string;
  passwordAntigo: string;
  confirmPassword: string;
}
