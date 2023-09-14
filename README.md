# Solution Architecture Core Libs

Distributed Event-Based CQRS Clean Domain Centric Arhitecture

# Clean Architecture
`The Clean Architecture` is used because it's the latest from the series of layered architectures. Layered architecture development timeline:
`Classic 3 Layers DataBase Centric` -> `Hexagonal Domain Centric` -> `Onion Domain Centric` -> `Clean Domain Centric Architecture`

Clean architecture diagram:
![1](https://github.com/Alexander-Shein/EmpCore/assets/7516186/034a001f-61b5-44ee-989e-a825df1c9b61)

There're 6 solution folders in this repository:

# Domain solution folder
This folder contains projects for `Central Domain Layer`. This layer contains all the business logic. This is a heart of software.

Domain models must be encapsulated and always in a valid state.

There're base classes for the following patterns from `Domain Driven Design`: `Value Objects`, `Entities`, `Aggregate Roots`, `Domain Events` etc.
- `Value Objects` (VO): Immutable objects compared by values. They can't be changed. If 2 value objects have the same values - they are equal. VO has no Id.
- `Entities`: An entity has an identifier. For instance a car can be an entity which is identified by its license plate. The difference with VO is if for example you change a car color from red to green it stays the same object with the same identifier. Color is VO in this case.
- `Aggregate Roots`: An Aggregate Root is an entity which encapsulates behavior for a cluster of entities, value objects. It combines them to a single indivisible object. It means if you delete an aggregate root all the child entities must be deleted as well. Only an Aggregate Root can be loaded from repository.
- `Domain Event`: this is a response to commands from `Domain Layer`. You can subscribe to these events to add more behavior. Ussualy `Domain Events` are used to communicate between different `Aggregate Roots`. It follows Open-Closed principle from Solid. If you need to publish a domain event outside of a `Microservice` you need to map a `Domain Event` to an `IntegrationEvent` and send it via `IMessageBus` from `Infrastructure Layer`. It uses `Azure Message Bus` so other `Microservices` can subscribe to it.

`Domain` project contains some helper classes like `Result` and `Failure`. We need these classes in order to avoid return result or error and to avoid throwing exceptions when business rules are violated. Because throwing exceptions when business rules are violated is a bad practice. 

# Application solution folder
This folder contains the next layer from `Clean Layers`: `Aplication Layer` which contains use cases.
Use cases ussualy are `commands` and `queries` following `CQRS` patterns.

This layer can handle `Integration Events` from other services as well. `Integration events` are used to communicate between `Microservices`. Domain events are used to connect parts inside a Microservice. If you need to publish an event outside you need to map `Domain Event` to `Integration Event` and send it via `IMessageBus`.

`Application layer` uses `IMediatr` library to send `Commands` and `Queries`.

If a domain event is raised we can handle it in application layer and map to an integration event and send it to Azure Message Topic so other services can subscribe to it.

# Infrastructure solution folder
This folder contains projects for `Infrastructure Layer`. It contains implementation details of external services like DataBase repositories, File Storages, APIs, Message Bus etc.
(P.S. Ussualy for DataBase repositories a new project is created but actually it isn't an another layer. It's a `Infrastructure Layer` as well. Because it's big so ussualy it's better to create a new project for it.)

It contains base classes for a repository pattern from `Domain Driven Design`. A `Repository` can load or save `Aggregate Roots` only. 
It contains `IUnitOfWork` patten as well.

There's a IMessageBus implementation: `CAP library` which uses `Azure Message Bus` to publish/subscribe `integration events` between `Microservices`.

# Presentation solution folder
This folder contains `Presentation Layer` projects. Here you can find projects for a WebApi but it can be gRPC, UI, WCF etc.
The responsibility of the `PresentationLayer` is to map http requests to `Commands` and `Queries` and send them to the `Application Layer`.

It also contains middleware for WebApi: `RateLimiting`, `Security` (Azure AD with OAuth2 + OpenID protocols using JWT totens), `Slugify`, `WebApiVersioning` urls etc.

# Crosscutting solution folder
This folder contains projects with services which are used across all the layers. For example `caching`, `logging` etc.

`Redis Distributed Cache` is configured.

There're generic extention methods for microsoft `IDistributedCache` interface to set and get objects instead of strings and binaries.

# QueryStack solution folder
This folder contains projects for `Queries`. In the current implementation the `CQRS` patterns are applied which means that commands and queries are splitted. I use abstractions from the layered arhitecture for `DomainStack` for `commands`. For `QueryStack` we don't need complex layers. We just need to return data as fast as possible. `QueryStack` goes outside of the arhitecture without any layers.

- `DomainStack` uses EntityFrameworkCore in repositories
- `QueryStack` uses fast Dapper

P.S. Because of using `CQRS` it allows me to use different connection strings for `QueryStack` queries and `DomainStack` repositories. It allows to have 2 different DataBases: OLTPDataBase for cammands and ReadOnlyDataBase for queries. I can significantly improve perfomance without changing code.

# Docker containers with Azure DevOps Pipelines are configured to deploy containers to Azure Container Registry and Azure Kubenetes Service

![2023-09-11_13-51-34](https://github.com/Alexander-Shein/EmpCore/assets/7516186/26192087-5cb6-47eb-ae9f-66704b9b0574)

Two Microservices are implemented:

# BlogPostManagement
Details: https://github.com/Alexander-Shein/BlogPostManagement

Deployed here: https://blog-post-management.polandcentral.cloudapp.azure.com/swagger/index.html

# CommentManagement
Details: https://github.com/Alexander-Shein/CommentManagement

Deployed here: https://comment-management.polandcentral.cloudapp.azure.com/swagger/index.html

# BlogPostManagement.Contracts

This repository contains BlogPost Integration Events: https://github.com/Alexander-Shein/BlogPostManagement.Contracts. CommentManagement Service is subscribed to these events.
