export interface Lote {
    id: number;
    nome: string;
    preco: number;
    datainicio?: Date;
    dataFim?: Date;
    quantidade: number;
    eventoId: number;
}
