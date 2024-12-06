# Trabalho Tópicos 3 - A2
Descrição do Projeto
Esta API foi desenvolvida para gerenciar informações relacionadas a aviação, incluindo aeronaves, tripulações, rotas, pilotos, copilotos e comissários. A aplicação foi construída utilizando ASP.NET Core, empregando boas práticas como DTOs (Data Transfer Objects) para separar as entidades de domínio das transferências de dados.

O sistema suporta operações CRUD para todas as entidades principais e implementa validações para garantir a consistência dos dados.

O sistema mantém o mesmo padrão de relacionamentos da avaliação A1.

## Validações
DTOs: Utilizados para transferir dados, garantindo que somente os campos necessários sejam manipulados.
Atributos Customizados:
[Required]: Para campos obrigatórios.
[Range]: Para validar valores numéricos dentro de um intervalo.
[StringLength]: Para restringir o tamanho de strings.

## Funcionalidades

- **Cadastro de Pilotos**:Registro de informações dos pilotos que conduzem os voos.
- **Cadastro de Comissários**: Facilita o registro de comissários de bordo.
- **Gerenciamento de Tripulação**: Cada tripulação é composta por um piloto, um copiloto e vários comissários. Os comissários podem ser associados a múltiplas tripulações.
- **Cadastro de Aviões**: Registre as informações dos aviões disponíveis para uso, além de relacionar o avião a uma tripulação
- **Gerenciamento de Rotas**: Cada rota pode estar associada a um avião, enquanto um avião pode ser utilizado em várias rotas.


## Passo a passo para rodar
Realizar o clone do repositório e iniciar com https, não há necessidade de conectar a nenhum banco de dados, pois está sendo usado o "InMemoryDatabase", que é basicamente um banco local.
