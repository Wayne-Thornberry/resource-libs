# Project Online
Project Online (PO) is two pronged solution to develop and attempt to bring GTA:Online's features and scripts to FiveM. Recreating R* code in a way that is understood, documented and works more effectivly than directly calling native APIs. 

## The solution
The project is broken down into three parts. The resource itself (This is Classic Online) which includes Moduals (Part of the resource that loads in Moduals in the resource folder) and LevelScripts (Part of the resource but loaded in via the Scripting Modual)

You can develop for each one of these parts. If you want to develop your own FiveM resource using the libraries provided, you can do that. Want to develop addional APIs, Commands, PermaScripts and logic, you can do that with Moduals. Want to make some fun missions or objectives without having to worry about heavy logic, you can do that using Levelscripts.

## Resource Tools
Dont want to build ontop of GTA:Online 2013? Thats fine PO also includes a wide range of libaries to help you develop your own resources within the framework that Online Classic was developed in.

Just reference ResourceLibs and load the resource in as normal.

## Repo
Code - resource, libs, tools and components are based in here. Everything to make the 'base' level function
Data - Data that refers to resource data files, database scripts and component data
Ext - Ext is for non resource code bases, levelscripts and modules go here
Vendor - dlls and 3rd party libraries used by code or ext.

