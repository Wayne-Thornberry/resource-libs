﻿Remove-Item ".\*" 
Copy-Item -Path "E:\OneDrive\Repo\Project Online\data\resources\ServerCore\*" -Destination ".\"
Copy-Item -Path "E:\OneDrive\Repo\Project Online\artifacts\ServerCore\*" -Destination ".\"
Remove-Item ".\CitizenFX.Core.*.dll"
Remove-Item ".\*.pdb"