# SimplePaint

Aplicação desktop desenvolvida em C# com Windows Forms (.NET Framework 4.7.2) que permite ao usuário desenhar livremente utilizando o mouse, selecionar cores, ajustar espessura do traço, utilizar borracha e salvar o desenho em diferentes formatos de imagem.

Este projeto foi desenvolvido como parte dos estudos realizados no curso **Introdução ao C# – Uma Abordagem Prática – Módulo III**, disponível gratuitamente na plataforma Udemy, com o objetivo de consolidar conceitos fundamentais de desenvolvimento com Windows Forms.

---

## Sobre o Projeto

O SimplePaint é uma aplicação gráfica simples inspirada em ferramentas básicas de pintura.  
O projeto foi desenvolvido com foco em:

- Manipulação de eventos do mouse  
- Uso da classe `Graphics`  
- Manipulação de `Bitmap`  
- Salvamento de imagens em diferentes formatos  
- Interação com `ColorDialog` e `SaveFileDialog`  
- Estruturação de aplicações Windows Forms  

---

## Funcionalidades

### Desenho Livre

- Desenho realizado com o mouse
- Pintura contínua enquanto o botão esquerdo estiver pressionado
- Renderização simultânea:
  - No painel da aplicação
  - Na imagem interna utilizada para salvar

---

### Seleção de Cor

- Escolha da cor da caneta via `ColorDialog`
- Atualização visual do botão com a cor selecionada

---

### Controle de Espessura

- Seleção de espessura via `ComboBox`
- Valores de 2 até 100
- Atualização dinâmica durante o desenho

---

### Borracha

- Ativação e desativação por botão
- Utiliza a cor de fundo do painel como padrão
- Clique direito permite alterar a cor da borracha

---

### Limpar Tela

- Confirmação antes de limpar
- Remove o conteúdo do painel
- Reinicia a imagem interna usada para salvamento

---

### Salvar Desenho

- Utiliza `SaveFileDialog`
- Permite salvar nos formatos:
  - PNG
  - JPEG
- Salva corretamente o conteúdo desenhado

---

## Tecnologias Utilizadas

- C#
- Windows Forms
- .NET Framework 4.7.2
- System.Drawing
