# TOTest

Solution where we get image from an external API and store in our own API in memory cache.

We have two projects:
 
 `TOTest.API` which is our api that calls the external API and store in memory cache.
 `TOTestApp` which is our Angular app that get the images from our API
 
 In order to execute both apps in your local environment you need to follow these steps:
 
 1. Download the repo.
 2. `TOTest.API` restore nuget packages and run it from Visual Studio.
 3. `TOTestApp` install packages `npm install` and then `npm start` in order to run the app.
 
 Be sure that first you have running the API and then the Angular app.
