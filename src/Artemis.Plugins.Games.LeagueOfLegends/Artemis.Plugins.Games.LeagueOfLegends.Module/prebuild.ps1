# Get the path to the files
$championsPath = Join-Path $PSScriptRoot "champion.json"
$itemsPath = Join-Path $PSScriptRoot "item.json"

# Check if files exist and get their versions
if (Test-Path $championsPath) {
    $championsVersion = (Get-Content $championsPath | ConvertFrom-Json).version
} else {
    $championsVersion = ""
}
if (Test-Path $itemsPath) {
    $itemsVersion = (Get-Content $itemsPath | ConvertFrom-Json).version
} else {
    $itemsVersion = ""
}

# Make HTTP request to retrieve versions
$versionsString = Invoke-RestMethod -Uri "https://ddragon.leagueoflegends.com/api/versions.json"

# Deserialize the JSON response
$versions = $versionsString -split '\s+'

# Get the latest version
$latestVersion = $versions[0]

# Check if files are up to date
if ($championsVersion -ne $latestVersion) {
    # Delete the file if it exists
    if (Test-Path $championsPath) { Remove-Item $championsPath }
    # Download the file
    Invoke-WebRequest -Uri "https://ddragon.leagueoflegends.com/cdn/$latestVersion/data/en_US/champion.json" -OutFile $championsPath
}
if ($itemsVersion -ne $latestVersion) {
    # Delete the file if it exists
    if (Test-Path $itemsPath) { Remove-Item $itemsPath }
    # Download the file
    Invoke-WebRequest -Uri "https://ddragon.leagueoflegends.com/cdn/$latestVersion/data/en_US/item.json" -OutFile $itemsPath
}