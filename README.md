# BettingApi

BettingApi is a .net 8 web api that implements the required assignment.

## Tools and environments

Windows 10
Visual studio 2022
SQL server express 2019
Microsoft SQL Management studio 18

## Deliverables

1. Web Api .net 8 project with all files needed to debug. 
Configuration for DB connection is stored in appsettings.Development.json, section "ConnectionStrings", field "BettingsDB".
2. DBBackup folder of this repository.
Contains BettingsDB.bak (.bak file) to fully restore BettingsDB and script file (script.sql) to create an MS SQl instance ready to use.
3. Swagger Documentation.pdf (pdf file) to have a complete ready to read documentation. 
In order to debug and call endpoint implemented you can use Swagger UI. You can debug and call enpdpoints here: http://localhost:34955/swagger/index.html.

## Implementation and structure

### Needs to be highlighted

**Program.cs:**
1. Get connection string form appsettings.
2. Set up swagger and enable swagger annotations for endpoints used.
3. Added {api_route}/healthcheck to enable .net HealthCheck mechanism. Used to monitor downtime and other similar issues

**ApiContracts Folder:** Contains endpoint of routes.

**UnitOfWork.cs**: UnitOfWork uses with BettingsDbContext and MatchRepository through dependency injection patern. It
is used to facilitate Db operations. We can implement more fucntionality handling DB transactions and more.

**Controllers Folder:** Contains the api controllers and edpoints.
I had time to write only MatchController.cs for Match entities but similarly we would implement MatchOddController.cs.
I chose match edpoints because it was a more complete entity than match odd.
I use dependency injection patern to pass BettingLogic instance and logger.
Swagger annotations pass extra information on swagger ui.
Every enpoint/method uses data transfer objects (DTOs) for requests and responses.
Where needed, we follow those steps:
1. Input validation
2. Mapping DTOs to Application objects through MatchMappings static class. We could alo use a library mapper.
3. Call functions implemented business logic, which makes the actions needed.
4. Map again to response object, if needed.

**BusinessLogic Folder:** Contains BettingLogic.cs. UnitOfWork is used with dependency injection patern. I
added match repository there. In case we have many entities, we may implement seperate services for some repositories.

**Models Folder:**
Contains all classes describing entities, DTOs and DbContext. For bigger projects we may have seperately DbContext and Db related classes.

**Services Folder:**
Contains repositories (MatchRepository) that interact with DB (CRUD operations). MatchOddRepository.cs would be implemented in a similar way.
In GetMatches we could implement more operations such as miltifield filtering and grouping. For now only simple operations are implemented such as
paging, contains filter of Description and sorting based on MatchDate.

Thank you for your time 😃,
John
