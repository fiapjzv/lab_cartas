# Game Server

Esta pasta contém o código do **servidor do jogo**.

O servidor é responsável por coordenar sessões de jogo, validar ações dos jogadores e garantir consistência do estado do
jogo em ambientes multiplayer.

## Responsabilidades

- Gerenciamento de sessões de jogo
- Autoridade sobre o estado do jogo
- Validação de ações dos jogadores
- Sincronização de estado entre clientes
- Comunicação em rede

## Objetivo

Manter o servidor como **fonte de verdade do estado do jogo**, evitando que clientes possam manipular regras ou
resultados (`hacks`).
