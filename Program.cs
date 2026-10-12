using System;
using System.Collections.Generic;
using System.Linq;

namespace GerenciadorProjetos
{
    class Program
    {
        static void Main(string[] args)
        {
            Projetos colecaoProjetos = new Projetos();
            int opcao = -1;

            while (opcao != 0)
            {
                ExibirMenu();
                opcao = LerInteiro("Escolha uma opção: ");
                Console.WriteLine();

                switch (opcao)
                {
                    case 0:
                        Console.WriteLine("Saindo do sistema. Até logo!");
                        break;
                    case 1:
                        AdicionarProjeto(colecaoProjetos);
                        break;
                    case 2:
                        PesquisarProjeto(colecaoProjetos);
                        break;
                    case 3:
                        RemoverProjeto(colecaoProjetos);
                        break;
                    case 4:
                        AdicionarTarefaEmProjeto(colecaoProjetos);
                        break;
                    case 5:
                        ConcluirTarefa(colecaoProjetos);
                        break;
                    case 6:
                        CancelarTarefa(colecaoProjetos);
                        break;
                    case 7:
                        ReabrirTarefa(colecaoProjetos);
                        break;
                    case 8:
                        ListarTarefasDeProjeto(colecaoProjetos);
                        break;
                    case 9:
                        FiltrarTarefasEmProjeto(colecaoProjetos);
                        break;
                    case 10:
                        FiltrarTarefasEmTodosProjetos(colecaoProjetos);
                        break;
                    case 11:
                        ExibirResumoGeral(colecaoProjetos);
                        break;
                    default:
                        Console.WriteLine("Opção inválida! Tente novamente.");
                        break;
                }

                if (opcao != 0)
                {
                    Console.WriteLine("\nPressione ENTER para continuar...");
                    Console.ReadLine();
                }
            }
        }

        static void ExibirMenu()
        {
            Console.Clear();
            Console.WriteLine("=========================================================================");
            Console.WriteLine(" IFSP Cubatão - CBTEDD2 - Gerenciador de Projetos (Semana 9)");
            Console.WriteLine("=========================================================================");
            Console.WriteLine("0. Sair");
            Console.WriteLine("1. Adicionar projeto");
            Console.WriteLine("2. Pesquisar projeto (mostrar tarefas por status e totais)");
            Console.WriteLine("3. Remover projeto (apenas se sem tarefas)");
            Console.WriteLine("4. Adicionar tarefa em projeto");
            Console.WriteLine("5. Concluir tarefa");
            Console.WriteLine("6. Cancelar tarefa");
            Console.WriteLine("7. Reabrir tarefa");
            Console.WriteLine("8. Listar tarefas de um projeto");
            Console.WriteLine("9. Filtrar tarefas por status ou prioridade em um projeto");
            Console.WriteLine("10. Filtrar tarefas por status ou prioridade em todos os projetos");
            Console.WriteLine("11. Resumo geral (projetos, tarefas abertas/fechadas/canceladas, % concluídas)");
            Console.WriteLine("=========================================================================");
        }

        static void AdicionarProjeto(Projetos colecao)
        {
            Console.WriteLine("--- 1. ADICIONAR PROJETO ---");
            int id = LerInteiro("Informe o ID do Projeto: ");

            if (colecao.Buscar(new Projeto { Id = id }) != null)
            {
                Console.WriteLine("Erro: Já existe um projeto com este ID!");
                return;
            }

            string nome = LerString("Informe o Nome do Projeto: ");
            Projeto p = new Projeto(id, nome);

            if (colecao.Adicionar(p))
            {
                Console.WriteLine("Projeto adicionado com sucesso!");
            }
            else
            {
                Console.WriteLine("Erro ao adicionar projeto.");
            }
        }

