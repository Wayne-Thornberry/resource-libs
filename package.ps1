$name = "ProjectOnline"
$deployPath = ".\$name"
$repo = ".\"

# we need to build the directory for the game
#Remove-Item -Force -Confirm -Recurse -Path "$deployPath"
New-Item -ItemType Directory -Force -Path "$deployPath"
New-Item -ItemType Directory -Force -Path "$deployPath\resources"
New-Item -ItemType Directory -Force -Path "$deployPath\resources\client-core"
New-Item -ItemType Directory -Force -Path "$deployPath\resources\server-core"
 
# we need to deploy the parts of the game 
Copy-Item -Path "$repo\config\server\root\*.*" -Destination "$deployPath\"
Copy-Item -Path "$repo\artifacts\ClientResource\OnlineEngine\debug\*.*" -Destination "$deployPath\resources\client-core"
Copy-Item -Path "$repo\artifacts\ServerResource\OnlineEngine\debug\*.*" -Destination "$deployPath\resources\server-core"
Copy-Item -Path "$repo\Five-Components\Five-RPClientComponents\artifacts\*.*" -Destination "$deployPath\resources\client-core"
Copy-Item -Path "$repo\Five-Components\Five-RPServerComponents\artifacts\*.*" -Destination "$deployPath\resources\server-core"
Copy-Item -Path "$repo\Five-Games\Five-Game-RPOnline\artifacts\*.*" -Destination "$deployPath\resources\client-core"
