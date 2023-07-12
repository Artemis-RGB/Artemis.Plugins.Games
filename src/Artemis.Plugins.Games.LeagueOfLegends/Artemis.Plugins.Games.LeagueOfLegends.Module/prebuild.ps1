# Make HTTP request to retrieve versions
$versionsString = Invoke-RestMethod -Uri "https://ddragon.leagueoflegends.com/api/versions.json"

# Deserialize the JSON response
$versions = $versionsString -split '\s+'

# Get the latest version
$latestVersion = $versions[0]

# Get the path to the files
$championsPath = Join-Path $PSScriptRoot "champion.json"
$itemsPath = Join-Path $PSScriptRoot "item.json"

# Delete the files if they exist
if (Test-Path $championsPath) { Remove-Item $championsPath }
if (Test-Path $itemsPath) { Remove-Item $itemsPath }

# Download the files
Invoke-WebRequest -Uri "https://ddragon.leagueoflegends.com/cdn/$latestVersion/data/en_US/champion.json" -OutFile $championsPath
Invoke-WebRequest -Uri "https://ddragon.leagueoflegends.com/cdn/$latestVersion/data/en_US/item.json" -OutFile $itemsPath

