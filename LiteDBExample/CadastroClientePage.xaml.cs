using LiteDBExample.Modelos;

namespace LiteDBExample;

public partial class CadastroClientePage : ContentPage
{
  //--------------------------------------------------------------------------------------------------
  // Esse atributo "cliente" serve para a ListaClientes informar qual Cliente foi clicado na lista.
  // Será usado para preencher as Entry's com os dados do Cliente, assim como para ser enviado para o 
  // ClienteControle que irá criar/atualizar o Banco de Dados
  public Cliente cliente { get; set; }
  Controles.ClienteControle clienteControle = new Controles.ClienteControle();
  Controles.EstadoControle estadoControle = new Controles.EstadoControle();

  //--------------------------------------------------------------------------------------------------

	public CadastroClientePage()
	{
		InitializeComponent();
    pickerEstado.ItemsSource = estadoControle.LerTodos();
	}

  //--------------------------------------------------------------------------------------------------

  void VoltarClicked(object sender, EventArgs e)
  {
    Application.Current.MainPage = new ListaClientesPage();
  }

  //--------------------------------------------------------------------------------------------------
  // OnAppearing -> Quando Aparecer: Esse método é executado quando a página é mostrada.
  // Nesse caso ao aparecer usaremos o "cliente" para preencher as Entry's
  protected override void OnAppearing()
  {
    base.OnAppearing();

    if (cliente != null)
    {
      IdLabel.Text        = cliente.Id.ToString();
      NomeEntry.Text      = cliente.Nome;
      SobrenomeEntry.Text = cliente.Sobrenome;
      TelefoneEntry.Text  = cliente.Telefone;
      pickerEstado.SelectedItem = cliente.Estado;
    }
  }

  //--------------------------------------------------------------------------------------------------
  // Método para limpar os dados da Entry
  private async void OnApagarClienteClicked(object sender, EventArgs e)
  {
    // Verifica se estamos editando um cliente ou criando um cliente
    // Se estiver criando, não se pode apagar, já que não se tem um `cliente.Id`
    if (cliente == null || cliente.Id < 1)
      await DisplayAlert("Erro", "Nenhum cliente para excluir", "ok");
    else if (await DisplayAlert("Excluir","Tem certeza que deseja excluir esse cliente?","Excluir Cliente","cancelar")) // Caso o usuário tocar no Botão "Excluir Cliente"
    {
      // Apaga do Banco de Dados
      clienteControle.Apagar(cliente.Id);
      // Volta para a tela de Lista
      // Esse código abaixo pode ser um:
      // await NavigationPage.PopAsync();
      // Se você veio pra cá com um 
      // await Navigation.PushAsync(new CadastroClientePage);
      Application.Current.MainPage = new ListaClientesPage(); 
    }
  }

  //--------------------------------------------------------------------------------------------------

  private async void OnSalvarDadosClicked(object sender, EventArgs e)
  {
    if (await VerificaSeDadosEstaoCorretos()) // Verifica se os dados são válidos antes de salvar no banco
    {
      // O código abaixo preenche o objeto cliente (Modelo) com os dados das Entry's
      var cliente = new Modelos.Cliente();
      if (!String.IsNullOrEmpty(IdLabel.Text))
        cliente.Id      = int.Parse(IdLabel.Text);
      else
        cliente.Id      = 0;
      cliente.Nome      = NomeEntry.Text;
      cliente.Sobrenome = SobrenomeEntry.Text;
      cliente.Telefone  = TelefoneEntry.Text;
      cliente.Estado    = pickerEstado.SelectedItem as Estado;

      // Com o objeto preenchido enviamos para o controle para criar/atualizar no Banco de Dados
      clienteControle.CriarOuAtualizar(cliente);
      // Mostra a mensagem de sucesso
      await DisplayAlert("Salvar", "Dados salvos com sucesso!", "OK");
    }
  }

  //--------------------------------------------------------------------------------------------------
  // Esse método pode ser escrito de várias maneiras. A idéia é que você valide os dados antes de 
  // preencher o objeto (Modelo). 
  // Perceba que além da retornar false (para indicar erro), também mostra qual o erro
  private async Task<bool> VerificaSeDadosEstaoCorretos()
  {
    // Verifica se a Entry do Nome está vazia
    if (String.IsNullOrEmpty(NomeEntry.Text))
    {
      await DisplayAlert("Cadastrar", "O campo Nome é obrigatório", "OK");
      return false;
    }
    // Verifica se a Entry do Sobrenome está vazia
    else if (String.IsNullOrEmpty(SobrenomeEntry.Text))
    {
      await DisplayAlert("Cadastrar", "O campo Sobrenome é obrigatório", "OK");
      return false;
    }
    // Verifica se a Entry do Telefone está vazia
    else if (String.IsNullOrEmpty(TelefoneEntry.Text))
    {
      await DisplayAlert("Cadastrar", "O campo Telefone é obrigatório", "OK");
      return false;
    }
    else
      return true;
  }

  //--------------------------------------------------------------------------------------------------
}

