using LiteDB;

namespace LiteDBExample.Modelos;

public class Estado : Registro
{
  [BsonId]
  public int Id { get; set; }
  public string Nome {get; set ;}
}