        static void PesquisarProjeto(Projetos colecao)
        {
            Console.WriteLine("--- 2. PESQUISAR PROJETO ---");
            int id = LerInteiro("Informe o ID do Projeto: ");
            Projeto p = colecao.Buscar(new Projeto { Id = id });

            if (p == null)
            {
                Console.WriteLine("Projeto não encontrado.");
                return;
            }

            Console.WriteLine($"\nProjeto Encontrado: {p.Nome} (ID: {p.Id})");
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine($"Total Abertas   : {p.TotalAbertas()}");
            Console.WriteLine($"Total Fechadas  : {p.TotalFechadas()}");
            Console.WriteLine($"Total Canceladas: {p.TotalCanceladas()}");
            Console.WriteLine($"Total Geral     : {p.Tarefas.Count}");
            Console.WriteLine("--------------------------------------------------");

            Console.WriteLine("\n[TAREFAS ABERTAS]");
            ExibirListaTarefas(p.TarefasPorStatus("Aberta"));

            Console.WriteLine("\n[TAREFAS FECHADAS]");
            ExibirListaTarefas(p.TarefasPorStatus("Fechada"));

            Console.WriteLine("\n[TAREFAS CANCELADAS]");
            ExibirListaTarefas(p.TarefasPorStatus("Cancelada"));
        }

        static void RemoverProjeto(Projetos colecao)
        {
            Console.WriteLine("--- 3. REMOVER PROJETO ---");
            int id = LerInteiro("Informe o ID do Projeto a remover: ");
            Projeto p = colecao.Buscar(new Projeto { Id = id });

            if (p == null)
            {
                Console.WriteLine("Projeto não encontrado.");
                return;
            }

            if (p.Tarefas.Count > 0)
            {
                Console.WriteLine($"Erro: O projeto '{p.Nome}' possui {p.Tarefas.Count} tarefa(s) vinculada(s). A remoção só é permitida para projetos sem tarefas!");
                return;
            }

            if (colecao.Remover(p))
            {
                Console.WriteLine("Projeto removido com sucesso!");
            }
            else
            {
                Console.WriteLine("Erro ao remover projeto.");
            }
        }

        static void AdicionarTarefaEmProjeto(Projetos colecao)
        {
            Console.WriteLine("--- 4. ADICIONAR TAREFA EM PROJETO ---");
            int idProj = LerInteiro("Informe o ID do Projeto: ");
            Projeto p = colecao.Buscar(new Projeto { Id = idProj });

            if (p == null)
            {
                Console.WriteLine("Projeto não encontrado.");
                return;
            }

            int idTar = LerInteiro("Informe o ID da Tarefa: ");
            if (p.BuscarTarefa(new Tarefa { Id = idTar }) != null)
            {
                Console.WriteLine("Erro: Já existe uma tarefa com este ID neste projeto!");
                return;
            }

            string titulo = LerString("Informe o Título da Tarefa: ");
            string desc = LerString("Informe a Descrição da Tarefa: ");
            
            int prio = 0;
            while (prio < 1 || prio > 3)
            {
                prio = LerInteiro("Informe a Prioridade (1=Alta, 2=Média, 3=Baixa): ");
                if (prio < 1 || prio > 3)
                {
                    Console.WriteLine("Prioridade inválida! Digite 1, 2 ou 3.");
                }
            }

            Tarefa t = new Tarefa(idTar, titulo, desc, prio);
            p.AdicionarTarefa(t);
            Console.WriteLine("Tarefa adicionada ao projeto com sucesso!");
        }

        static void ConcluirTarefa(Projetos colecao)
        {
            Console.WriteLine("--- 5. CONCLUIR TAREFA ---");
            Tarefa t = LocalizarTarefaPorProjeto(colecao, out Projeto p);
            if (t != null)
            {
                t.Concluir();
                Console.WriteLine($"Tarefa ID {t.Id} concluída com sucesso! Data de Conclusão: {t.DataConclusao}");
            }
        }

        static void CancelarTarefa(Projetos colecao)
        {
            Console.WriteLine("--- 6. CANCELAR TAREFA ---");
            Tarefa t = LocalizarTarefaPorProjeto(colecao, out Projeto p);
            if (t != null)
            {
                t.Cancelar();
                Console.WriteLine($"Tarefa ID {t.Id} cancelada com sucesso!");
            }
        }

