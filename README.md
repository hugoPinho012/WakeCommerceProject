# API CRUD de Produtos

Nesse repositório está a API para realizar o CRUD de produtos seguindo todos os requisitos funcionais, não funcionais e a parte bônus. É possível: 
1. Criar um produto (com o preço limitado a valores positivos);
2. Atualizar um produto;
3. Deletar um produto;
4. Listar os produtos, visualizar um produto específico, orderná-los por diferentes campos, e buscar um produto pelo nome.

No que tange à base de dados, fiz uso do SQL Server e optei pela abordagem code-first. 

Busquei incorporar elementos da Clean Architecture, contudo abri mão de alguns elementos afim de reduzir a complexidade. Assim, nessa solução se encontram 5 projetos: 
  1. Domain, que contém a definição da entidade "Produto" (obtei por incluir, além dos nome, preço e estoque uma descrição e SKU, pois julguei que poderiam ser interessantes para os consumidores dessa API);
  2. Application, que contém as abstrações que serão implementadas na camada de dados;
  3. Infra.Data, que lida com o acesso ao banco de dados;
  4. API.Tests, por fim, implementa os testes unitários e de integração, que estão em suas respectivas pastas.
  
Além disso, fiz uso de DTOs (Data Transfer Objects) e o padrão de projeto "Repository", que atua como um mediador entre o acesso ao bancos de dados afim de desacoplar essa função dos controllers e manter o código mais limpo e sustentável. Optei, também, por utilizar a língua inglesa no projeto. Os testes são realizados através do Github Actions e são ativados ao ser feito um "push" no branch "main".
