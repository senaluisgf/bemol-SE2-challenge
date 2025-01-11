<!-- # Programming Challenge -->

## Documentação
- **Necessário ter .NET 8 ou superior**
---
### Inicialização da Aplicação
#### Instalação de dependências
##### Serviço A
1. Clone o repositório.
1. Usando o visual studio, entre na pasta do projeto.
1. Usando a interface, basta selecionar e compilar o serviço A
1. É importante que altere as variáveis **_AccessKey_**, **_SecretKey_** e **_QueueUrl_** para poder fazer o uso adequado do serviço (que está fazendo requisições para a AmazonSQS)

##### Serviço B
1. Usando o terminal, entre na pasta do projeto.
1. Usando o terminal, entre na basta **frontend** e execute o comando ```npm install```
1. Assim como no serviço A, é importante que altere as variáveis **_AccessKey_**, **_SecretKey_** e **_QueueUrl_** para poder fazer o uso adequado do serviço (que está fazendo requisições para a AmazonSQS) 

##### Mongo
1. Usando o terminal, entre na pasta do projeto.
1. Utilize o comando ```docker compose up -d```, ele irá subir uma imagem docker do mongodb contendo uma database chamado ***bemol_db*** e uma coleção chamada ***payments***
---
### Executar a Aplicação
##### Serviço A
1. Após você iniciar o serviço A.
1. O navegador se abrirá no endereço [https://localhost:32820/swagger/index.html](https://localhost:32820/swagger/index.html).
1. Lá você encontrará e poderá executar o endpoint desenvolvido para este serviço.

##### Serviço B
1. Infelizmente quando criei o template gRPC utilizando o Visual Stúdio, o mesmo não conseguiu compilar o arquivo protobuffer
2. Por este motivo eu dei prosseguimento ao código de resolução do problema proposto, porém não tive êxito em corrigir este erro de compilação que foi gerado ao começo do serviço B
---

### Serviço A (intenção de compra)
<p align="center">
  <a href="https://dotnet.microsoft.com/pt-br/download/dotnet/8.0" target="blank"><img src="https://miro.medium.com/v2/resize:fit:1200/1*z2AhVTYkSSkQJ2lg30GGcA.png" width="310" alt=".NET 8 Logo" /></a>
  <a href="https://aws.amazon.com/pt/sqs/" target="blank"><img src="https://blog.knoldus.com/wp-content/uploads/2021/09/sqs.png" width="310" alt="AmazonSQS Logo" /></a>
</p>

#### Decisões de Projeto
- Escolhi utilizar [.Net 8](https://dotnet.microsoft.com/pt-br/download/dotnet/8.0) pois é a versão stável mais recente. Com isso podendo fazer um implementação que não sofrerá tantas mudanças a curto/longo prazo;
- Também decidi usar [AmazonSQS](https://aws.amazon.com/pt/sqs/), pois é o sistema de filas com a qual eu já tinha tido algum contato anterior ao desafio;
- Para Documentar o serviço e executar as rotas foi utilizado o [Swagger](https://swagger.io/docs/) que é executada na página inicial do Serviço([https://localhost:32820/swagger/index.html](https://localhost:32820/swagger/index.html))

#### Desafios encontrados durante o Projeto
- **Tempo longe da tecnologia** - Como eu estava trabalhando em um projeto monolítico há 3 anos. Demorei um pouco para me familiarizar novamente com o [.Net 8](https://dotnet.microsoft.com/pt-br/download/dotnet/8.0) e todo o ecossistema utilizando C#. Porém, acredito que me sai muito bem e foi uma ótima experiência poder exercitar este conhecimento novamente;

#### Rotas Desenvolvidas no Serviço A
Todas as Rotas podem ser facilmente **encontrada e executadas na página inicial do Serviço** disponibilizadas através do **Swagger** 
- **Criação de intenção de compra** - *POST https://localhost:32820/PaymentIntent* - Recebe através do *Body* da requisição uma intenção de compra e envia ela para uma fila utilizando AmazonSQS.

---
### Serviço B (processamento da compra)
<p align='center' justify='center'>
  <a href="https://dotnet.microsoft.com/pt-br/download/dotnet/8.0" target="blank"><img src="https://miro.medium.com/v2/resize:fit:1200/1*z2AhVTYkSSkQJ2lg30GGcA.png" width="210" alt=".NET 8 Logo" /></a>
  <a href="https://grpc.io/" target="blank"><img src="https://grpc.io/img/logos/grpc-icon-color.png" width="210" alt="gRPC Logo" /></a>
  <a href="https://aws.amazon.com/pt/sqs/" target="blank"><img src="https://blog.knoldus.com/wp-content/uploads/2021/09/sqs.png" width="210" alt="AmazonSQS Logo" /></a>
</p>

#### Decisões de Projeto
- As decisões foram as mesmas que as utilizadas no serviço A.


#### Desafios encontrados durante o Projeto
- **Erros de compilação do Visual Studio** - Meu maior desafio durante o desenvolvimento deste segundo serviço com certeza foi a compilação deste projeto. Mesmo utilizando o template padrão de [gRPC](https://grpc.io/) fornecido pelo Visual studio 2022,
 o compilador do protobuf não estava conseguindo encontrar o arquivo greet.proto (sim o arquivo padrão gerado pelo próprio visual studio). Depois de várias horas tentando criar vários e vários projetos, optei por dar prosseguimento a implementação
da solução mesmo não conseguindo testar efetivamente para verificar o resultado de forma concreta. Sinto que foi um pouco frustrante não conseguir completar 100% do desafio a tempo por um "erro bobo de compilação", mas acredito que este erro é devido
 a minha ausência de prática com o .net há alguns anos (como citado anteriormente);
- **Testes** - Não tive tempo ábil de realizar os mocks do serviço de aws e do banco de dados (digo isso com pesar por sou muito fã de tests unitários)

#### Rotas desenvolvidas no Serviço B
- **Status da Compra** - *GET http://localhost:porta/payments/{uuid}* - Retorna o registro da compra com o campo **_Status_** = "**Sucess**" ou um **NotFound()**
