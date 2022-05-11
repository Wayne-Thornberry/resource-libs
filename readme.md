# Project Online
Project online is an attempt to bring GTA:Online's features and scripts to FiveM. Recreating R* code in a way that is understood, documented and works compared to some API counterparts. Project online aims to port a lot of R* levelscripts over in an attempt to create GTA:Online 1to1 as it was in 2013. 

As part of project online. There are several included libraries programmers can use to help develop their resources in the FiveM framework. These tools and libraries where used to create PO as it is today. 

PO uses MSSQl for its Database solution.
WebAPI components to comunicate with the database
Server resources to communicate with the WebAPIs
Client resources to communicate with the server

The client is broken down into 3 layers
Resource
Modules
LevelScripts

If you just want to develop a mission script. You can easily spin up a class file with an execute method and start writing a levelscript that is light weight, created when needed, disposed when finished and performs API actions on the modules or game itself.

If you want to make something a bit meaty with logic that will stay throughout the game. You can develop a module for the client. These come with their own set of script levels, apis to access from other modules or levelscripts and a way to communicate with the server, perform logging or write the console.

If you want to create something more, you can opt to create your own resource using the resource framework. By default the base resource loads modules into the game and setups the enviroment. But you can choose to not go down the path and develope your own resource all together. Resources are broken down into the core components of a resource, events, scripts, exports and commands.

This repo is broken into several sections
Code - resource, libs, tools and components are based in here. Everything to make the 'base' level function
Data - Data that refers to resource data files, database scripts and component data
Ext - Ext is for non resource code bases, levelscripts and modules go here
Vendor - dlls and 3rd party libraries used by code or ext.

If you are going to develop your own version of PO, please fork the repo and develop inside the ext folder. That way you keep everthing self contained. If you want to go down the creating your own resource route. Then feel free to develop inside code.

