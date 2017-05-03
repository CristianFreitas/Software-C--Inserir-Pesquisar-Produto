
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AplicacaoDeCadastro
{
    public partial class cadastro_produto : Form
    {
        public cadastro_produto()
        {
            InitializeComponent();
        }

        

        private void btn_salvar_Click(object sender, EventArgs e)
        {


            byte[] imagem_byte = null; // Conter a quantidade de bytes da imagem

            FileStream fstream = new FileStream(this.txt_imagem.Text, FileMode.Open, FileAccess.Read); // Importa o System.IO, ele e um objeto que faz transição de dados, busca de dado, armazenagem de bytes.

            BinaryReader br = new BinaryReader(fstream); // Leitor de dados binarios.

            imagem_byte = br.ReadBytes((int)fstream.Length); //Ler os bytes do tipo inteiro, contendo o tamanho da imagem.

            string sintaxe = "INSERT INTO products (nome_products,description,marca,valor,categoria,foto) values (@nome,@description,@marca,@valor,@categoria,@foto)";

            string string_conexao = "Server=localhost; Database=base; Uid=root; Pwd=;";

            MySqlConnection CONEXAO = new MySqlConnection(string_conexao);


            MySqlCommand comando_inserir = new MySqlCommand(sintaxe, CONEXAO);

            MySqlDataReader meu_reader;

            try
            {
                CONEXAO.Open();

                comando_inserir.Parameters.Add(new MySqlParameter("@nome", txt_nome.Text));

                comando_inserir.Parameters.Add(new MySqlParameter("@description", txt_descricao.Text));

                comando_inserir.Parameters.Add(new MySqlParameter("@marca", txt_marca.Text));

                comando_inserir.Parameters.Add(new MySqlParameter("@valor", txt_preco.Text));

                comando_inserir.Parameters.Add(new MySqlParameter("@categoria", txt_categoria.Text));

                comando_inserir.Parameters.Add(new MySqlParameter("@foto", imagem_byte));

                meu_reader = comando_inserir.ExecuteReader();

                MessageBox.Show("Produto Salvo!");

            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message.ToString());

            }
            finally
            {
                CONEXAO.Close();

            }

        }

        private void btn_inserir_imagem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog(); // Cria uma instancia para navegar nos arquivos do windows.
            dialog.Filter = "JPG Files(*.jpg)|*.jpg|PNG Files(*.png)|*.png|AllFiles(*.*)|*.*"; // Filtragem dos arquivos selecionados.

            if (dialog.ShowDialog() == DialogResult.OK)  // Se a imagem foi selecionada ele entrará no if.
            {
                string foto = dialog.FileName.ToString(); // Variavel string, ela vai conter o diretorio da imagem selecionada.
                txt_imagem.Text = foto; // aparecerá no textbox o caminho da imagem selecionado.
                pictureBox1.ImageLocation = foto;
            }
        }



        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void cadastro_produto_Load(object sender, EventArgs e)
        {

        }

        private void pesquisaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pesquisa_produto Pesquisa = new pesquisa_produto();

            Pesquisa.Show();
        }
    }
}
