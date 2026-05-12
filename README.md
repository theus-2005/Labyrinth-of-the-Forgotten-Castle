# 🏰 Labyrinth of the Forgotten Castle

> Trabalho acadêmico desenvolvido para o **Unigame** — Unity maze game com mecânicas de lógica, iluminação e controles embaralhados.

---

## 🎮 Sobre o Jogo

**Labyrinth of the Forgotten Castle** é um jogo de labirinto em 2D feito na Unity. O jogador precisa explorar o labirinto e descobrir a lógica por trás de **5 botões** e **5 portas** espalhadas pelo mapa.

As portas são visualmente indistinguíveis das paredes — cabe ao jogador descobrir onde estão e como abri-las.

---

## 🔀 Mecânica dos Botões e Portas

A cada nova partida, as conexões entre botões e portas são **geradas aleatoriamente**:

- Cada **botão** está ligado a **2 portas**
- Cada **porta** está conectada a **2 botões**
- Ao ativar um botão: se a porta estiver **fechada**, ela **abre** — se estiver **aberta**, ela **fecha**

---

## ⚙️ Dificuldades

| Dificuldade | Descrição |
|-------------|-----------|
| 🟢 **Normal** | Labirinto iluminado, controles normais |
| 🟡 **Médio** | Mapa escuro com iluminação ao redor do jogador e das tochas espalhadas pelo labirinto |
| 🔴 **Difícil** | Mapa escuro + embaralhamento aleatório dos 4 direcionais de movimento em intervalos de tempo |

> No modo Difícil os controles não são simplesmente invertidos — os 4 direcionais (cima, baixo, esquerda, direita) são trocados de forma completamente aleatória.

---

## 🛠️ Tecnologias

- **Engine:** Unity
- **Linguagem:** C#

---

## 👥 Equipe

Projeto desenvolvido por 3 integrantes como trabalho acadêmico para o **Unigame**.
