# MixedAviation
## Authors

Team Leader: Adrian Sarbach \
Styrmir Óli Þorsteinsson \
Rohit Koonireddy

## Description

MixedAviation is running on unity 2022.3.11f1\
Setup for Hololens 2\

The idea of the project is to create a visual experience for pilots of the pre-flight and in-flight aspects of flight. Pre-flight is the way to plan your flight. This is done in the project in a way that the terrain and all obstacles in said terrain are shown and visible to help the pilot find a good route for their next trip. This is mostly completed but the need for more user input and interaction is something that was in the works but didn’t se the light of day. In-flight was to have everything visible to the pilot and letting the pilot interact with mountains, villages or other named terrain to find their location, all the mountains and hazards are displayed in world and they interaction to  get the name of the object/terrain works but further implementation needed more time.

## Installation 
These are the packages need to get the object and maps

Maps: https://polybox.ethz.ch/index.php/s/1OiY5cyZxT0p9mf

Objects: https://polybox.ethz.ch/index.php/s/kX0zR1fRGixoEvJ

The install the application it is best to start with vuforia.\
Vuforia: you can get it in the unity packagemanager and this can be done following this guide https://developer.vuforia.com/library/getting-started/getting-started-vuforia-engine-unity#intro \
or getting the package from this link and inserting it to the [package] folder in unity https://polybox.ethz.ch/index.php/s/DrrGjpdXQSXQ1Zt \
you can also get the latest release of the project with this zip link: https://developer.vuforia.com/library/getting-started/getting-started-vuforia-engine-unity#intro \
After getting all the needed components you'll need to set the maps and object to the scripts but if you don't want to go for that the latest-release is the way to go. \
With that the project should run and start after building. to build you'll need to change to Windows Universal (set the build information to Release and ARM64) \  
Now you can run it and get in on the hololens.

## Demo
Here are some demo videos of the application. \
Here is the vuforia demo: https://drive.google.com/file/d/1Qo0cB5aABopoFyQsn_xpDimkajS_FwYF/view?usp=sharing \
Here is the static map demo: https://drive.google.com/file/d/1xgxNWv4q46tuNO0l4wgWtZBdiQ7LfPaX/view?usp=sharing
