using AutoPecasERP.Core.Entities; using AutoPecasERP.Data.Context; using Microsoft.EntityFrameworkCore;
namespace AutoPecasERP.Services.Vendas;
public record ItemVendaInput(int ProdutoId, decimal Quantidade, decimal ValorUnitario, decimal Desconto);
public class VendaService {
 readonly ErpDbContext _db; public VendaService(ErpDbContext db)=>_db=db;
 public async Task<Venda> FinalizarAsync(int? clienteId,int usuarioId,string vendedor,string pagamento,decimal desconto,IEnumerable<ItemVendaInput> entradas){
  var itens=entradas.Where(x=>x.Quantidade>0).ToList(); if(itens.Count==0) throw new InvalidOperationException("Inclua ao menos um produto.");
  await using var tx=await _db.Database.BeginTransactionAsync();
  var venda=new Venda{Numero=$"VD-{DateTime.Now:yyyyMMddHHmmss}",ClienteId=clienteId,UsuarioId=usuarioId,Vendedor=vendedor,FormaPagamento=pagamento,Desconto=desconto};
  foreach(var i in itens){
   var p=await _db.Produtos.SingleAsync(x=>x.Id==i.ProdutoId);
   if(p.EstoqueAtual<i.Quantidade) throw new InvalidOperationException($"Estoque insuficiente: {p.Descricao}. Saldo: {p.EstoqueAtual}.");
   var total=(i.Quantidade*i.ValorUnitario)-i.Desconto; venda.Itens.Add(new ItemVenda{ProdutoId=p.Id,Descricao=p.Descricao,Quantidade=i.Quantidade,ValorUnitario=i.ValorUnitario,Desconto=i.Desconto,Total=total});
   venda.Subtotal+=i.Quantidade*i.ValorUnitario; var anterior=p.EstoqueAtual;p.EstoqueAtual-=i.Quantidade;
   _db.MovimentacoesEstoque.Add(new MovimentacaoEstoque{ProdutoId=p.Id,Tipo="SAIDA_VENDA",Quantidade=i.Quantidade,SaldoAnterior=anterior,SaldoPosterior=p.EstoqueAtual,Documento=venda.Numero,Usuario=vendedor});
  }
  venda.Total=Math.Max(0,venda.Subtotal-venda.Desconto-venda.Itens.Sum(x=>x.Desconto)); _db.Vendas.Add(venda); await _db.SaveChangesAsync();
  _db.ContasReceber.Add(new ContaReceber{VendaId=venda.Id,ClienteId=clienteId,Descricao=$"Venda {venda.Numero}",Valor=venda.Total,FormaPagamento=pagamento,Status="ABERTO"});
  _db.Auditorias.Add(new Auditoria{Data=DateTime.Now,Usuario=vendedor,Acao="VENDA_FINALIZADA",Entidade="Venda",Referencia=venda.Numero,Detalhes=$"{venda.Numero} - R$ {venda.Total:N2}"});
  await _db.SaveChangesAsync(); await tx.CommitAsync(); return venda;
 }
public record ItemInput(int ProdutoId,decimal Quantidade,decimal PrecoUnitario,decimal Desconto);
public async Task<Venda> FinalizarAsync(int clienteId,IReadOnlyCollection<ItemInput> entradas,decimal descontoVenda,string forma,string vendedor,string usuario){
if(entradas.Count==0)throw new InvalidOperationException("Venda sem itens.");
await using var tx=await _db.Database.BeginTransactionAsync();
var venda=new Venda{Numero=await ProximoNumeroAsync(),ClienteId=clienteId,Data=DateTime.Now,FormaPagamento=forma,Vendedor=vendedor,Status="FINALIZADA"};
decimal subtotal=0;
foreach(var e in entradas){if(e.Quantidade<=0)throw new InvalidOperationException("Quantidade inválida.");var p=await _db.Produtos.SingleAsync(x=>x.Id==e.ProdutoId);if(p.EstoqueAtual<e.Quantidade)throw new InvalidOperationException($"Estoque insuficiente: {p.Descricao}. Disponível: {p.EstoqueAtual:N2}");var total=Math.Max(0,e.Quantidade*e.PrecoUnitario-e.Desconto);subtotal+=total;venda.Itens.Add(new ItemVenda{ProdutoId=p.Id,Quantidade=e.Quantidade,PrecoUnitario=e.PrecoUnitario,Desconto=e.Desconto,Total=total});}
if(descontoVenda<0||descontoVenda>subtotal)throw new InvalidOperationException("Desconto inválido.");venda.Subtotal=subtotal;venda.Desconto=descontoVenda;venda.Total=subtotal-descontoVenda;_db.Vendas.Add(venda);await _db.SaveChangesAsync();
foreach(var i in venda.Itens){var p=await _db.Produtos.SingleAsync(x=>x.Id==i.ProdutoId);var antes=p.EstoqueAtual;p.EstoqueAtual-=i.Quantidade;_db.MovimentacoesEstoque.Add(new MovimentacaoEstoque{ProdutoId=p.Id,Tipo="SAIDA",Quantidade=i.Quantidade,SaldoAnterior=antes,SaldoPosterior=p.EstoqueAtual,Documento=venda.Numero,Observacao="Venda PDV"});}
_db.ContasReceber.Add(new ContaReceber{VendaId=venda.Id,ClienteId=clienteId,Descricao=$"Venda {venda.Numero}",Valor=venda.Total,Vencimento=DateTime.Today,Status="ABERTO"});
_db.Auditorias.Add(new Auditoria{Usuario=usuario,Acao="VENDA_FINALIZADA",Entidade="Venda",Referencia=venda.Numero,Detalhes=$"Total {venda.Total:C}; {venda.Itens.Count} itens"});
await _db.SaveChangesAsync();await tx.CommitAsync();return venda;}
}