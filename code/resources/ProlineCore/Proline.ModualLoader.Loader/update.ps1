Remove-Item "%~dp0\*"  -Recurse
Copy-Item -Path "E:\OneDrive\Repo\Project Online\data\resources\ProlineCore\*" -Destination "%~dp0\" -Recurse
Copy-Item -Path "E:\OneDrive\Repo\Project Online\artifacts\ProlineCore\*" -Destination "%~dp0\" -Recurse
Remove-Item "%~dp0\CitizenFX.Core.*.dll" -Recurse
Remove-Item "%~dp0\*.pdb" -Recurse