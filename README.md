# Student Management System

Este é um sistema para gerenciar informações de alunos, incluindo suas notas e status de aprovação. O projeto é dividido em dois principais componentes: o **backend** (API em ASP.NET Core) e o **frontend** (aplicação em ReactJS com uso do `webpack`).

## Arquitetura do Projeto

O projeto segue a seguinte estrutura de pastas:

### Backend (API)

- **Controllers**: Contém os controladores da API.
- **Models**: Contém os modelos de dados, como `Student`.
- **Services**: Contém a lógica de negócio, como serviços de manipulação dos alunos.
- **Repositories**: Contém a implementação de repositórios para acessar os dados (neste caso, um repositório baseado em arquivo CSV).
  
### Frontend (ReactJS)

- **src/components**: Contém os componentes React que gerenciam a interface do usuário, como `StudentList` e `StudentDetails`.
- **src/services**: Contém funções para se comunicar com a API do backend.

## Tecnologias Utilizadas

### Backend
- **ASP.NET Core**: Framework para construir a API.
- **C#**: Linguagem usada no backend.
- **Swagger**: Para documentação da API.

### Frontend
- **ReactJS**: Biblioteca para construção de interfaces de usuário.
- **Reactstrap**: Biblioteca para componentes de UI com base no Bootstrap.
- **Webpack**: Para empacotamento e configuração do build do frontend.
- **Axios**: Para fazer requisições HTTP à API.

## Pré-requisitos

Antes de começar, você precisará ter as seguintes ferramentas instaladas:

- [.NET SDK](https://dotnet.microsoft.com/download) (para o backend)
- [Node.js](https://nodejs.org/) (para o frontend)
- [npm](https://www.npmjs.com/) (gerenciador de pacotes JavaScript)
  
## Configuração do Backend (API)

1. Clone o repositório do backend.
2. Navegue até a pasta do projeto `StudentManagementAPI`.
3. Execute o seguinte comando para restaurar as dependências:

    ```bash
    dotnet restore
    ```

4. Após restaurar as dependências, rode o projeto:

    ```bash
    dotnet run
    ```

5. A API estará rodando em `https://localhost:7292`.

## Configuração do Frontend (ReactJS)

1. Clone o repositório do frontend.
2. Navegue até a pasta do projeto `StudentManagementFront`.
3. Instale as dependências necessárias:

    ```bash
    npm install
    ```

4. Para rodar o frontend, execute:

    ```bash
    npm start
    ```

5. A aplicação React estará rodando em `http://localhost:3000`.

## Como Testar

### Backend
- Use o Swagger para testar os endpoints da API. O Swagger estará disponível em `https://localhost:7292/swagger` quando a API estiver rodando.

### Frontend
1. No navegador, acesse `http://localhost:3000`.
2. Você verá a lista de alunos.
3. Selecione a estratégia de ordenação e visualize os detalhes de um aluno ao clicar no botão "Ver Detalhes".
4. Navegue para a lista de melhores alunos por matéria, clicando no botão "Melhores Alunos por Matéria".

## Endpoints da API

- **GET `/api/student/sorted?strategy={strategy}`**: Retorna a lista de alunos ordenada de acordo com a estratégia de ordenação. A estratégia pode ser `0` (Bubble Sort) ou `1` (LINQ Sort).
- **GET `/api/student/{registration}`**: Retorna os detalhes de um aluno baseado na matrícula.
- **GET `/api/student/best-by-subject`**: Retorna a lista dos melhores alunos por matéria.

## Considerações

- O sistema foi desenvolvido com foco na simplicidade e clareza.
- A interface foi construída utilizando ReactJS com o auxílio do `reactstrap` para garantir uma interface de usuário moderna e responsiva.
- A comunicação entre frontend e backend é feita via API RESTful utilizando o Axios para requisições HTTP.

## Contribuições

Contribuições são bem-vindas! Caso queira contribuir com melhorias ou correções, sinta-se à vontade para abrir um pull request.

## Licença

Este projeto está licenciado sob a [MIT License](LICENSE).
