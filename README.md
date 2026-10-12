# Instituto Federal de Educação, Ciência e Tecnologia de São Paulo — Campus Cubatão
**Curso:** Tecnologia em Análise e Desenvolvimento de Sistemas  
**Disciplina:** Estrutura de Dados 2 (CBTEDD2)  
**Semana 9:** 28/09/2026  
**Projeto 07:** Gerenciador de Projetos (Listas em C# - Console Application)

---

## Contexto do Projeto
Uma empresa gerencia diversos projetos simultaneamente. Cada projeto é composto por uma lista de tarefas contendo atributos como **prioridade**, **status**, **data de criação** e **data de conclusão**.

O sistema em modo Console permite:
- Cadastrar e gerenciar projetos e tarefas.
- Concluir, cancelar e reabrir tarefas.
- Filtrar tarefas por prioridade e status tanto localmente (em um projeto) quanto globalmente (em todos os projetos).
- Gerar relatórios e métricas percentuais consolidadas.

---

## Diagrama de Classes

```
+---------------------------------------------------+
| Tarefa                                            |
+---------------------------------------------------+
| - id: int                                         |
| - titulo: string                                  |
| - descricao: string                               |
| - prioridade: int (1=alta, 2=média, 3=baixa)      |
| - status: string ("Aberta","Fechada","Cancelada") |
| - dataCriacao: DateTime                           |
| - dataConclusao: DateTime                         |
+---------------------------------------------------+
| + concluir(): void                                |
| + cancelar(): void                                |
| + reabrir(): void                                 |
+---------------------------------------------------+

+---------------------------------------------+
| Projeto                                     |
+---------------------------------------------+
| - id: int                                   |
| - nome: string                              |
| - tarefas: List<Tarefa>                     |
+---------------------------------------------+
| + adicionarTarefa(Tarefa t): void           |
| + removerTarefa(Tarefa t): bool             |
| + buscarTarefa(Tarefa t): Tarefa            |
| + tarefasPorStatus(string s): List<Tarefa>  |
| + tarefasPorPrioridade(int p): List<Tarefa> |
| + totalAbertas(): int                       |
| + totalFechadas(): int                      |
+---------------------------------------------+

+------------------------------+
| Projetos                     |
+------------------------------+
| - itens: List<Projeto>       |
+------------------------------+
| + adicionar(Projeto p): bool |
| + remover(Projeto p): bool   |
| + buscar(Projeto p): Projeto |
| + listar(): List<Projeto>    |
+------------------------------+
```

---

## Funcionalidades do Seletor (Menu)

0. **Sair**: Encerra a aplicação.
1. **Adicionar projeto**: Cadastra um novo projeto informando ID e Nome.
2. **Pesquisar projeto**: Exibe os detalhes do projeto, tarefas agrupadas por status e totais.
3. **Remover projeto**: Exclui um projeto **somente se ele não possuir tarefas vinculadas**.
4. **Adicionar tarefa em projeto**: Cria uma tarefa vinculada a um projeto específico.
5. **Concluir tarefa**: Altera o status para `"Fechada"` e registra a data/hora de conclusão.
6. **Cancelar tarefa**: Altera o status para `"Cancelada"`.
7. **Reabrir tarefa**: Altera o status de volta para `"Aberta"`.
8. **Listar tarefas de um projeto**: Exibe todas as tarefas de um determinado projeto.
9. **Filtrar tarefas por status ou prioridade em um projeto**: Permite busca filtrada no escopo de um único projeto.
10. **Filtrar tarefas por status ou prioridade em todos os projetos**: Permite busca filtrada global.
11. **Resumo geral**: Exibe totais de projetos, tarefas por status e o **% de concluídas em relação às abertas**.

---

## Como Executar no GitHub Codespaces / Terminal

1. Abra o **GitHub Codespaces** no seu repositório.
2. Crie ou navegue até uma pasta de projeto Console .NET:
   ```bash
   dotnet new console -n GerenciadorProjetos
   cd GerenciadorProjetos
   ```
3. Substitua o conteúdo do arquivo `Program.cs` pelo código C# disponibilizado nesta solução.
4. Execute o projeto pelo terminal:
   ```bash
   dotnet run
   ```
