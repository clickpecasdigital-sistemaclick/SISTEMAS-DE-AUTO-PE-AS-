using AutoPecasERP.Core.Models;using AutoPecasERP.Data.Context;using Microsoft.EntityFrameworkCore;
namespace AutoPecasERP.Services.Compras;
public class ReposicaoService{readonly ErpDbContext _db;public ReposicaoService(ErpDbContext db)=>_db=db;
public async Task<List<ReposicaoView>> SugestoesAsync()=>await _db.Produtos.Where(x=>x.Ativo&&x.EstoqueAtual<=x.EstoqueMinimo).OrderBy(x=>x.Descricao).Select(x=>new ReposicaoView{ProdutoId=x.Id,Codigo=x.CodigoInterno,Produto=x.Descricao,EstoqueAtual=x.EstoqueAtual,EstoqueMinimo=x.EstoqueMinimo,EstoqueMaximo=x.EstoqueMaximo,Custo=x.PrecoCusto}).ToListAsync();
}