Copy-Item -Path "E:\OneDrive\Repo\Project Online\artifacts\ProjectOnline\client\*.dll" -Destination "E:\ProjectOnline\resources\client-core"
Remove-Item "E:\ProjectOnline\resources\client-core\CitizenFX.Core.*.dll"
Remove-Item "E:\ProjectOnline\resources\client-core\*.pdb"