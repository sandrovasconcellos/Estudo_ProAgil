import { Component, OnInit, TemplateRef } from '@angular/core';
import { EventoService } from '../_services/evento.service';
import { Evento } from '../_models/Evento';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { BsLocaleService} from 'ngx-bootstrap/datepicker';
import { defineLocale } from 'ngx-bootstrap/chronos';
import { ptBrLocale } from 'ngx-bootstrap/chronos';

defineLocale('pt-br', ptBrLocale);

// decoreitor
@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.css']
})

export class EventosComponent implements OnInit {
  // variaveis
  eventosFiltrados: Evento[];
  eventos: Evento[];
  evento: Evento;
  modoSalvar = 'post';

  imagemLargura = 50;
  imagemMargem = 2;
  mostrarImagem = false;
  registerForm: FormGroup;
  bodyDeletarEvento = '';

  // valor digitado
  vFiltroLista: string;

  get filtroLista(): string {
    return this.vFiltroLista;
  }
  set filtroLista(value: string){
    this.vFiltroLista = value;
    this.eventosFiltrados = this.filtroLista ? this.filtrarEventos(this.filtroLista) : this.eventos;
  }

  // construtor
  constructor(
    private eventoService: EventoService,
    private modalService: BsModalService,
    private fb: FormBuilder,
    private localeService: BsLocaleService ) {
      this.localeService.use('pt-br');
    }

    editarEvento(evento: Evento, template: any) {
      this.modoSalvar = 'put';
      this.openModal(template);
      this.evento = evento;
      this.registerForm.patchValue(evento);
    }

    novoEvento(template: any){
      this.modoSalvar = 'post';
      this.openModal(template);
    }

    excluirEvento(evento: Evento, template: any) {
      this.openModal(template);
      this.evento = evento;
      this.bodyDeletarEvento = `Tem certeza que deseja excluir o evento: ${evento.tema}, código: ${evento.id}`;
    }

    // excluirEvento(evento: Evento, template: any) {
    //   this.openModal(template);
    //   this.evento = evento;
    //   this.bodyDeletarEvento = `Tem certeza que deseja excluir o Evento: ${evento.tema}, Código: ${evento.id}`;
    // }

    confirmeDelete(template: any) {
      this.eventoService.deleteEvento(this.evento.id).subscribe(
        () => {
          template.hide();
          this.getEventos();
        }, error => {
          console.log(error);
        }
      );
    } 
    
    // confirmeDelete(template: any) {
    //   this.eventoService.deleteEvento(this.evento.id).subscribe(
    //     () => {
    //       template.hide();
    //       this.getEventos();
    //       this.toastr.success('Deletado com Sucesso');
    //     }, error => {
    //       this.toastr.error('Erro ao tentar Deletar');
    //       console.log(error);
    //     }
    //   );
    // }

  // abre a tela modal
  openModal(template: any) {
    this.registerForm.reset();
    template.show(template);
  }

  // igual ao load ou o init
  ngOnInit() {
    this.getEventos();
    this.validation();
  }

  // função
  // recebe um parametro e retorna um array de eventos
  filtrarEventos(filtrarPor: string): Evento[] {
    // coloca para maiusculo o valor do parametro
    filtrarPor = filtrarPor.toLocaleLowerCase();
    return this.eventos.filter(
      evento => evento.tema.toLocaleLowerCase().indexOf(filtrarPor) !== -1
    );
  }

  // função
  alterarImagem(){
    this.mostrarImagem = !this.mostrarImagem;
  }

  validation() {
    this.registerForm = this.fb.group(
      // Objeto
      {
        tema: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(50)]],
        local: ['', Validators.required],
        dataEvento: ['', Validators.required],
        imagemURL: ['', Validators.required],
        qtdPessoas: ['', [Validators.required, Validators.max(12000)]],
        telefone: ['', Validators.required],
        email: ['', [Validators.required, Validators.email]],
      }
    );
  }

  salvarAlteracao(template: any) {
    if (this.registerForm.valid){
      if (this.modoSalvar === 'post') {
        this.evento = Object.assign({}, this.registerForm.value);
        this.eventoService.postEvento(this.evento).subscribe(
          (novoEvento: Evento) => {
            console.log(novoEvento);
            template.hide();
            this.getEventos();
          }, error => {
            console.log(error);
          }
        );
      } else {
        // this.registerForm.value = possui as informações do formulario
        this.evento = Object.assign({id: this.evento.id}, this.registerForm.value);
        this.eventoService.putEvento(this.evento).subscribe(
          () => {
            template.hide();
            this.getEventos();
          }, error => {
            console.log(error);
          }
        );
      }
    }
  }

  // função - acessa o service do evento eventoservice
  getEventos() {
    this.eventoService.getAllEvento().subscribe(
    (pEventos: Evento[]) => {
      this.eventos = pEventos;
      this.eventosFiltrados = this.eventos;
      console.log(this.eventos);
    }, error => {
      console.log(error);
    });
  }
}




// eventos: any = [
  //   {
  //     EventoId: 1,
  //     Tema: 'Angular',
  //     Local: 'São Gonçalo',
  //   },
  //   {
  //     EventoId: 2,
  //     Tema: '.NET',
  //     Local: 'Niteroi',
  //   },
  //   {
  //     EventoId: 3,
  //     Tema: 'Angular .NET',
  //     Local: 'Rio de Janeiro',
  //   }
  // ]
