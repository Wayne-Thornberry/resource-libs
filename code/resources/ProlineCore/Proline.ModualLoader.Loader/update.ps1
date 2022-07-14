Remove-Item ".\*"  -Recurse
Copy-Item -Path "E:\OneDrive\Repo\Project Online\data\resources\ProlineCore\*" -Destination ".\" -Recurse
Copy-Item -Path "E:\OneDrive\Repo\Project Online\artifacts\ProlineCore\*" -Destination ".\" -Recurse
Remove-Item ".\CitizenFX.Core.*.dll" -Recurse
Remove-Item ".\*.pdb" -Recurse