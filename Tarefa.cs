using System;

namespace GerenciadorProjetos
{
    public class Tarefa
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public int Prioridade { get; set; } // 1=alta, 2=média, 3=baixa
        public string Status { get; set; }  // "Aberta", "Fechada", "Cancelada"
        public DateTime DataCriacao { get; set; }
        public DateTime? DataConclusao { get; set; }

        public Tarefa()
        {
            DataCriacao = DateTime.Now;
            Status = "Aberta";
            DataConclusao = null;
        }

        public Tarefa(int id, string titulo, string descricao, int prioridade) : this()
        {
            Id = id;
            Titulo = titulo;
            Descricao = descricao;
            Prioridade = prioridade;
        }

        public void Concluir()
        {
            Status = "Fechada";
            DataConclusao = DateTime.Now;
        }

        public void Cancelar()
        {
            Status = "Cancelada";
        }

        public void Reabrir()
        {
            Status = "Aberta";
            DataConclusao = null;
        }

        public string ObterPrioridadeTexto()
        {
            return Prioridade switch
            {
                1 => "Alta",
                2 => "Média",
                3 => "Baixa",
                _ => "Indefinida"
            };
        }

        public override bool Equals(object obj)
        {
            if (obj is Tarefa t)
            {
                return Id == t.Id;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override string ToString()
        {
            string conclusao = DataConclusao.HasValue ? DataConclusao.Value.ToString("dd/MM/yyyy HH:mm") : "-";
            return $"[ID: {Id}] {Titulo} | Prioridade: {ObterPrioridadeTexto()} ({Prioridade}) | Status: {Status}\n" +
                   $"    Criada em: {DataCriacao:dd/MM/yyyy HH:mm} | Concluída em: {conclusao}\n" +
                   $"    Descrição: {Descricao}";
        }
    }
}