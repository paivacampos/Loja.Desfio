# Loja.Desfio
API REST Para Loja
#
Requisitos para o funcionamento:

.NET Core 3.1.14

Links de acesso: https://dotnet.microsoft.com/download/dotnet/3.1

SQL-Server (Express)

Link de acesso: https://www.microsoft.com/pt-br/sql-server/sql-server-downloads

Visual Studio 2019

Link de acesso: https://visualstudio.microsoft.com/pt-br/vs/
#
Descritivo do sistema:

Tecnologia utilizada: ASP.NET Core 3.1.14

Bancdo de dados: SQL-SERVER Express
#
Para executar locamente a aplicação deve-se

01 - Utilizar o visual studio 2019 (já que só contem os fontes)

02 - Abrir a solção : Loja.Desfio.sln (arquivo dentro do diretório clonado / baixado)

03 - Aguardar o download dos packages necessários para iniciar o processo

04 - Trocar a string de conexão em : appsettings.json (existem 2 perfis)

04.1 - appsettings.Development.json : este roda aplicação localmente

4.1.1 - Trocar a string de conexão do banco, para as configurações do banco instalado

4.2 - appsettings.Production.json

4.2.1 - Trocar a string de conexão do banco, para as configurações do banco instalado

5 - Acessar dentro do visual studio Package Manager Console 

5.1 - Caso não o tenha na barra inferior do visual studio, ir no Menu do Topo -> Tools -> Nuget Package Manager -> Package Manager Console (clicando)

6 - Selecione no menu dropdown Default Project : API.Desafio

6.1 - As migrations do identity já estão criadas

6.2 - Atualize o banco de dados com o commando: Update-Database -Verbose -Context ApplicationDbContext

6.3 - As migrations da API já estão criadas

6.4 - Atualize o banco de dados com o commando: Update-Database -Verbose -Context DbAPIContext

Com os passos acima feitos está pronto para se rodar a aplicação localmente.
