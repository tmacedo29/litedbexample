using LiteDBExample.Modelos;

namespace LiteDBExample;
public partial class ListaClientesPage : ContentPage
{
  Controles.ClienteControle clienteControle = new Controles.ClienteControle();

  public ListaClientesPage()
	{
		InitializeComponent();

    ListaClientes.ItemsSource = clienteControle.LerTodos();
	}

  void Josefina(object sender, SelectedItemChangedEventArgs e)
  {
    var page = new CadastroClientePage();
    page.cliente = e.SelectedItem as Cliente;
    Application.Current.MainPage = page;
  }

}