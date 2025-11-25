Compreendido. O README deve servir como um documento de intenção e planejamento, descrevendo o projeto que **será desenvolvido**, em vez de um relatório do que já está pronto.

Vou reajustar o README para o repositório Frontend, mantendo o foco nos objetivos e tecnologias que **pretendemos implementar** (Unity/C#), mas sem afirmar que o código ou as funcionalidades já existem.

---

## 🎮 Frontend - Projeto Mines Academy (Unity/C#)

Este repositório está dedicado ao desenvolvimento da camada **Frontend** do Projeto Mines Academy, utilizando **Unity** e **C#**. Nosso objetivo é construir a interface gráfica e a experiência de usuário completa para o jogo single-player "Mines", focando na sincronização visual e animada com os dados e eventos em tempo real fornecidos pelo backend.

---

## 🚀 Escopo e Tecnologias

### 🎯 Nosso Foco

O desenvolvimento do frontend será concentrado em traduzir a lógica de jogo do backend em uma experiência visual fluida, reativa e interativa.

* **Unity:** Utilização da engine para construção de cenas, UI/UX e gerenciamento de estados.
* **C#:** Implementação da lógica de comunicação, controle de jogo e manipulação de elementos visuais.
* **Comunicação Híbrida:** Integração de chamadas REST para ações pontuais e WebSocket para atualizações em tempo real.

### 🛠️ Tecnologias Chave que Serão Utilizadas

| Componente | Uso Pretendido |
| :--- | :--- |
| **UnityWebRequest** | Realizar requisições **REST** (iniciar partida, enviar passo, obter saldo). |
| **WebSocket Client** | Manter uma conexão persistente para receber eventos em tempo real do backend. |
| **Unity UI** | Criação das interfaces de usuário (saldo, aposta, tela de jogo, vitória/derrota). |
| **Mecanim / Animações** | Sincronizar efeitos visuais e animações (pisada, explosão, vitória) com os eventos recebidos. |

---

## 🏗️ Estrutura de Implementação (Planejamento)

Abaixo, detalhamos as áreas de desenvolvimento e os objetivos para cada componente principal do frontend.

### 1. Comunicação e Sincronização

A prioridade é estabelecer a comunicação bidirecional com o backend. O frontend será projetado para ser **"reagente"**, ou seja, ele tomará ações com base nas mensagens recebidas, e não por lógica interna do jogo.

* **REST (Ações):** Implementaremos o consumo dos endpoints REST para funcionalidades como autenticação, consulta de saldo, **início da partida (`/game/start`)** e **envio de passos (`/game/step`)**.
* **WebSocket (Eventos):** Estabeleceremos a conexão após o início da partida para receber em tempo real os eventos cruciais:
    * `GAME_STARTED`: Para configurar o visual do tabuleiro.
    * `STEP_RESULT`: Para atualizar o progresso do jogador.
    * `GAME_WIN` / `GAME_LOSE`: Para encerrar a partida e exibir o resultado final.
    * `BALANCE_UPDATED`: Para refletir a atualização do saldo.

### 2. UI/UX e Fluxo de Jogo

As seguintes telas e interações serão desenvolvidas para guiar o jogador através do fluxo da partida.

* **Tela Inicial/Saldo:** Permitirá a visualização do saldo atual e a definição do valor da aposta, seguida pelo acionamento do `POST /game/start`.
* **Tela do Jogo:** Conterá os elementos visuais das casas (células) e o botão "Avançar". O estado visual (quantas casas foram "pisadas") será mantido e atualizado com base nos eventos `STEP_RESULT`.
* **Telas Finais:** Exibição clara e imediata das telas de **Vitória** (com o prêmio) e **Derrota** (com a mina revelada, se possível), acionadas pelos eventos `GAME_WIN` e `GAME_LOSE`.

### 3. Animações e Feedback Visual

O feedback visual é crucial para a experiência do jogo.

* **Animação de Sucesso:** Uma animação clara indicará o sucesso de cada passo seguro (`STEP_RESULT`).
* **Animação de Perda:** Uma animação de **explosão** será acionada imediatamente ao receber o evento `GAME_LOSE`.
* **Animação de Saldo:** Efeitos visuais serão utilizados para dar destaque à atualização do saldo na tela após vitórias.

---

## 🛠️ Próximos Passos (Próximas Sprints)

1.  Estruturar as cenas base no Unity.
2.  Implementar o `NetworkManager` para consumir os endpoints REST de Saldo e Autenticação.
3.  Implementar o `WebSocketClient` para estabelecer a conexão e processar o evento `GAME_STARTED`.