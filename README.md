## Short Description of Project
Project is written in C# utilizing .net 9.0.102 c# version 13

## Running
Run `docker compose up --build` to start the service on `localhost:5000`
After which you can hit the two routes from
GET http://localhost:5000/receipts/{id}/process
And
POST http://localhost:5000/receipts/process
- With the request body being the receipt object.
