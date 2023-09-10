# Solution Architecture Core Libs

Distributed Event-Based CQRS Clean Domain Centric Arhitecture

# Clean Architecture
`The Clean Architecture` is the latest from the series of layered architectures. Timeline:
`Classic 3 Layers DataBase Centric` -> `Hexagonal Domain Centric` -> `Onion Domain Centric` -> `Clean Domain Centric Architecture`

Clean architecture diagram:
![1](https://github.com/Alexander-Shein/EmpCore/assets/7516186/034a001f-61b5-44ee-989e-a825df1c9b61)

There're 6 solution folders in this repository:

# Domain solution folder
This folder contains projects for `Central Domain Layer`. This layer contains all business logic. This is a heart of software.

There're base classes for the following patterns from `Domain Driven Design`: `Value Objects`, `Entities`, `Aggregate Roots`, `Domain Events` etc.
- `Value Objects` (VO): Immutable objects compared by values. They can't be changed. If 2 value objects have the same values - they are equal. VO has no Id.
- `Entities`: An entity has an identifier. For instance a car can be an entity which is identified by its license plate. The difference with VO is if for example you change a car color from red to green it stays the same object with the same identifier. Color is VO in this case.
- `Aggregate Roots`: An Aggregate Root is an entity which encapsulates behavior for a cluster of entities, value objects. It combines them to a single indivisible object. It means if you delete an aggregate root all the child entities must be deleted as well. Only an Aggregate Root can be loaded from repository.
- `Domain Event`: this is a response for commands from Domain Layer. You can subscribe to these events to add more behavior. It follows Open-Closed principle from Solid. There's a posibility to map a `DomainEvent` to an `IntegrationEvent` and send it via `Azure Message Bus` so other services can subscribe to it.

`Domain` project contains some helper classes like `Result` and `Failure`. We need these classes in order to avoid return result or error and to avoid throwing exceptions when business rules are violated. Because throwing exceptions when business rules are violated is a bad practice. 

# Application solution folder
This folder contains the next layer from `Clean Layers`: `Aplication Layer` which contains software use cases.
Use cases ussualy are `commands` and `queries` following `CQRS` patterns. This layer can handle `IntegrationEvents` from other services as well.

# Infrastructure solution folder
This folder contains projects for `Infrastructure Layer`. It contains implementation details of external services like DataBases repositories, File Storages, APIs, Message Bus etc.
(P.S. Ussualy for DataBase repositories a new project is created but actually it isn't an another layer. It's a `Infrastructure Layer` as well. Because it's big so ussualy it's better to create a new project for it.)

# Presentation solution folder
This folder contains `Presentation Layer` projects. Here you can find projects for a WebApi but it can be gRPC, UI, WCF etc.

# QueryStack solution folder


1) Domain Driven Design
2) CQRS patterns
3) 2 DataBases: for ReadOnly and OLTP 

- There're no layers for QueryStack. It returns data fast with Dapper

- DomainStack uses EntityFrameworkCore

- Redis Distributed Cache is configured

- CAP with Azure Service Bus is configured to communicate between Microservices

- Azure AD with OAuth2 + OpenID protocols using JWT totens is configured

- Rate Limiting is configured

- Docker containers with Azure DevOps Pipelines are configured to deploy containers to Azure Container Registry and Azure Kubenetes Service

- Other features


Two Microservices are implemented:

- BlogPostManagement: https://github.com/Alexander-Shein/BlogPostManagement

Deployed here: http://blog-post-management.polandcentral.cloudapp.azure.com/swagger/index.html

- CommentManagement: https://github.com/Alexander-Shein/CommentManagement

Deployed here: http://comment-management.polandcentral.cloudapp.azure.com/swagger/index.html

- BlogPostManagement.Contracts repository with events: https://github.com/Alexander-Shein/BlogPostManagement.Contracts
