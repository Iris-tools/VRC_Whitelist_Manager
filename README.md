# VRC_Whitelist_Manager

* A VRC UdonSharp script that allows you to protect your private world by whitelisting specific usernames.
* Unauthorized users are exiled to a deny zone, shown a black screen, and lights can be turned off for them as a deterrent.

## Description

This is a whitelist script designed to protect your private world. It exiles players who are not on the whitelist to a designated location, blocks their vision with a black screen, and disables world lighting as a form of protection.

## Dependency

* Requires ```UdonSharp v1.2.0-b1```  
    * Download: [UdonSharp v1.2.0-b1](https://github.com/MerlinVR/UdonSharp/releases/tag/v1.2.0-b1)

## Setup Instructions

### 1. Create Script Folder
* Create a folder in your Unity ```Assets``` directory named ```Scripts```

### 2. Add the Script File
* Drag ```Iris_Whitelist_Manager.cs``` into the ```Scripts``` folder

### 3. Create UdonSharp Program Asset
* Inside the ```Scripts``` folder, create a new ```Udon C# Program Asset```
* Rename it to ```Iris_Whitelist_Manager```
* Click ```Create Script```, and select ```Iris_Whitelist_Manager.cs```
* If Unity prompts that the script already exists, click ```Yes``` to overwrite

### 4. Fix Script Compilation (IMPORTANT!)
* Go to ```Assets > Scripts > Iris_Whitelist_Manager.cs``` in your Unity project's folder
* Open both this file and your downloaded ```Iris_Whitelist_Manager.cs``` in ```Visual Studio Code```
* You'll see that the Unity-generated script is empty
* Copy the entire content from your downloaded ```Iris_Whitelist_Manager.cs``` into the Unity version
* Save ```Ctrl + S``` and return to Unity to let it recompile

### 5. Add Udon Behaviour to Scene
* In the Hierarchy, create an ```Empty GameObject``` and name it ```Iris_Whitelist_Manager```
* Add a ```Udon Behaviour``` component
* Assign the ```Iris_Whitelist_Manager``` Udon Program Asset you created

### 6. Setup Deny Zone
* In the Hierarchy, create a ```Cube``` or ```Empty GameObject``` named ```DenyZone```
* Move it to a far-off location (e.g., position ```9999, 0, 9999```)
* Assign this object to the ```Deny Zone``` slot in ```Iris_Whitelist_Manager```

### 7. Setup Blackout Canvas
* In the Hierarchy: ```UI > Canvas```, name it ```BlackScreenCanvas```
* Set ```Render Mode``` to ```Screen Space - Overlay```
* Add an ```Image``` component inside the Canvas
* Set the ```Color``` to ```solid black``` ```#000000``` with ```Alpha = 255```
* In the Inspector, uncheck the ```BlackScreenCanvas``` ```SetActive``` (disable it by default)
* Assign this Canvas to the ```Black Screen Canvas``` slot in ```Iris_Whitelist_Manager```

### 8. The most important step
Don’t forget to add ```your name``` and ```your friends’ usernames``` to the ```whitelist```

##  What Happens to Unauthorized Users?
* They are instantly teleported to the ```Deny Zone```
* The screen is covered by a ```Black Screen Canvas```
* Their movement is disabled
* Optional: Lights are turned off for them (if assigned)
* Users who are not on the whitelist will be unable to interact with any UI elements and must quit the game to leave

##  What Happens to Authorized Users?
* They are teleported to the specified ```Welcome Zone``` (if set)
* They can freely explore and interact

## Version History

* ```1.0```
    * Initial Release
