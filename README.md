# Juno

API backend desenvolvida como projeto pessoal para aprofundamento em **C#, .NET, desenvolvimento de APIs e organização de aplicações em camadas**.

O projeto foi utilizado para praticar conceitos como autenticação, regras de negócio, persistência de dados, separação de responsabilidades e relacionamento entre usuários e organizações.

## Tecnologias

* C#
* .NET 10
* ASP.NET Core Web API
* SQL Server
* Entity Framework Core
* JWT Authentication
* Scalar / OpenAPI
* Git

## Arquitetura do Backend

O backend está dividido em quatro projetos principais:

```text
Backend/
├── Juno.API/
├── Juno.Application/
├── Juno.Domain/
└── Juno.Infrastructure/
```

### Juno.Domain

Responsável pelos elementos centrais do domínio da aplicação.

A camada contém:

* Entidades
* Interfaces
* Enums
* Validadores
* Exceções
* Projeções
* Argumentos utilizados pelas regras de domínio

Entre as entidades presentes no projeto estão:

```text
User
Office
OfficeMembership
Client
```

### Juno.Application

Responsável pelas regras e serviços de aplicação, fazendo a comunicação entre a API, o domínio e a infraestrutura.

A estrutura inclui:

* Services
* DTOs
* Interfaces
* Mappers
* Configurações
* Tratamento de exceções

Entre os serviços implementados estão:

```text
AuthService
UserService
OfficeService
OfficeMembershipService
```

### Juno.Infrastructure

Responsável pela persistência e acesso aos dados.

Inclui:

* Entity Framework Core
* SQL Server
* Repositórios
* Migrations
* Serviços de infraestrutura
* Configuração do contexto de banco de dados

### Juno.API

Camada responsável pela exposição dos endpoints HTTP da aplicação.

Atualmente possui controllers para:

```text
AuthController
UserController
OfficeController
OfficeMembershipController
```

## Funcionalidades implementadas

Durante o desenvolvimento foram trabalhadas funcionalidades como:

* Cadastro e gerenciamento de usuários
* Autenticação de usuários
* Autenticação baseada em JWT
* Cadastro e gerenciamento de escritórios
* Associação de usuários a escritórios
* Persistência dos dados utilizando Entity Framework Core
* Organização das regras de negócio em serviços e domínio
* Documentação e teste dos endpoints da API

## Banco de Dados

O projeto utiliza **SQL Server** com **Entity Framework Core**.

Para desenvolvimento local, a aplicação está configurada para utilizar SQL Server LocalDB com o banco:

```text
Juno
```

As migrations estão organizadas na camada `Juno.Infrastructure`.

## Autenticação

A API utiliza autenticação baseada em **JWT Bearer**.

A chave utilizada para geração e validação dos tokens não é armazenada no repositório público e deve ser configurada localmente durante a execução da aplicação.

## Organização e Separação de Responsabilidades

O projeto foi estruturado buscando separar responsabilidades entre:

* Exposição da API
* Regras de aplicação
* Domínio
* Persistência de dados

Essa organização foi utilizada como exercício prático para aprofundar conhecimentos em arquitetura backend, orientação a objetos, injeção de dependência e manutenção de código organizado.

## Sobre este Repositório

Este repositório é uma versão pública destinada ao meu **portfólio de desenvolvimento backend**.

Ele demonstra conhecimentos aplicados em:

* C# e .NET
* APIs REST
* Orientação a objetos
* SQL Server
* Entity Framework Core
* Autenticação JWT
* Organização em camadas
* Regras de negócio
* Git
* Desenvolvimento backend
