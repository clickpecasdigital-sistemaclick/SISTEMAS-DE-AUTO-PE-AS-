# Status da entrega para teste
Foi feita auditoria estática adicional:
- XML de todos os XAML analisados: estrutura XML válida.
- Correspondência básica x:Class/code-behind validada.
- DbSets duplicados removidos na consolidação.
- Contrato de Produto normalizado.
- Senha do certificado A1 alterada para proteção DPAPI (CurrentUser).
- Script de um clique `TESTAR_AGORA_NO_WINDOWS.bat` incluído.

## Limitação desta execução
O ambiente que gerou este pacote não possui o comando `dotnet`, portanto não foi possível compilar nem executar WPF aqui. A validação definitiva começa ao executar `TESTAR_AGORA_NO_WINDOWS.bat` em Windows com .NET 8 SDK.

## Critério
Se o build passar, o script publica e abre `publish\win-x64\AutoPecasConcorrenteERP.exe`.
Se falhar, a saída do compilador deve ser usada para a próxima correção; não esconda nem ignore os erros.
