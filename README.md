## The Task

Create WebApp 
 simple grid, read info to the page
 SITE (Angular) -> API -> DB (SalesOrderDetails + JOINS)
 - Best Practices
 - Search box + button
 - Responsive
 - Paging

## Work log

Download DB
Update SQL Version (had only SQL 14, needed 15)

### 5:20 PM started 
Create Solution
 - Create API (Log/OpenAPI/Error Handling/Redis?)
 - Create AppService (Mapster)
 - Create Domain
 - Create Repository (Dapper)

### 7:30 PM - first commit, API working with DB

Starting work with UI (Agnular)
 - Create UI (Angular)

Create hello world, add material components, add textbox and button

### 8:30 PM 
- Dinner breack

### 9 pm 
- Back to work

### 10:20 pm 
- get request from server returning data

### 12 am 
- almost everything working on Angular side - missing error handling on client side, but gave up, still need to fix pagination

### 1 am 
- pagination done

### 1:20 am 
- fix minor bugs on server side + minor refactor in pagination on client

### 1:35 am 
- fix error handling on client!

### 1:42 am 
- found minor bugs / last commit

## Considerations

## UI

### AppService

- Layer created for business logic / mapping from domain to view model not used that much in this sample

### Repository

- Base repository could have a base execute method code for better logging and simplicity of use by repository

- Project could have folder for better organization, but found unnecessary due the amount of classes

### Tests

- Tried to add tests, but lack of time, too much Angular research
