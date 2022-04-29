# StockChat

Service created as tech-Challenge.

I used docker to run SQL server and RabbitMQ, you can use the link below to use the same image that I used.

- docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=yourStrong(!)Password" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2019-latest
- docker run -d --hostname rabbitserver --name rabbitmq-server -p 15672:15672 -p 5672:5672 rabbitmq:3-management

Inside the solution you will find 2 entrypoints 'StockChat.Web' and 'StockChatBot', one is for web and the chat management and the second one is for manage the command.
you need to run both projects to the application run in his full options.

- To Create de database I recommend to use the restore file provided in the folder 'DataBase Restore' the database name is 'StockChat'.

- Unfortunately I have some personal problems and I didn't finish everything that I want, Like Unit tests for everything, But i want to finish this project.
