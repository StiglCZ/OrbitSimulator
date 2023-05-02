## Orbit simulator
Very simple orbit simulator made in C#

#### Capabilities
- Multiple planets/moons
- Each planet making effect on all other ones
- Simple config file system

<img src="https://cdn.discordapp.com/attachments/947503302361354261/1103045726235263017/screenshot1.png" alt="Image" width="400" height="400">

#### Config system
My orbit simulator uses simple config system, which includes config for different things and planets itsels
Example config:
```
$ This is a comment!

$ Optional arg for clearing screen after each frame (default FALSE)
CLEAR

$ Focuses on specific planet by moving the camera to its position (default -1, does not move)
FOCUS 1

$ Framerate (default 120)
FRAMERATE 120

$ Gravitational constant (default 1)
CONSTANT 1

$ Sets background color (default BLACK)
BACKGROUND BLACK
$ Optional argument for displaying velocity of object (default -1 - Not displaying)
DVEL 1

$ Planet configuration
$ The arguments are positionX, positionY, planets Mass, velocityX, velocityY, color
$ the last one is optional, used to make the object unaffected by gravity + intial velocity
350, 350,60,0,0, ORANGE, TRUE
500, 350,5,0,0.7, BLUE, FALSE
490, 350,0.1,0,1.5, GREEN, FALSE
```
Short demo: https://youtu.be/0OISRY4UWtk
#### Command line arguments
If you will specify existing file in cmd arguments, it will use it instead of default config file inside working directory
#### Shortcuts
C - Clears the screen if autoclearing is off<br/>
SPACE/SHIFT - Speeds up/ Slows down the object with last idex (on end of config file) so you can edit simulation at runtime<br/>
F2 - Takes a screenshot<br/>
F11 - Toggless full screen(If you end simulation before undoing it, your monitor will set to vga resolution)<br/>
