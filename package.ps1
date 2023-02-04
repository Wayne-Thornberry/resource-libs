$name = "ProjectOnline"
$deployPath = "D:\Games\$name"
$repo = "C:\Users\Wayno717\OneDrive\Repo\FiveM"

# we need to build the directory for the game
Remove-Item -Force -Confirm -Recurse -Path "$deployPath"
New-Item -ItemType Directory -Force -Path "$deployPath"
New-Item -ItemType Directory -Force -Path "$deployPath\resources"
New-Item -ItemType Directory -Force -Path "$deployPath\resources\client-core"
New-Item -ItemType Directory -Force -Path "$deployPath\resources\server-core"
 
# we need to deploy the parts of the game 
Copy-Item -Path "$repo\Five-Config\Server\*.*" -Destination "$deployPath\"
Copy-Item -Path "$repo\Five-Resources\Five-ClientResource\artifacts\*.*" -Destination "$deployPath\resources\client-core"
Copy-Item -Path "$repo\Five-Resources\Five-ServerResource\artifacts\*.*" -Destination "$deployPath\resources\server-core"
Copy-Item -Path "$repo\Five-Components\Five-RPClientComponents\artifacts\*.*" -Destination "$deployPath\resources\client-core"
Copy-Item -Path "$repo\Five-Components\Five-RPServerComponents\artifacts\*.*" -Destination "$deployPath\resources\server-core"
Copy-Item -Path "$repo\Five-Games\Five-Game-RPOnline\artifacts\*.*" -Destination "$deployPath\resources\client-core"
