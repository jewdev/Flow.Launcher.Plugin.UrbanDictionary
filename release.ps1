dotnet publish Flow.Launcher.Plugin.UrbanDictionary -c Release -r win-x64 --no-self-contained
Compress-Archive -LiteralPath Flow.Launcher.Plugin.UrbanDictionary/bin/Release/win-x64/publish -DestinationPath Flow.Launcher.Plugin.UrbanDictionary/bin/UrbanDictionary.zip -Force