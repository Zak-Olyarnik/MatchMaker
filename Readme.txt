MatchMaker Readme
Joshua Martin, Zakary Olyarnik
January 2017

INTRODUCTION:
It’s just your typical high school dance...
Full of awkward and confused teenagers.
Play Cupid and shoot your arrows into matching pairs to help them find true love!
(Probably not...)
Match everyone by midnight when the dance ends, but be careful not to mis-fire or hit the obstacles!

RUNNING THE GAME:
We have provided a Windows build (MatchMaker.exe) and a WebGL build (index.html), both located in the Builds
subfolder, and both tested for validity.  The Windows build must be run with 1920x1080 resolution to display as
intended, due to the unpredictable way Unity's UI scales to different resolutions.  The web build is also set to
1920x1280, but should be run in fullscreen to properly display this way.

ASSETS:
All sprite images used were created by Josh.
"balloon_pop.wav" sound effect by Mike Koenig on SoundBible.com
"aww.wav" sound effect by phmiller42 of Freesound.org is used under a Creative Commons license.
"gibberish.wav" sound effect by xtrgamr of Freesound.org is used under a Creative Commons license.
"glass-break-2.wav" sound effect by Natty23 of Freesound.org is used under a Creative Commons license.
"Interference 01.wav" sound effect by Glitchedtones of Freesound.org is used under a Creative Commons license.
breaker noise (formerly "Kill Switch (Large Breaker Switch).wav" by Alvinwhatup2 of Freesound.org) is used under a Creative Commons license.
All other sound filed obtained through the Drexel database.

UPDATES FROM BETA:
-Incorporation of a lives system - rather than just "playing until the end", the game can now be lost by making too many incorrect matches.
-Targets spawn in pre-defined locations onscreen to eliminate downtime waiting for them to move on from the edges.
-Targets speed up as they are cleared, elminitating downtime waiting for the last few to match up at round's end.
-Teacher's movement has been randomized.
-Balloons are constantly respawned rather than having a set number which cycle.
-Target sprites updated with simple animations.
-Light and speaker obstacles added - hitting will affect the in-game brightness and music, respectively.
-New background image.
-Added a fifth round.
-GameController script modified to follow Singleton pattern (for more efficient updating of lives UI).