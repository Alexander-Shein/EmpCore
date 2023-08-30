# Solution Architecture Core Libs

I use Distributed Event-Based Clean Domain Centric Arhitecture including:
1) Domain Driven Design
2) CQRS patterns
3) 2 DataBases: for ReadOnly and OLTP 

Note: There're no layers for QueryStack. It returns data fast with Dapper
Note: DomainStack uses EntityFrameworkCore
Note: Redis Distributed Cache is configured
Note: CAP with Azure Message Bus is configured to communicate between Microservices
Note: Azure AD with OAuth2 + OpenID protocols using JWT totens is configured
Note: Rate Limiting is configured
Note: Docker containers with Azure DevOps Pipelines are configured to deploy containers to Azure Container Registry and Azure Kubenetes Service
Note: 

Two Microservices are implemented:
BlogPostManagement: https://github.com/Alexander-Shein/BlogPostManagement
CommentManagement: https://github.com/Alexander-Shein/CommentManagement

BlogPostManagement.Contracts repository with events: https://github.com/Alexander-Shein/BlogPostManagement.Contracts
