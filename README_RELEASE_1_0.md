# AUTO PEÇAS CONCORRENTE ERP — Release 1.0 consolidada
Este pacote substitui a sequência incremental como base principal de desenvolvimento.

Inclui arquitetura desktop .NET 8/WPF, SQLite, segurança, cadastros, compras/cotação, estoque/reposição, PDV, financeiro, oficina, CRM, relatórios, fiscal por provider, backup, versionamento do banco e scripts de publicação/instalador.

## Gerar no Windows
Execute `build\release-1.0.ps1` em Windows 10/11 x64 com .NET 8 SDK e Inno Setup 6.

## Importante
O pacote é código-fonte consolidado e pipeline de release. O instalador só deve ser chamado de pronto depois de compilado e homologado no Windows. A emissão fiscal real continua bloqueada até configuração/homologação de um provider SEFAZ real.
