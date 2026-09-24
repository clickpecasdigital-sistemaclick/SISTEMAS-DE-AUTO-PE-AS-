namespace AutoPecasERP.Core.Entities;
public class ItemVenda {
 public int Id {get;set;} public int VendaId {get;set;} public Venda? Venda {get;set;}
 public int ProdutoId {get;set;} public Produto? Produto {get;set;} public string Descricao {get;set;}="";
 public decimal Quantidade {get;set;} public decimal ValorUnitario {get;set;} public decimal Desconto {get;set;}
 public decimal Total {get;set;}
}