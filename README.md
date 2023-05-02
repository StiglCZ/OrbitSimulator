## Orbit simulator
#### Very simple orbit simulator made in C#

#### Capabilities
- Multiple planets/moons
- Each planet making effect on all other ones
- Simple config file system


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

$ Optional argument for displaying velocity of object (default -1 - Not displaying)
DVEL 1

$ Planet configuration
$ The arguments are positionX, positionY, planets Mass, velocityX, velocityY, color
$ the last one is optional, used to make the object unaffected by gravity + intial velocity
350, 350,60,0,0, ORANGE, TRUE
500, 350,5,0,0.7, BLUE, FALSE
490, 350,0.1,0,1.5, GREEN, FALSE
```
