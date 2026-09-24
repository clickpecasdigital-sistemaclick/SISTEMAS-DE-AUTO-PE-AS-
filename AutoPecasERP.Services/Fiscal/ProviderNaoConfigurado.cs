using AutoPecasERP.Core.Entities;
namespace AutoPecasERP.Services.Fiscal;
public class ProviderNaoConfigurado:IFiscalProvider{
 FiscalResult R()=>new(false,"PROVEDOR_NAO_CONFIGURADO","","","","Configure um provedor fiscal real para comunicação com a SEFAZ.");
 public Task<FiscalResult> EmitirAsync(NotaFiscal n,ConfiguracaoFiscal c,CancellationToken ct=default)=>Task.FromResult(R());
 public Task<FiscalResult> ConsultarAsync(NotaFiscal n,ConfiguracaoFiscal c,CancellationToken ct=default)=>Task.FromResult(R());
 public Task<FiscalResult> CancelarAsync(NotaFiscal n,ConfiguracaoFiscal c,string j,CancellationToken ct=default)=>Task.FromResult(R());
 public Task<FiscalResult> CartaCorrecaoAsync(NotaFiscal n,ConfiguracaoFiscal c,string x,CancellationToken ct=default)=>Task.FromResult(R());}