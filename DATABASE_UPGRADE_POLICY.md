# Política de atualização do banco
1. Nunca apagar/recriar banco do cliente durante update.
2. Toda mudança de schema recebe número de versão.
3. Antes de migrar, criar backup `PreUpgrade`.
4. Cada versão roda em transação e grava `VersoesBanco`.
5. Seeds devem ser idempotentes.
6. Aplicativo mais antigo deve recusar banco de versão superior.
7. Migração deve ser testada em cópia de banco real antes de liberar instalador.
8. `EnsureCreated` serve apenas para primeira instalação; evolução posterior deve ocorrer por migrações versionadas.
9. Restauração continua fora do processo do ERP.