        static void ReabrirTarefa(Projetos colecao)
        {
            Console.WriteLine("--- 7. REABRIR TAREFA ---");
            Tarefa t = LocalizarTarefaPorProjeto(colecao, out Projeto p);
            if (t != null)
            {
                t.Reabrir();
                Console.WriteLine($"Tarefa ID {t.Id} reaberta com sucesso!");
            }
        }

        static void ListarTarefasDeProjeto(Projetos colecao)
        {
            Console.WriteLine("--- 8. LISTAR TAREFAS DE UM PROJETO ---");
            int idProj = LerInteiro("Informe o ID do Projeto: ");
            Projeto p = colecao.Buscar(new Projeto { Id = idProj });

            if (p == null)
            {
                Console.WriteLine("Projeto não encontrado.");
                return;
            }

            Console.WriteLine($"\nTarefas do Projeto '{p.Nome}':");
            ExibirListaTarefas(p.Tarefas);
        }

        static void FiltrarTarefasEmProjeto(Projetos colecao)
        {
            Console.WriteLine("--- 9. FILTRAR TAREFAS EM UM PROJETO ---");
            int idProj = LerInteiro("Informe o ID do Projeto: ");
            Projeto p = colecao.Buscar(new Projeto { Id = idProj });

            if (p == null)
            {
                Console.WriteLine("Projeto não encontrado.");
                return;
            }

            Console.WriteLine("1. Filtrar por Status (Aberta, Fechada, Cancelada)");
            Console.WriteLine("2. Filtrar por Prioridade (1=Alta, 2=Média, 3=Baixa)");
            int tipo = LerInteiro("Escolha o tipo de filtro: ");

            if (tipo == 1)
            {
                string status = LerString("Digite o Status desejado (Aberta, Fechada ou Cancelada): ");
                List<Tarefa> res = p.TarefasPorStatus(status);
                Console.WriteLine($"\nResultados para Status '{status}' no Projeto '{p.Nome}':");
                ExibirListaTarefas(res);
            }
            else if (tipo == 2)
            {
                int prio = LerInteiro("Digite a Prioridade desejada (1, 2 ou 3): ");
                List<Tarefa> res = p.TarefasPorPrioridade(prio);
                Console.WriteLine($"\nResultados para Prioridade '{prio}' no Projeto '{p.Nome}':");
                ExibirListaTarefas(res);
            }
            else
            {
                Console.WriteLine("Opção de filtro inválida.");
            }
        }

        static void FiltrarTarefasEmTodosProjetos(Projetos colecao)
        {
            Console.WriteLine("--- 10. FILTRAR TAREFAS EM TODOS OS PROJETOS ---");
            Console.WriteLine("1. Filtrar por Status (Aberta, Fechada, Cancelada)");
            Console.WriteLine("2. Filtrar por Prioridade (1=Alta, 2=Média, 3=Baixa)");
            int tipo = LerInteiro("Escolha o tipo de filtro: ");

            if (tipo == 1)
            {
                string status = LerString("Digite o Status desejado: ");
                Console.WriteLine($"\n=== Tarefas com Status '{status}' em Todos os Projetos ===");
                bool encontrou = false;

                foreach (var proj in colecao.Listar())
                {
                    var lista = proj.TarefasPorStatus(status);
                    if (lista.Count > 0)
                    {
                        encontrou = true;
                        Console.WriteLine($"\nProjeto: {proj.Nome} (ID: {proj.Id})");
                        ExibirListaTarefas(lista);
                    }
                }

                if (!encontrou)
                {
                    Console.WriteLine("Nenhuma tarefa encontrada com esse status.");
                }
            }
            else if (tipo == 2)
            {
                int prio = LerInteiro("Digite a Prioridade desejada (1, 2 ou 3): ");
                Console.WriteLine($"\n=== Tarefas com Prioridade '{prio}' em Todos os Projetos ===");
                bool encontrou = false;

                foreach (var proj in colecao.Listar())
                {
                    var lista = proj.TarefasPorPrioridade(prio);
                    if (lista.Count > 0)
                    {
                        encontrou = true;
                        Console.WriteLine($"\nProjeto: {proj.Nome} (ID: {proj.Id})");
                        ExibirListaTarefas(lista);
                    }
                }

                if (!encontrou)
                {
                    Console.WriteLine("Nenhuma tarefa encontrada com essa prioridade.");
                }
            }
            else
            {
                Console.WriteLine("Opção de filtro inválida.");
            }
        }

