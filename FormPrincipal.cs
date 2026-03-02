using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimplePaint
{
    public partial class FormPrincipal : Form
    {
        private bool flagPintar = false; // Para controlar quando se deve pintar
        private Graphics graphicsPainelPintura;
        private float espessuraCaneta;
        private Color corBorracha = Color.White; // Define variavel corBorracha
        private bool flagApagar = false;
        private Image imagemASalvar;
        private Graphics graphicsImagemASalvar;

        public FormPrincipal()
        {
            InitializeComponent();

            // As propriedades abaixo só fazem efeito quando a propriedade do botão FlatStyle == Flat
            buttonBorracha.FlatAppearance.MouseOverBackColor = Color.DarkSlateGray; // Altera a cor do botão quando o cursor hover por cima
            buttonSalvar.FlatAppearance.MouseOverBackColor = Color.DarkSlateGray; // Altera a cor do botão quando o cursor hover por cima
            buttonLimpar.FlatAppearance.MouseOverBackColor = Color.DarkSlateGray; // Altera a cor do botão quando o cursor hover por cima

            for (int i = 2; i <= 100; i += 2) // Irá preencher comboBox de 2 até 100 com intervalos contando de 2 em 2
            {
                comboBoxEspessuraDaCaneta.Items.Add(i);
            }
            comboBoxEspessuraDaCaneta.Text = "10"; // Texto padrão, que irá aparecer na comboBox
            comboBoxEspessuraDaCaneta.IntegralHeight = false; // Necessário para que seja possível definir a quantidade de itens exibidos na comboBox
            comboBoxEspessuraDaCaneta.MaxDropDownItems = 5; // Define o número de itens a serem exibidos na comboBox

            graphicsPainelPintura = panelPintura.CreateGraphics(); // O graphics permite o desenho sobre o controle
            espessuraCaneta = float.Parse(comboBoxEspessuraDaCaneta.Text); // tipo.Parse() converte texto da comboBox para float, definindo o tamnaho do texto
            corBorracha = panelPintura.BackColor; // Especifica a cor padrão da borracha para a mesma da cor de fundo do painel.

            imagemASalvar = new Bitmap(panelPintura.Width, panelPintura.Height); // Imagem para salvar
            graphicsImagemASalvar = Graphics.FromImage(imagemASalvar);           // extraindo graphics da imagem para salvar de forma a podermos desenhar nela
            graphicsImagemASalvar.Clear(panelPintura.BackColor); // Preenchemos a imagem com a cor do fundo do painel
            buttonBorracha.ForeColor = Color.White;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var saveFileDialog = new SaveFileDialog(); // Janela para salvar arquivos
            saveFileDialog.Filter = "Portable Network Graphics|.png|Arquivo JPEG|.jpeg"; // Atribuindo formatos de imagem para o usuário escolher
            if(saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                switch(saveFileDialog.FilterIndex)
                {
                    case 1: 
                        imagemASalvar.Save(saveFileDialog.FileName, ImageFormat.Png);
                        break;
                    case 2:
                        imagemASalvar.Save(saveFileDialog.FileName, ImageFormat.Jpeg);
                        break;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Tem certeza disso? Todo o desenho será apagado", "Apagar desenho", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                graphicsPainelPintura.Clear(Color.White); // Limpa todo o desenho e preenche novamente com o fundo branco
                graphicsImagemASalvar = Graphics.FromImage(imagemASalvar);
                graphicsImagemASalvar.Clear(panelPintura.BackColor);
            }
        }


        // Ocorre quando é feita uma escolha de item pelo usuário na comboBox (quando o index muda, no for)
        private void comboBoxEspessuraDaCaneta_SelectedIndexChanged(object sender, EventArgs e)
        {
            espessuraCaneta = float.Parse(comboBoxEspessuraDaCaneta.SelectedItem.ToString());
        }

        private void buttonCorCaneta_Click(object sender, EventArgs e) // Handler do evneto de click do botão
        {                                                              // É disparado quando o botão é clicado, Serve para manipularmos o que acontece quando o clique ocorre
            var colorDialog = new ColorDialog();                // ColorDialog é uma caixa de seleção de cores
            var corEscolhida = colorDialog.ShowDialog();        // Exibe na forma modal- aplicação fica travada enquanto o diálogo não é resolvido
            if (corEscolhida == DialogResult.OK)
            {
                buttonCorCaneta.BackColor = colorDialog.Color;  // Alteramos a cor do botão para a cor escolhida pelo usuário
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void panelPintura_MouseDown(object sender, MouseEventArgs e)
        {
            flagPintar = true; // Define o momento em que o usuário pressiona o botão do mouse
        }

        private void panelPintura_MouseMove(object sender, MouseEventArgs e)
        {
            if (flagPintar)
            {
                if (!flagApagar)
                {
                    graphicsPainelPintura.DrawEllipse(new Pen(buttonCorCaneta.BackColor, espessuraCaneta), new RectangleF(e.X, e.Y, espessuraCaneta, espessuraCaneta)); // Primeiro, definimos o desenho que queremos. Neste caso, uma Elipse (bola). Após, criamos uma caneta, definimos a cor dela através da variável criada anteriormente, além da espessura da caneta, criamos um retângulo, que será utilizado para definir onde a bolinha será desenhada. Para isso, definimos X, Y e as dimensões do retângulo, que são as mesmas da bola a ser desenhada.

                    // Desenhando na imagem para salvar
                    graphicsImagemASalvar.DrawEllipse(new Pen(buttonCorCaneta.BackColor, espessuraCaneta), new RectangleF(e.X, e.Y, espessuraCaneta, espessuraCaneta)); // Primeiro, definimos o desenho que queremos. Neste caso, uma Elipse (bola). Após, criamos uma caneta, definimos a cor dela através da variável criada anteriormente, além da espessura da caneta, criamos um retângulo, que será utilizado para definir onde a bolinha será desenhada. Para isso, definimos X, Y e as dimensões do retângulo, que são as mesmas da bola a ser desenhada.
                    buttonBorracha.ForeColor = Color.White;
                }
                else
                {
                    graphicsPainelPintura.DrawRectangle(new Pen(corBorracha, espessuraCaneta), new Rectangle(e.X, e.Y, (int)espessuraCaneta, (int)espessuraCaneta)); // Transformamos variável tipo float para tipo inteiro
                    buttonBorracha.ForeColor = Color.Black;
                }

            }
        }

        private void panelPintura_MouseUp(object sender, MouseEventArgs e)
        {
            flagPintar = false; // Define o momento em que o usuário solta o botão do mouse
        }

        private void buttonBorracha_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right) // Se clicar com o botão direito do mouse sobre o botão borracha
            {
                var colorDialog = new ColorDialog();
                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    corBorracha = colorDialog.Color; // Seleciona a cor da borracha de acordo com a escolha do usuário
                }
            }
            else
            {
                if (!flagApagar) // O operador ! antes irá inverter o valor de flagApagar
                {
                    flagApagar = true;
                    buttonBorracha.BackColor = corBorracha; // Cor do botão da borracha será a mesma da borracha                }
                }
                else
                {
                    flagApagar = false;
                    buttonBorracha.BackColor = Color.Black;
                }
            }
        }

        private void buttonBorracha_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void panelPintura_Resize(object sender, EventArgs e) // Evento disparado sempre que o painel é redimensionado
        {
            graphicsPainelPintura = panelPintura.CreateGraphics(); // Atualiza referência do objeto graphics do painel
        }
    }
}
