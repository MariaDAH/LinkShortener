# Link Shortener
URL shortening is used to create shorter aliases for long URLs.
 ## Functional Requirements:
* Given a URL, our service should generate a shorter and unique alias of it. This link should be short enough to be easily copied and pasted into applications.
* When users access a short link, the service should redirect them to the original link.
* Users can optionally be able to pick a custom short link for their URL (QR code or text). 
* Links will expire after a standard default timespan. (1 month)
* To be able to generate shortlink from any client it is needed an Api_key. Web API works separately from the UI and can be exposed to many clients.
## Non functional requirements: 
* The system should be highly available. This is required because, if the service is down, all the URL redirections will start failing.
* URL redirection should happen in real-time with minimal latency.
* Shortened links should not be guessable (not predictable). (Use hash algorithm).
## Diagrams: 
Components:
![Components](https://github.com/MariaDAH/LinkShortener/assets/5183628/97e128d7-d029-45e5-a85d-084ff93e2af7)
Sequence: 
![ShareLinkTypeText](https://github.com/MariaDAH/LinkShortener/assets/5183628/db9bf4c9-9582-4219-b893-5b94a39f1cc7)
## Tech:
* Blazor Web Assembly Standalone
* ASP.NetCore Web API
* Entity Framework Core 8
* Sql Server 2022
## Database Model
![ER-Diagram](https://github.com/MariaDAH/LinkShortener/assets/5183628/aa05928c-83c5-48a3-9c39-0cf7768c242b)
## Cache 
It was considered using cache database like Redis to store non used hash keys. In reality, I would like to create a separate servide to generate the hash, using a hashAlgoritm but because of limited time I just relied on EF documentation and assuming a property annotated with [key] will be unique.
```A key serves as a unique identifier for each entity instance. Most entities in EF have a single key, which maps to the concept of a primary key in relational databases (for entities without keys, see Keyless entities). Entities can have additional keys beyond the primary key (see Alternate Keys for more information).```
However, looking at SQL server schema definition of the table generated using a code first approach, looks like EF is not generating such a key with any of the known mechanisms for generating subrrogate keys. In this case I was expenting see a PK generated with a UNIQUEIDENTIFIER mechanism but is not the case. Nonetheless please assume the PK will be generated uniquely. 
## UI 
<img width="428" alt="image" src="https://github.com/MariaDAH/LinkShortener/assets/5183628/96fd4481-8d96-42a7-a191-f9ce6903c282">
