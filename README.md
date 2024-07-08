### A) How to run as application:
- start docker
- once docker is running open powershell and change directory to where docker-compose.yml file is located i.e. cd C:\Users\xxx\Desktop\git\documents
- type docker-compose up
- application is running on port 5000

### B) How to run with visual studio for development purposes (only tested with version 2022):
- open solution
- configure startup project to Single startup project "docker-compose"
- choose only available profile "Docker compose"
- run the application, application is running on port 5000

### How requirements were accomplished:

1. Decided to go with classic onion architecture with rest web api

2. Use Accept header for specifing response format when retrieving the document. Currently supported formats: "application/xml", "application/x-msgpack", "application/json"
  
3. Providing support for new formats will be as easy as creating new serializer in 'SerializationHelper' class and add use it in the controller.

4. I think change of underlying storage rather means to separate data access layer. In case we would like to change storage it is as simple as implement new Data access layer in 'Infrastrucutre' project and inject such implementation of repository to IDocumentRepository which is used in 'DocumentService'. I decided to go with no sql database which is ideal use case for documents storage, since we do not need any relations. 

5. Implemented in memory cache in 'DocumentService' for retrieving documents. In case performance is still not enough, and we are willing to spend additional effort possibilities are endless. For example we could extract projects to separate microservices, implement redis cache, add loadbalancer, that would enable us to scale both vertically and horizontaly as needed.

6. Wrote few tests in Domain.Tests project for classes DocumentService and SerializationHelpers, seems only place worth unit testing

7. Used build in data annotation functionality to validate user inputs. We also return corresponding error/sucess messages from controller, i.e. document we try to create already exists
