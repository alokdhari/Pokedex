# Pokedex - How to run?

Steps to run the project, presuming you have docker installed. 
* Where this directory is located, run the below command to build the docker image. This might take a bit depending on if you have .net packages already.

```docker build -f .\Pokedex.Api\Dockerfile  --force-rm -t pokedex:latest .```

* Run the next command to run the docker image:

``` docker run -d -p 25000:80 pokedex -P -e "ASPNETCORE_ENVIRONMENT=Development" -e "ASPNETCORE_URLS=https://+:443;http://+:80" ```

Head to ` http://localhost:25000/pokemon/mewtwo` to verify all is well.


# What would I have done more?

* Wrap the external calls using HttpClients and add HttpClient as a dependency
* Write a lot more tests, especially mocking the services and testing the logic
* Implement rate limiting functions and better feedback to the api callers
* Implement logging
* Implement Caching abilities