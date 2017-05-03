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
    public partial class pesquisa_produto : Form
    {
        public pesquisa_produto()
        {
            InitializeComponent();
        }

        private void cadastroToolStripMenuItem_Click(object sender, EventArgs e)
        {

            cadastro_produto Cadastro = new cadastro_produto();

            Cadastro.Show();

        }

        private void btn_pesq_Click(object sender, EventArgs e)
        {
          //  string sintaxe = "INSERT INTO products (nome_products,description,marca,valor,categoria,foto) values (@nome,@description,@marca,@valor,@categoria,@foto)";

            string string_conexao = "Server=localhost; Database=base; Uid=root; Pwd=;";

            MySqlConnection CONEXAO = new MySqlConnection(string_conexao);

            string sintaxe = "SELECT * FROM products where nome_products = @nome";

            MySqlCommand comando_buscar = new MySqlCommand(sintaxe, CONEXAO);

            MySqlDataReader meu_reader;
            try
            {
                CONEXAO.Open();

                comando_buscar.Parameters.Add(new MySqlParameter("@nome", txt_nome.Text));

                meu_reader = comando_buscar.ExecuteReader();

                while (meu_reader.Read())
                {

                    byte[] imagem = (byte[])(meu_reader["foto"]);


                    if(imagem == null)
                    {

                        pictureBox1.Image = null;

                    }
                    else
                    {

                        MemoryStream mstream = new MemoryStream(imagem);

                        pictureBox1.Image = System.Drawing.Image.FromStream(mstream);

                        MessageBox.Show("Produto encontrado!");

                    }

                }

                

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
    }
}
