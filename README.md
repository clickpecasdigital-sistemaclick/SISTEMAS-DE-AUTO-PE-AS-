# Auto Peças Concorrente ERP — Fase 3

Fase 3 adiciona Compras integradas ao Estoque e Financeiro.

## Incluído
- Pedido de compra com fornecedor, itens, frete e desconto
- Recebimento de pedido
- Entrada automática no estoque
- Histórico de custo por produto
- Conta a pagar automática no recebimento
- Auditoria de criação/recebimento
- Bloqueio de recebimento duplicado
- Base preparada para XML/NF-e de entrada

## Fluxo
Fornecedor → Pedido → Recebimento → Estoque → Histórico de custo → Contas a pagar → Auditoria

## Observação de banco
A Fase 3 acrescenta tabelas ao esquema. Para instalação nova, o banco é criado normalmente pelo bootstrap existente. Para banco já usado na Fase 2, gere uma migration do EF Core antes de preservar dados em produção. Não apague banco real para atualizar esquema.

## Próxima fase
PDV/Vendas: orçamento, venda, pagamentos, baixa automática de estoque, comissão e preparação do handoff para NF-e/NFC-e.


## Fase 4 — PDV / Vendas
- PDV com cliente, busca por código/código de barras/descrição
- Formas de pagamento: dinheiro, PIX, cartão, boleto e crediário
- Baixa de estoque transacional e auditável
- Contas a receber automáticas
- Registro de auditoria
- Bloqueio por estoque insuficiente
- Fluxo pós-venda: NF-e / NFC-e / Somente venda
- Status fiscal preparado para o módulo Fiscal (sem simular autorização SEFAZ)

### Regra de consistência
A finalização usa transação no banco. Falha em estoque/financeiro reverte a venda inteira.

## Fase 5 — Fiscal
Estrutura NF-e (55) e NFC-e (65), numeração/série, configuração fiscal da empresa, ambientes homologação/produção, certificado A1, notas e eventos fiscais, XML/DANFE paths e interface desacoplada de provedor SEFAZ.

**Importante:** esta fase não falsifica autorização fiscal. `ProviderNaoConfigurado` bloqueia emissão até a integração de um provedor real. Senhas/tokens não devem permanecer em texto puro em produção; aplicar Windows DPAPI/Credential Manager.

Próximos itens fiscais de produção: validação tributária completa por CRT/UF/operação, assinatura XML, schemas vigentes, QR Code NFC-e, contingência, consulta, cancelamento, CC-e, inutilização, persistência segura de certificado e DANFE.

## Fase 6 — Financeiro
- Abertura e fechamento de caixa
- Entradas, saídas, suprimentos e sangrias
- Contas a receber originadas pelas vendas
- Baixas financeiras e movimentos de caixa vinculados
- Saldo calculado x saldo informado no fechamento
- Histórico por usuário, origem e forma de pagamento
- Correção de consistência: venda não é marcada como paga antes de sua baixa financeira.

### Evolução necessária
A interface atual usa valores demonstrativos de R$ 100 para os botões rápidos de suprimento/sangria e zero na abertura/fechamento. Na próxima etapa de acabamento esses valores devem ser digitados em diálogos validados. Contas a pagar da Fase 3 devem ser integradas ao mesmo serviço de baixas.

## Fase 7 — Oficina
- Veículos vinculados ao cliente
- Ordem de Serviço com KM, problema, diagnóstico, mecânico e status
- Itens de peça e serviço
- Totais separados de peças/serviços
- Finalização transacional
- Baixa de peças no estoque
- Conta a receber gerada uma única vez
- Auditoria da finalização
- Status: aberta, aguardando peça, em execução e finalizada

A tela desta fase fornece o gerenciamento inicial de OS. O formulário detalhado de edição de veículo/diagnóstico/itens será refinado na consolidação de UX.

## Fase 8 — CRM e Vendas Perdidas
- Leads e oportunidades
- Etapas comerciais e responsável
- Follow-up e próximo contato
- Histórico de interações
- Clientes inativos calculados pelo histórico de vendas (90 dias por padrão)
- Vendas perdidas por cliente/produto, quantidade, valor e motivo
- Estrutura pronta para relatórios de motivos de perda e recuperação de clientes

A interface inicial lista os dados; formulários completos de edição e campanhas serão refinados na fase de consolidação.

