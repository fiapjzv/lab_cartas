# Game Core

Esta pasta contém a **lógica central e compartilhada do jogo**.

O objetivo do `core` é concentrar todas as regras e estruturas fundamentais do jogo de forma **independente de
plataforma**.

Este código deve poder ser utilizado tanto pelo cliente (`unity`) quanto pelo `server`.

> [!WARNING]  
>  O `core` **não deve depender** de **unity**

## Responsabilidades

- Regras do jogo
- Estruturas de dados do jogo
- Sistemas determinísticos
- Simulação do estado do jogo
- Modelos de entidades do jogo

## Exemplos de Conteúdo

- Classes: `Card`, `Player`, `GameState`, etc
- Regras e resoluções de ações
- Sistemas de turnos
- Simulação de partidas
- Serialização de estado

## Objetivo

Garantir que as regras do jogo sejam **centralizadas, reutilizáveis e testáveis**.
