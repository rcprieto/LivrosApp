import { AfterViewInit, Component, HostListener, inject, OnInit, output, signal, ViewChild } from '@angular/core';
import { AbstractControl, FormBuilder,  FormGroup, FormsModule, ReactiveFormsModule, ValidatorFn, Validators } from '@angular/forms';
import { TextInputComponent } from "../../_forms/text-input/text-input.component";
import { NgClass, NgIf } from '@angular/common';
import { UserListComponent } from "../user-list/user-list.component";
import { User } from '../../_models/user';
import { ToastrService } from 'ngx-toastr';
import { UserService } from '../../_services/user.service';
import { PaginatedResut } from '../../_models/pagination';

@Component({
  selector: 'app-user-register',
  standalone: true,
  imports: [FormsModule, TextInputComponent, ReactiveFormsModule, NgIf, NgClass, UserListComponent],
  templateUrl: './user-register.component.html',
  styleUrl: './user-register.component.css'
})
export class UserRegisterComponent implements OnInit, AfterViewInit {

  private userService = inject(UserService);
  private fb = inject(FormBuilder);
  private toastr = inject(ToastrService);
  userServices = inject(UserService);
  pageNumber =1;
  pageSize = 1;
  paginatedResults: PaginatedResut<User[]> = {};

  user: User = {
    id: '',
    userName: '',
    token: '',
    email: '',
    password: '',
    confirmPassword: '',
    roles: []
  };

  registerForm: FormGroup = new FormGroup({});
  cancelRegister = output<boolean>();
  validationErrors: string[] | undefined;

  @HostListener('window:beforeunload', ['$event']) unloadNotification($event: any){
    if(this.registerForm.dirty)
    {
        $event.returnValue = true;
    }
  }

  ngOnInit(): void {
    this.initializeForm();

  }

  ngAfterViewInit(){
    this.getUsers();

  }


  register(){
    this.user = this.registerForm.value;
    if(this.user.id.length == 0)
    {

      this.userService.register(this.user).subscribe({
        next: response => console.log(response),
        error: retorno => {
             this.validationErrors = retorno;
        },
        complete: () => {
          this.registerForm?.reset();
          this.toastr.success('Cadastrado com Sucesso!');
        }
      })

    }
    else
    {
      this.userService.updateUser(this.user).subscribe({
        next: response => console.log(response),
        error: retorno => {
             this.validationErrors = retorno;
        },
        complete: () => {
          this.registerForm?.reset();
          this.toastr.success('Atualizado com Sucesso!');
        }
      })

    }
  }


  fillForm(model: User){
    this.user = model;
    //this.registerForm.controls['email'].setValue(this.user.email);
    this.registerForm.setValue(this.user);
    this.registerForm.controls["password"].setValue(null);
  }

  cancel(){
    this.cancelRegister.emit(false);

  }

  initializeForm(){
    this.registerForm = this.fb.group({
      id: [''],
      token: [''],
      roles: [[]],
      userName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      password: ['',[Validators.required, Validators.minLength(4)]],
      confirmPassword: ['', [Validators.required, Validators.minLength(4), this.matchValues('password')]],
    });

    //roda novamente o matchValues do confirmPassword se mudarem o password
    this.registerForm.controls['password'].valueChanges.subscribe({
      next: () => this.registerForm.controls['confirmPassword'].updateValueAndValidity()
    });
  }

  getUsers()
  {
    this.userServices.getUserListPaginated(this.pageNumber, this.pageSize).subscribe({
      next: response => {

        this.paginatedResults!.pagination = JSON.parse(response.headers.get('Pagination')!);
        this.paginatedResults!.result = response.body as User[];
      }
    });;
  }

  pagedChanged(event: any){
    if(this.pageNumber != event.page )
    {
      this.pageNumber = event.page;
      this.getUsers();
    }
  }

  matchValues(matchTo: string): ValidatorFn{
    return(control: AbstractControl) =>{
      //vai no pai do controle e busca o controle para comparar
      return control.value === control.parent?.get(matchTo)?.value ? null : {notMatching: true}
    }
  }

}
