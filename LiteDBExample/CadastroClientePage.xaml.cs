using LiteDBExample.Modelos;

namespace LiteDBExample;

public partial class CadastroClientePage : ContentPage
{
  public Cliente cliente {get ; set;}
  Controles.ClienteControle clienteControle = new Controles.ClienteControle();

	public CadastroClientePage()
	{
		InitializeComponent();
	}

  protected override void OnAppearing()
  {
    base.OnAppearing();

    if (cliente != null)
    {
      IdEntry.Text        = cliente.Id.ToString();
      NomeEntry.Text      = cliente.Nome;
      SobrenomeEntry.Text = cliente.Sobrenome;
      TelefoneEntry.Text  = cliente.Telefone;
    }
  }

  private void OnApagarDadosClicked(object sender, EventArgs e)
  {
    IdEntry.Text = string.Empty;
    NomeEntry.Text = string.Empty;
    SobrenomeEntry.Text = string.Empty;
    TelefoneEntry.Text = string.Empty;
  }

  private void OnSalvarDadosClicked(object sender, EventArgs e)
  {
    if (VerificaSeDadosEstaoCorretos())
    {
      var cliente = new Modelos.Cliente();
      cliente.Id        = int.Parse(IdEntry.Text);
      cliente.Nome      = NomeEntry.Text;
      cliente.Sobrenome = SobrenomeEntry.Text;
      cliente.Telefone  = TelefoneEntry.Text;

      clienteControle.CriarOuAtualizar(cliente);
    }
    DisplayAlert("Salvar", "Dados salvos com sucesso!", "OK");
  }

  private bool VerificaSeDadosEstaoCorretos()
  {
    if (String.IsNullOrEmpty(NomeEntry.Text))
    {
      DisplayAlert("Cadastrar", "O campo Nome é obrigatório", "OK");
      return false;
    }
    else if (String.IsNullOrEmpty(SobrenomeEntry.Text))
    {
      DisplayAlert("Cadastrar", "O campo Sobrenome é obrigatório", "OK");
      return false;
    }
    else if (String.IsNullOrEmpty(TelefoneEntry.Text))
    {
      DisplayAlert("Cadastrar", "O campo Telefone é obrigatório", "OK");
      return false;
    }
    else
      return true;
  }
}

