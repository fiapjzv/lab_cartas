# Lab Cartas

Estudo sobre renderização de cartas em Unity.

Objetivo é fazer algo inspirado em [Marvel Snap](https://marvelsnap.com/) ou [Balatro](https://www.playbalatro.com/).

---

## Estrutura do Repositório

```bash

 ├── src/                   # código fonte (projeto unity)
     ├── unity/             # apresentação do projeto
     ├── server/            # servidor autoritativo para multi-player
     └── core/              # regras básicas do jogo
 ├── prototype/             # código fonte da versão de protótipo do jogo (projeto unity)
 ├── docs/                  # documentação (GDD, guia de estilo, convenções de código, processo decisório)
 ├── todo/                  # tarefas a serem feitas ou ideias para fazer depois
 ├── refs/                  # referências e inspirações (screenshots, ideias de UX)
 └── README.md              # esse arquivo com uma visão geral do projeto
```

> [!NOTE]  
>  O objetivo de separar os projetos `unity`, `server` e `core` é manter o projeto Unity focado em **apresentação e
> interação**, enquanto a lógica principal permanece desacoplada.

## Contribuição

Use [TBD](https://trunkbaseddevelopment.com/) (Trunk Based Development) para preservar um histórico linear e todos
poderem aprender com o que você fez.

Portanto, sempre faça **rebase** antes de abrir um
[PR](https://docs.github.com/pt/pull-requests/collaborating-with-pull-requests/proposing-changes-to-your-work-with-pull-requests/about-pull-requests).

> [!WARNING]  
> Tente não fazer `push` diretamente em `main` para evitar conflitos e pra que todo mundo revise e aprove o que você
> fez. A não ser é claro que esteja todo mundo alinhado e feliz com o que foi feito. 😁

### Fluxo de trabalho

1. **Crie uma `branch`** a partir de `main`.
2. **Faça commits** das suas alterações na sua própria branch.
3. **Abra o PR**, definindo você mesmo como responsável e escolhendo o revisor apropriado.
4. **Discuta e ajuste** o código até que o revisor possa encerrar todas as discussões.
5. **Organize os commits**, preferencialmente fazendo **squash** para manter um conjunto semântico de mudanças juntas.
6. **Faça o merge** na branch para `main`
