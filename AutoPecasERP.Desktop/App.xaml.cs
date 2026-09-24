using System.Windows; using AutoPecasERP.Data.Context; using AutoPecasERP.Services.Auth;
namespace AutoPecasERP.Desktop;
public partial class App:Application { protected override async void OnStartup(StartupEventArgs e){base.OnStartup(e);var db=new ErpDbContext();var auth=new AuthService(db);await auth.InicializarAsync();var login=new Views.LoginWindow(auth);login.Show();} }
