using MySqlConnector;

namespace TrabalhoMauiSQL.Models
{
    public class Membros : Conectar
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Endereco { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Cep { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }

        public List<Membros> lstMembros = new List<Membros>();
        private string Url;

        public Membros()
        {

        }
        public Membros(int id, string nome, string email, string Telefone)
        {
            this.Id = id;
            this.Nome = nome;
            this.Email = email;
            this.Telefone = Telefone;

        }

        public bool Consulta(int pId)
        {
            bool Ret = false;
            if (!Conexao())
            {
                Ret = false;
                return Ret;
            }

            StrQuery = "SELECT * FROM `TBMEMBRO` WHERE id =" + pId.ToString();
            Cmd = new MySqlCommand(StrQuery, Conn);
            Dr = Cmd.ExecuteReader();
            if (Dr.Read())
            {
                Nome = Dr["nome"].ToString();
                Endereco = Dr["Endereco"].ToString();
                Bairro = Dr["Bairro"].ToString();
                Cidade = Dr["Cidade"].ToString();
                Estado = Dr["Estado"].ToString();
                Cep = Dr["Cep"].ToString();
                Telefone = Dr["Telefone"].ToString();
                Email = Dr["Email"].ToString();
                Senha = Dr["Senha"].ToString();
                Ret = true;
            }
            Dr.Close();
            Conn.Close();

            return Ret;
        }

        public bool ListaMembros(string xBusca = "")
        {
            bool Ret = false;
            if (!Conexao())
            {
                Ret = false;
                return Ret;
            }
            if (xBusca.ToString() == "")
            {
                StrQuery = "SELECT * FROM `TBMEMBRO` ORDER BY `nome`";
            }
            else
            {
                StrQuery = "SELECT * FROM `TBMEMBRO` WHERE `nome` LIKE '%" + xBusca + "%' ORDER BY `nome`";
            }
            Cmd = new MySqlCommand(StrQuery, Conn);
            Dr = Cmd.ExecuteReader();
            while (Dr.Read())
            {
                //  lstAlunos.Add(Dr["nome"].ToString());
                lstMembros.Add(
                    new Membros
                    {
                        Id = int.Parse(Dr["id"].ToString()),
                        Nome = Dr["nome"].ToString(),
                        Email = Dr["email"].ToString(),
                        Senha = Dr["senha"].ToString()
                    }
                );
            }
            Dr.Close();
            Conn.Close();
            return Ret;
        }

        public bool Excluir()
        {
            bool Ret = false;
            if (!Conexao())
            {
                return Ret;
            }

            StrQuery = "DELETE FROM TBMEMBRO WHERE id=" + Id.ToString();

            using (Cmd = new MySqlCommand(StrQuery, Conn))
            {
                try
                {
                    int i = Cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        Ret = true;
                        conexao_status = "OK";
                    }

                }
                catch (Exception ex)
                {
                    conexao_status = "Erro:" + ex.Message.ToString();
                    Ret = false;
                }
            }
            Dr.Close();
            Conn.Close();
            return true;
        }

        // salvar dados dos alunos

        public bool Salvar(bool pIncluir = false)
        {
            bool ret = false;
            if (!Conexao())
            {
                ret = false;
                return ret;
            }

            // VERIFICANDO DADOS NULOS
            if (string.IsNullOrEmpty(Url))
            {
                // Url = "https://icon-library.com/images/android-profile-icon/android-profile-icon-2.jpg";
                Url = "icon.png";
            }
            if (pIncluir)
            {
                // inclusao de dados

                StrQuery = "INSERT INTO TBMEMBRO(id,nome,endereco,bairro,cidade,estado,cep,telefone,email,senha) VALUES " +
                           "(@id,@nome,@endereco,@bairro,@cidade,@estado,@cep,@telefone,@email,@senha);";

            }
            else
            {
                // alteração de dados
                // o ID devera ser passado da View para a propriedade da Classe antes de atualizar

                StrQuery = "UPDATE TBMEMBRO SET id=@id,nome=@nome,endereco=@endereco," +
                           "bairro=@bairro,cidade=@cidade,estado=@estado,cep=@cep," +
                           "telefone=@telefone,email=@email,senha=@senha WHERE id=" + Id.ToString();

            }

            using (Cmd = new MySqlCommand(StrQuery, Conn))
            {
                if (!pIncluir)
                {
                    Cmd.Parameters.AddWithValue("@id", Id); // SE FOR ALTERAÇAO INFORMAR O CODIGO ID CORRETO

                }
                else
                {
                    Cmd.Parameters.AddWithValue("@id", 0); // SE FOR INCLUSAO MANDAR CODIGO 0
                }
                Cmd.Parameters.AddWithValue("@nome", Nome.ToString());
                Cmd.Parameters.AddWithValue("@endereco", Endereco.ToString());
                Cmd.Parameters.AddWithValue("@bairro", Bairro.ToString());
                Cmd.Parameters.AddWithValue("@cidade", Cidade.ToString());
                Cmd.Parameters.AddWithValue("@estado", Estado.ToString());
                Cmd.Parameters.AddWithValue("@cep", Cep.ToString());
                Cmd.Parameters.AddWithValue("@telefone", Telefone.ToString());
                Cmd.Parameters.AddWithValue("@email", Email.ToString());
                Cmd.Parameters.AddWithValue("@senha", Senha.ToString());
                try
                {
                    int i = Cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        ret = true;
                        conexao_status = "OK";
                    }

                }
                catch (Exception ex)
                {
                    conexao_status = "Erro:" + ex.Message.ToString();
                }


            }
            Dr.Close();
            Conn.Close();
            return ret;
        }





    }

}
