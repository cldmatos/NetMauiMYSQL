using MySqlConnector;

namespace TrabalhoMauiSQL.Models
{
    public class Grupos : Conectar
    {
        public int id { get; set; }
        public string grupo { get; set; }
        public string detalhe { get; set; }
        public string email { get; set; }
        public string senha { get; set; }


        public List<Grupos> listaGrupos = new List<Grupos>();

        public Grupos() { }

        public bool Grupos_Add(string grupo, string detalhe)
        {

            if (!Conexao())
            {
                return false;
            }


            StrQuery = "INSERT INTO TBGRUPO (grupo, detalhe) VALUES (@grupo, @detalhe)";
            Cmd = new MySqlCommand(StrQuery, Conn);
            Cmd.Parameters.AddWithValue("@grupo", grupo);
            Cmd.Parameters.AddWithValue("@detalhe", detalhe);
            try
            {
                Cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        // retornar os times cadastrados no banco de dados em uma lista

        public bool Grupos_Consulta()
        {
            if (!Conexao())
            {
                return false;
            }

            StrQuery = "SELECT * FROM TBGRUPO order by grupo";
            MySqlCommand cmd = new MySqlCommand(StrQuery, Conn);
            cmd.CommandType = System.Data.CommandType.Text;
            Dr = cmd.ExecuteReader();
            listaGrupos.Clear();
            while (Dr.Read())
            {
                listaGrupos.Add(new Grupos
                {
                    id = int.Parse(Dr["id"].ToString()),
                    grupo = Dr["grupo"].ToString() + " - " + Dr["detalhe"].ToString(),
                }
                              );
            }

            return true;

        }










    }
}
