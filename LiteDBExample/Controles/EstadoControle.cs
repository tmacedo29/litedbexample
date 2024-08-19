
using LiteDBExample.Modelos;

namespace Controles;

public class EstadoControle : BaseControle
{
  //----------------------------------------------------------------------------

  public EstadoControle() : base()
  {
    NomeDaTabela = "Estados";
  }

  //----------------------------------------------------------------------------

  public virtual Registro? Ler(int idEstado)
  {
    var collection = liteDB.GetCollection<Cliente>(NomeDaTabela);
    return collection.FindOne(d => d.Id == idEstado);
  }

  //----------------------------------------------------------------------------

  public virtual List<Estado>? LerTodos()
  {
    var tabela = liteDB.GetCollection<Estado>(NomeDaTabela);
    return new List<Estado>(tabela.FindAll());
  }

  //----------------------------------------------------------------------------

  public virtual void Apagar(int idEstado)
  {
    var collection = liteDB.GetCollection<Estado>(NomeDaTabela);
    collection.Delete(idEstado);
  }

  //----------------------------------------------------------------------------

  public virtual void CriarOuAtualizar(Estado estado)
  {
    var collection = liteDB.GetCollection<Estado>(NomeDaTabela);
    collection.Upsert(estado);
  }

  //----------------------------------------------------------------------------
}