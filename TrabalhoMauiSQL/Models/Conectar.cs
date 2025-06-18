using MySqlConnector;

namespace TrabalhoMauiSQL.Models
{
    public class Conectar
    {

        public string conexao_status { get; set; }
        public string StrQuery { get; set; }
        public string StrCon { get; set; }
        public MySqlDataReader Dr;
        public MySqlCommand Cmd;
        public MySqlConnection Conn;


        public Conectar()
        {
        }

        public bool Conexao()
        {
            //StrCon = "host=192.168.1.250;port=3306;user=fukuta;password=#abc123456;";
            MySqlConnectionStringBuilder StrCon = new MySqlConnectionStringBuilder();

            StrCon.Server = "glicguard.eastus2.cloudapp.azure.com"; // conexao interna na rede, dominio ou ip
            StrCon.Port = 3306;
            StrCon.UserID = "claudio";
            StrCon.Password = "FatecFranca123#";
            StrCon.Database = "DAVISG";

            Conn = new MySqlConnection(StrCon.ToString());
            bool ret = false;
            try
            {
                Conn.Open();
                conexao_status = "Conexão realizada com sucesso !";
                ret = true;
            }
            catch (Exception ex)
            {
                //Console.Write(ex.Message);
                conexao_status = ex.Message;
                ret = false;
            }

            return ret;
        }


    }
}
