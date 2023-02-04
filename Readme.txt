Artifacts - A place where the whole project is built into one folder for packaging and deployment
Code - Source code for tools, components, resources, resource-components and misc scripts
	Libs - Libararies used by any of the other source level items 
	Game - Client specific enviroment under FiveM
		Resources - Resources that are loaded and run under FivesM scripting enviroment
		Components - Components loaded in via the client specific resource client-core
		LevelScripts - Scripts that are run using the Common component CScripting
	Server - Server specific enviroment under FiveM
		Resources - Resources that are loaded and run under FivesM scripting enviroment
		Components - Components loaded in via the client specific resource server-core 
	Tools - .Net core tools developed to interface with the server &or web apis
Data - data files used by tools, server, components, resources and resource-components
	Game - Client only specific data files
	Server - Server only specific data files 
Config 
	Game - Game resource specific configs, includes manifest, 
	Server - Server resource specific configs, includes manifest,  
Vendor - 3rd Party libraries that otherwies cannot be referenced via nugets