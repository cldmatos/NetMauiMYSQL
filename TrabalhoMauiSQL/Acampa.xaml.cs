
using TrabalhoMauiSQL.Models;

namespace TrabalhoMauiSQL;

public partial class Acampa : ContentPage
{

    Conectar banco = new Conectar();
    Grupos grupos = new Grupos();

    public Acampa()
    {
        InitializeComponent();
        if (banco.Conexao())
        {
            lblStatus.Text = "Banco conectado com Sucesso !";
            if (grupos.Grupos_Consulta())
            {
                lstGrupos.ItemsSource = grupos.listaGrupos;
            }
        }
        else
        {
            lblStatus.Text = banco.conexao_status;
        }
    }

    private void btnAdicionar(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtGrupos.Text))
        {
            DisplayAlert("Atenção", "Preencha o nome do Grupo", "OK");
            return;
        }

        grupos.Grupos_Add(txtGrupos.Text, txtDetalhe.Text);

        if (grupos.Grupos_Consulta())
        {
            lstGrupos.ItemsSource = null;
            lstGrupos.ItemsSource = grupos.listaGrupos;
            // lstTimesFutebol.IsRefreshing = true;
            txtGrupos.Text = "";
            txtDetalhe.Text = "";
        }


    }
}