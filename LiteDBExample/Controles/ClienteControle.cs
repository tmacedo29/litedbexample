
using LiteDBExample.Modelos;

namespace Controles;

public class ClienteControle : BaseControle
{
  //----------------------------------------------------------------------------

  public ClienteControle() : base()
  {
    NomeDaTabela = "Clientes";
  }

  //----------------------------------------------------------------------------

  public virtual Registro? Ler(int idCliente)
  {
    var collection = liteDB.GetCollection<Cliente>(NomeDaTabela);
    return collection.FindOne(d => d.Id == idCliente);
  }

  //----------------------------------------------------------------------------

  public virtual List<Cliente>? LerTodos()
  {
    var collection = liteDB.GetCollection<Cliente>(NomeDaTabela);
    return new List<Cliente>(collection.FindAll().OrderBy(d => d.Nome));
  }

  //----------------------------------------------------------------------------

  public virtual void Apagar(int idCliente)
  {
    var collection = liteDB.GetCollection<Cliente>(NomeDaTabela);
    collection.Delete(idCliente);
  }

  //----------------------------------------------------------------------------

  public virtual void CriarOuAtualizar(Cliente cliente)
  {
    var collection = liteDB.GetCollection<Cliente>(NomeDaTabela);
    collection.Upsert(cliente);
  }

  //----------------------------------------------------------------------------
}