#Remove-Item "D:\ProjectOnline\resources\client-core\*"  -Recurse
#Copy-Item -Path ".\vendor\ProjectOnline\*" -Destination "D:\ProjectOnline\resources\client-core\" -Recurse
Copy-Item -Path ".\data\*" -Destination "D:\ProjectOnline\resources\client-core\" -Recurse -Force

Copy-Item -Path ".\data\*" -Destination "D:\ProjectOnline\resources\client-core\" -Recurse -Force
Copy-Item -Path ".\code\tools\" -Destination "D:\ProjectOnline\resources\client-core\" -Recurse -Force
Copy-Item -Path ".\artifacts\*" -Destination "D:\ProjectOnline\resources\client-core\" -Recurse -Force
Remove-Item "D:\ProjectOnline\resources\client-core\CitizenFX.Core.*.dll" -Recurse
Remove-Item "D:\ProjectOnline\resources\client-core\*.pdb" -Recurse