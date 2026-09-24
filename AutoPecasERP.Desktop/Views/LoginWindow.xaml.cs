using System.Windows; using AutoPecasERP.Services.Auth;
namespace AutoPecasERP.Desktop.Views;
public partial class LoginWindow:Window { readonly AuthService _auth; public LoginWindow(AuthService auth){InitializeComponent();_auth=auth;} async void Entrar_Click(object sender,RoutedEventArgs e){var u=await _auth.LoginAsync(UserBox.Text.Trim(),PassBox.Password);if(u==null){Msg.Text="Usuário/senha inválidos ou acesso temporariamente bloqueado.";return;}new MainWindow(u).Show();Close();} }
