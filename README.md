# Automata 
![robot](https://github.com/dchaves80/Automata/assets/16888179/e0d2223f-3cec-451e-86c3-f62d2f58d6bf)
#### Data Flow
```mermaid
sequenceDiagram
  actor user as User
  participant client as Game
  box midnightblue Server Side Containers
  participant server as Server API
  participant httpsql as HTTPtoSQL
  participant database as Database SQL SERVER
  end
%% 1
  user->>client: Do Action{<span>asd</span>}
  client->>client: process
%% 1
  create participant thread as THREAD
  client-->>thread:Open thread
  thread->>server:Send http request to controller
  server->server:Process request
  server->>httpsql:Send request qith sql sentence
  httpsql->httpsql:Convert request into sql sentence
  httpsql->>database:Send Query
  database->database:Process Query
%% 3
  database->>httpsql:Returns query result
  httpsql->>server:Returns query result (json)
  server->server:Process Data
  thread->>client:Store response in message queue
  destroy thread
  client->>thread:Destroy thread
  
%% 4
  loop Each message in message queue of each gameobject
    client->client:Take first message from queue
    client->client:Process Message
    client->client:Do stuff
    client->>user:Send visual updates
    client->client:Deletes first message from queue
  end
  ```