        static void ExibirResumoGeral(Projetos colecao)
        {
            Console.WriteLine("--- 11. RESUMO GERAL DO SISTEMA ---");
            int totalProjetos = colecao.Listar().Count;

            int totalAbertas = 0;
            int totalFechadas = 0;
            int totalCanceladas = 0;

            foreach (var p in colecao.Listar())
            {
                totalAbertas += p.TotalAbertas();
                totalFechadas += p.TotalFechadas();
                totalCanceladas += p.TotalCanceladas();
            }

            int totalGeralTarefas = totalAbertas + totalFechadas + totalCanceladas;

            // Porcentagem de concluídas em relação às abertas
            double pctConcluidasEmRelacaoAbertas = totalAbertas > 0 ? ((double)totalFechadas / totalAbertas) * 100 : 0.0;

            // Porcentagem de concluídas em relação ao total de tarefas criadas
            double pctConcluidasDoTotal = totalGeralTarefas > 0 ? ((double)totalFechadas / totalGeralTarefas) * 100 : 0.0;

            Console.WriteLine($"Quantidade Total de Projetos            : {totalProjetos}");
            Console.WriteLine($"Total de Tarefas Abertas                : {totalAbertas}");
            Console.WriteLine($"Total de Tarefas Fechadas (Concluídas) : {totalFechadas}");
            Console.WriteLine($"Total de Tarefas Canceladas              : {totalCanceladas}");
            Console.WriteLine($"Total Geral de Tarefas Cadastradas      : {totalGeralTarefas}");
            Console.WriteLine("-------------------------------------------------------------------------");
            Console.WriteLine($"% Concluídas em relação às Abertas      : {pctConcluidasEmRelacaoAbertas:F2}%");
            Console.WriteLine($"% Concluídas em relação ao Total        : {pctConcluidasDoTotal:F2}%");
            Console.WriteLine("=========================================================================");
        }

        // Funções Auxiliares de Input e Busca
        static Tarefa LocalizarTarefaPorProjeto(Projetos colecao, out Projeto projetoEncontrado)
        {
            projetoEncontrado = null;
            int idProj = LerInteiro("Informe o ID do Projeto: ");
            Projeto p = colecao.Buscar(new Projeto { Id = idProj });

            if (p == null)
            {
                Console.WriteLine("Projeto não encontrado.");
                return null;
            }

            projetoEncontrado = p;
            int idTar = LerInteiro("Informe o ID da Tarefa: ");
            Tarefa t = p.BuscarTarefa(new Tarefa { Id = idTar });

            if (t == null)
            {
                Console.WriteLine("Tarefa não encontrada neste projeto.");
                return null;
            }

            return t;
        }

        static void ExibirListaTarefas(List<Tarefa> lista)
        {
            if (lista == null || lista.Count == 0)
            {
                Console.WriteLine("  (Nenhuma tarefa nesta categoria)");
                return;
            }

            foreach (var t in lista)
            {
                Console.WriteLine("--------------------------------------------------");
                Console.WriteLine(t.ToString());
            }
            Console.WriteLine("--------------------------------------------------");
        }

        static int LerInteiro(string mensagem)
        {
            int valor;
            Console.Write(mensagem);
            while (!int.TryParse(Console.ReadLine(), out valor))
            {
                Console.Write("Valor inválido. Digite um número inteiro: ");
            }
            return valor;
        }

        static string LerString(string mensagem)
        {
            Console.Write(mensagem);
            string entrada = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(entrada))
            {
                Console.Write("O campo não pode ser vazio. Digite novamente: ");
                entrada = Console.ReadLine();
            }
            return entrada;
        }
    }
}