# Auto Peças Concorrente ERP — Arquitetura consolidada Release 1.0

## Camadas
Desktop WPF/MVVM → Services/Application → Core/Domain → Data/EF Core → SQLite.
Integrações externas são adaptadores: Fiscal/SEFAZ, impressão, exportação e futuras APIs. O domínio não depende da internet.

## Módulos consolidados
Segurança e perfis; Dashboard; Clientes/veículos; Fornecedores; Produtos/aplicações; Compras/cotação; Estoque/reposição; PDV; Fiscal; Financeiro/caixa; Oficina; CRM; Relatórios; Auditoria; Backup/Restore; Atualização do banco; Instalador.

## Fluxos críticos
Compra: reposição → cotação → pedido → recebimento → custo → estoque → contas a pagar.
Venda: cliente → carrinho → autorização de desconto → venda → estoque → contas a receber → fiscal.
Oficina: cliente/veículo → OS → peças/serviços → estoque → financeiro.
Fiscal: venda → preparação → provider → SEFAZ → protocolo/XML/DANFE/eventos.
Upgrade: backup pré-upgrade → migração transacional → registro de versão → inicialização.

## Regras de release
Nenhuma versão é “produção” sem build Release em Windows, testes integrados, teste de upgrade sobre banco anterior e homologação fiscal. Banco do cliente nunca deve ser apagado por atualização.
