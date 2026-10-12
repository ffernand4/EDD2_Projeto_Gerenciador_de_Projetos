using System;
using System.Collections.Generic;
using System.Linq;

namespace GerenciadorProjetos
{
    public class Projetos
    {
        public List<Projeto> Itens { get; set; }

        public Projetos()
        {
            Itens = new List<Projeto>();
        }

        public bool Adicionar(Projeto p)
        {
            if (p == null || Itens.Any(x => x.Id == p.Id))
            {
                return false;
            }
            Itens.Add(p);
            return true;
        }

        public bool Remover(Projeto p)
        {
            if (p == null) return false;
            Projeto existente = Buscar(p);

            // Regra do enunciado: remover projeto apenas se não possuir tarefas
            if (existente != null && existente.Tarefas.Count == 0)
            {
                return Itens.Remove(existente);
            }
            return false;
        }

        public Projeto Buscar(Projeto p)
        {
            if (p == null) return null;
            return Itens.FirstOrDefault(x => x.Id == p.Id);
        }

        public List<Projeto> Listar()
        {
            return Itens;
        }
    }
}