Rodando a Aplicação com Docker e Docker Compose:
--------------------------------------------------
Pré-requisitos:
- Docker instalado (https://docs.docker.com/get-docker/)
- Docker Compose (já vem com Docker Desktop)
--------------------------------------------------
1. Build da aplicação com Docker:
docker build -t smartcityapp .
docker run -p 5000:80 smartcityapp
--------------------------------------------------
2. Rodar com Docker Compose:
docker-compose up --build
--------------------------------------------------
Serviços criados:
- smartcityapp (API ASP.NET Core)

Acessando a API:
--------------------------------------------------
http://localhost:5000
--------------------------------------------------
Variáveis de ambiente utilizadas:

Aplicação:
ASPNETCORE_ENVIRONMENT=Development
ConnectionStrings__DefaultConnection=User Id=rm552998;Password=050404;;Data Source=oracle.fiap.com.br:1521/ORCL;

Banco de dados:
- Utiliza o banco Oracle da FIAP, já hospedado externamente.
- Não é necessário rodar banco local via Docker.


--------------------------------------------------
Parar tudo:
docker-compose down