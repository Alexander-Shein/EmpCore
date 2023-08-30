# Solution Architecture Core Libs

I use Distributed Event-Based Clean Domain Centric Arhitecture including:
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