## Fase 9 — Dashboard e Relatórios
Indicadores calculados diretamente do banco:
- Faturamento do dia
- Vendas do mês
- Ticket médio
- Clientes ativos
- Produtos cadastrados
- Estoque abaixo do mínimo
- Contas a receber em aberto
- Valor registrado em vendas perdidas
- Ordens de serviço abertas
- Ranking de clientes
- Ranking de produtos por quantidade
- Evolução diária das vendas (30 dias)

A camada de serviço foi separada da UI para permitir posteriormente gráficos, impressão, PDF/Excel e filtros por filial/período/vendedor.

## Fase 10 — Segurança, Backup, Auditoria e Permissões
- Perfis e permissões por módulo/ação
- Administrador com acesso total
- Perfis iniciais: Gerente, Vendedor, Estoquista e Financeiro
- Visualização da trilha de auditoria
- Backup online consistente do SQLite usando API nativa de backup
- Verificação `PRAGMA integrity_check` após criação
- Backups em Documentos/AutoPecasERP/Backups

### Restauração segura
A restauração deliberadamente não sobrescreve o banco enquanto o ERP está aberto. Na consolidação/instalador será criado um utilitário separado de restauração, que valida o backup, fecha a aplicação e só então substitui o banco. Isso evita corrupção.

### Segurança pendente para produção
Aplicar DPAPI/Credential Manager às credenciais fiscais; política de senha; expiração opcional; bloqueio persistente; autorização de ações críticas e aplicação das permissões em cada comando/tela, não apenas no menu.

## Fase 11 — Consolidação
Correções antes do empacotamento:
- Removidos valores fixos demonstrativos de abertura, sangria, suprimento e fechamento do caixa.
- Diálogo de valores com validação e observação.
- Fechamento mostra saldo calculado, contado e diferença.
- Baixa parcial de contas a receber considera pagamentos anteriores e impede baixa acima do saldo.
- Verificação central de permissão antes de abrir módulos principais.
- Utilitário de validação básica de CPF/CNPJ e UF.
- Mantida arquitetura única: nenhuma fase virou projeto paralelo.

### Gate antes do instalador
Ainda é necessário compilar em Windows/.NET 8, executar migrations/EnsureCreated contra banco limpo, corrigir erros de build encontrados, executar testes integrados e substituir os formulários simplificados de CRM/Oficina por edição completa. A emissão SEFAZ continua dependendo de provedor fiscal real.

## Fase 12 — Build e Instalador
- Preflight PowerShell: SDK, restore e build Release.
- Publish self-contained para Windows x64.
- Script Inno Setup para gerar `AUTO PECAS CONCORRENTE ERP SETUP.EXE`.
- Checklist de homologação ponta a ponta.
- Documento de build para Windows.
- Nenhum EXE foi falsamente marcado como compilado: este pacote contém fonte e automação de build.

## Fase 13 — Release Candidate 1.0
Revisão estática de integração e correções de inconsistências entre fases. Consulte `RELEASE_CANDIDATE_1_0.md`.

## Fase 14 — Banco versionado e atualização segura
- Tabela de versão do schema.
- Backup automático pré-upgrade.
- Migrações incrementais transacionais.
- Bloqueio de aplicativo antigo contra banco mais novo.
- Seed administrativo idempotente.
- Inicialização do banco antes do login.
- Política formal de upgrade sem `EnsureDeleted`.

## Fase 15 — UX operacional
Formulários reais substituem lançamentos demonstrativos de Oficina e CRM; tema visual centralizado para padronização do desktop.

## Fase 16 — Oficina profissional
Editor operacional de OS com peças, serviços, diagnóstico, cadastro rápido de veículo, totais e finalização integrada a estoque/financeiro.

## Fase 17 — PDV profissional
PDV de balcão com carrinho multi-item, atalhos, desconto controlado, validação transacional de estoque e integração fiscal/financeira.

## Fase 18 — Financeiro profissional
Centro financeiro com CR/CP, baixas parciais, atrasos, caixa e indicadores operacionais.

## Fase 19 — Compras e Estoque
Reposição por mínimo/máximo, valor estimado e histórico de custos integrado ao ciclo de compras.

# CONSOLIDAÇÃO
A partir deste ponto use `README_RELEASE_1_0.md` e `ARQUITETURA_RELEASE_1_0.md` como referência principal. O pacote consolidado substitui a continuidade por microfases.
