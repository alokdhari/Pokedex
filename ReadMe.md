# Pokedex - How to run?

Steps to run the project, presuming you have docker installed. 

* Where this directory is located, run the below command to build the docker image. This might take a bit depending on if you have .net packages already.

```
docker build -f .\Pokedex.Api\Dockerfile  --force-rm -t pokedex:latest .
```

* Run the next command to run the docker image:

``` 
docker run -d -p 25000:80 pokedex -P -e "ASPNETCORE_ENVIRONMENT=Development" -e "ASPNETCORE_URLS=https://+:443;http://+:80" 
```

Head to ` http://localhost:25000/pokemon/mewtwo` to verify all is well.


## What would I have done more?

* Move the urls to the configuration file
* Wrap the external calls using HttpClients and add HttpClient as a dependency in the startup
* Write tests for the services
* Implement rate limiting functions and better feedback to the api callers
* Implement logging to elastic search or some other log provider
* Implement Caching for similar requests

## Feedback

Really enjoyed writing this little api. Was absolutely unaware of the pokemon world (my kid's only 2 so know more about Peppa pig and Paw Patrol :D) and the crazy translation api. 

Thank you.