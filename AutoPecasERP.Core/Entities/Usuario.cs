namespace AutoPecasERP.Core.Entities;
public enum PerfilUsuario{Admin,Gerente,Vendedor,Estoquista,Financeiro}
public class Usuario{public int Id{get;set;}public string Login{get;set;}="";public string Nome{get;set;}="";public string SenhaHash{get;set;}="";public PerfilUsuario Perfil{get;set;}=PerfilUsuario.Vendedor;public bool Ativo{get;set;}=true;}