### How to run:
- start docker
- once docker is running open powershell and change directory to where docker-compose.yml file is located i.e. cd C:\Users\xxx\Desktop\git\documents
- type docker-compose up
- application is running on port 5000

### How requirements were accomplished:
1. Decided to go with classic onion architecture with rest web api

2. & 3. Use Accept header for specifing type on GET. Providing support for new formats will be as easy as creating new serializer in 'SerializationHelper' class and add use it in the controller.

4. I decided to go with no sql database which is ideal use case for documents storage, since we do not need any relations. I think change of underlying storage rather means to separate data access layer. In case we would like to change storage it is as simple as implement new Data access layer in 'Infrastrucutre' project and inject such implementation of repository to IDocumentRepository which is used in 'DocumentService'

5. Implemented in memory cache in 'DocumentService' for retrieving documents.

6. Wrote few tests for Domain project for classes DocumentService and SerializationHelpers, seems only place worth unit testing

7. Used build in data annotation functionality to validate user inputs. We also return corresponding error/sucess messages from controller, i.e. document we try to create already exists
