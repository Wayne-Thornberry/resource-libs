Remove-Item "D:\ProjectOnline\" -Recurse
New-Item -Path "D:\" -Name "ProjectOnline" -ItemType "directory" 
#New-Item -Path "D:\ProjectOnline\" -Name "resources" -ItemType "directory"
#New-Item -Path "D:\ProjectOnline\resources" -Name "client-core" -ItemType "directory"
#New-Item -Path "D:\ProjectOnline\resources" -Name "server-core" -ItemType "directory" 
Copy-Item -Path ".\data\*" -Destination "D:\ProjectOnline\" -Recurse
Copy-Item -Path ".\artifacts\ProjectOnline*" -Destination "D:\ProjectOnline\" -Recurse
Copy-Item -Path ".\artifacts\*Client*.dll" -Destination "D:\ProjectOnline\resources\client-core\" -Recurse
Copy-Item -Path ".\artifacts\*Server*.dll" -Destination "D:\ProjectOnline\resources\server-core\" -Recurse
Remove-Item "D:\ProjectOnline\resources\client-core\CitizenFX.Core.*.dll" -Recurse
Remove-Item "D:\ProjectOnline\resources\client-core\*.pdb" -Recurse
Remove-Item "D:\ProjectOnline\resources\server-core\CitizenFX.Core.*.dll" -Recurse
Remove-Item "D:\ProjectOnline\resources\server-core\*.pdb" -Recurse