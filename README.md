# Artemis Game Plugins
[![GitHub license](https://img.shields.io/badge/license-noncommercial-blue.svg)](https://github.com/Artemis-RGB/Artemis.Plugins.Games/blob/master/LICENSE)
[![GitHub stars](https://img.shields.io/github/stars/Artemis-RGB/Artemis.Plugins.Games.svg)](https://github.com/Artemis-RGB/Artemis.Plugins.Games/stargazers)
[![Discord](https://img.shields.io/discord/392093058352676874?logo=discord&logoColor=white)](https://discord.gg/S3MVaC9) 
[![Donate](https://img.shields.io/badge/Donate-PayPal-green.svg)](https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=VQBAEJYUFLU4J) 

Repository containing official Artemis game plugins

## Download
https://nightly.link/Artemis-RGB/Artemis.Plugins.Games/workflows/build/master

**If you run into any issues please let us know on Discord.**


## Plugin development
While Artemis 2 is still in development, the plugin API is pretty far along.  
To get started, you can download the Visual Studio extension in the [Visual Studio Marketplace](https://marketplace.visualstudio.com/items?itemName=SpoinkyNL.ArtemisTemplates).

This extension provides you with interactive template projects for each type of Artemis 2 plugin.
To use the templates you must have Artemis 2 extracted somewhere on your drive. The plugin wizard will ask you to locate the exectuable in order to setup all the required project references for you.

Due to the volatine nature of the project right now, there is no documentation yet. The templates provide some commentary to get you going and feel free to ask for more help in Discord.

#### Want to build? Follow these instructions
1. Create a central folder like ```C:\Repos```
2. Clone RGB.NET's development branch into ```<central folder>\RGB.NET```
3. Clone Artemis into  ```<central folder>\Artemis```
5. Open ```<central folder>\RGB.NET\RGB.NET.sln``` and build with the default config
4. Open ```<central folder>\Artemis\src\Artemis.sln``` and build with the default config
5. Clone Artemis.Plugins.Games into  ```<central folder>\Artemis.Plugins.Games```
6. Open ```<central folder>\Artemis.Plugins.Games\src\Artemis.Plugins.Games.sln``` and build with the default config
