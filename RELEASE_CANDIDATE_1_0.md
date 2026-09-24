# Release Candidate 1.0 — Fase 13
Correções estáticas realizadas:
- Auditoria: removido uso inexistente de `EntidadeId`; vendas/OS usam `Referencia`.
- Conta a receber: `VendaId` tornou-se opcional para suportar títulos originados por OS.
- Permissões: compatibilidade com enum `PerfilUsuario` e proteção dos módulos restantes do menu.
- Seed do gerente ampliado para Fiscal, Oficina, Fornecedores e Segurança.
- Instalador corrigido para o AssemblyName real: `AutoPecasConcorrenteERP.exe`.
- Preflight passou a executar auditoria estática antes do restore/build.

## Ainda obrigatório antes de produção
1. Rodar `build\preflight.ps1` em Windows com .NET 8.
2. Corrigir qualquer erro real de compilação/XAML que o compilador apontar.
3. Validar criação/evolução do banco; `EnsureCreated` não substitui uma estratégia de migrations para atualização de clientes.
4. Homologar o fiscal com provedor SEFAZ real.
5. Testar todos os fluxos do checklist integrado.
6. Somente depois gerar e assinar o instalador final.
