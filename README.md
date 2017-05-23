# Containerized Delivery with Visual Studio Team Services and Docker
On may 23 I did a session on [Techorama](http://techorama.be) on Containerized Delivery with Visual Studio Team Services and Docker.

##Repository
This repo contains the demo script, code and slides. Enjoy and please let me know what you think!

## Abstract
With Continuous Delivery 3.0, I introduced the concept of rethinking what you do instead of optimizing what you have. Containers are a very important concept in this new way of thinking. Instead of delivering your application on a server, you can also store your application and everything that is needed to run it, inside a container. But what do you need for his and how does that work?

This session will dive into the concept of Containers and will use Docker to set up and deliver images within a full automated pipeline. Of course we will instrument the pipeline with all the necessary feedback loops and quality gates to deliver to a real Container Cluster in the cloud.

# Demo Script
# Demo script Techorama - Containerized Delivery #

## Before the session ##
* Clean up docker images

```
docker stop $(docker ps -a -q)
docker rm $(docker ps -a -q)
```
* Spin up Kubernetes cluster
    * Navigate to `http://127.0.0.1:8001/ui`
* Spin up portainer

    `docker run -d -p 9000:9000 -v /var/run/docker.sock:/var/run/docker.sock portainer/portainer`

* Open CreateCluster.ps1
* Clean up Kubernetes 

`kubectl delete deployments,pods,services,replicasets --all`

* Start Up SQL Management Studio
* Start up Zoomit
* Start up local agent
* Clean up git dirs

``` 
git checkout master
git clean -f -d
```

## Demo 0 - Starting a KillerApp ##
```
docker pull rvanosnabrugge/killerapp
docker run -d -p 4242:80 rvanosnabrugge/killerapp
```

## Demo 1: Why I really like Docker ##
In this first demo I want to show how Docker can really help the developer in his daily workflow, and at the same time walk through some concepts of Docker.  I start out with the question "Who has ever installed a SQL Server on a new Machine". "And how long did that take?" And then.. WITH Docker.

`docker pull microsoft/mssql-server-linux`

`docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=Welkom1234!" -d -p 1433:1433 --name sqlserver microsoft/mssql-server-linux`

Then connect again in SQL Management Studio and create the Nortwind Database

Stop the container, and commit the changes, explaining the layers

`docker commit sqlserver rvanosnabrugge/northwind`

`docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=Welkom1234!" -d -p 1433:1433 --name sqlserver-rvo rvanosnabrugge/northwind` 

## Demo 2: Orchestration and Compose ##
In this demo I want to move my existing, dotnet core, Self Hosted application towards a container. I open the solution with only the dotnetcore stuff in it, and show how it looks. Then, I add Docker support. But what does that do? It adds files. Docker-Compose, a new Project, Docker File.

I explain that the Dockerfile is responsible for describing what is inside your container. That you can use this file to build a container image and that you can distribute that image.

I run the docker command in Visual Studio, and show how you can debug in the container.. So that does also work flawless on the command line ? No !

```
cd demo
docker build -t rvanosnabrugge/techorama-vote . 
```

It fails, because it cannot find the files it needs. Show the Dockerfile and show how the source is an argument that is filled by VS

Tell about a container that you can use for building that takes the source, compiles and store it in a folder that is shared.

```
docker-compose -f docker-compose.ci.build.yml up
```

The run the build command again and see how it works
then run the container and show the application 

```
docker run -d -p 5000:80 rvanosnabrugge/techorama-vote
```

## Demo 3: Setting up the pipelines ##
Now we have teh containers, it is time to set up the Bakery. You can do different things. You can set up a build for a Composition, or you can set up a build for a container. If you release the application as a whole you can do that. You can of course also set up a pipeline for a specific container and only use that one.

We will set up a build for the composition. But then Where do you store it. In the container registry. 

## Demo 4: Setting up the cluster and doing a release ##
Show a little of the cluster, how it is built and how it works. Open a command line and show Kubectl. The command line tool for Kubernetes. 

`kubectl proxy`

Run the app, Scale the app to 10 instances, and show a QR code and bit.ly so people can vote.. Let people visit the website, cast a vote and show the results.

`kubectl scale --replicas=10 deployments/vote`

Then modify the vote app, to only cast A votes, and to change the colors. Check-in, trigger the build and release and show the rolling update. Run the load test as part of the pipeline.

Run the pipeline and run the load test at the same time, showing that the results will slowly change...

```
kubectl set image deployments/vote vote=rvotechorama/votingapp-net:latest`
```

# Links #
* [Original Docker voting app](https://github.com/dockersamples/example-voting-app)
* [Azure Container Registry registration in Kubernetes](https://kubernetes.io/docs/concepts/containers/images/#using-azure-container-registry-acr)
* [Full Screen Mario](https://kotori.me/mario/)
* [Full Screen Mario Docker Container](https://dockerdemos.github.io/fullscreenmario/)
* [PhantomJS in a Docker Container](https://hub.docker.com/r/wernight/phantomjs/)
* [Portainer Docker Management](http://portainer.io/) 
* [Road To ALM - Blog Rene van Osnabrugge](https://roadtoalm.com)
* [Page Object Model Selenium](https://automatetheplanet.com/page-object-pattern/)
* [Docker Voting app](https://github.com/dockersamples/example-voting-app)
* [Environment pattern replacement](https://docs.docker.com/engine/reference/builder/#environment-replacement)