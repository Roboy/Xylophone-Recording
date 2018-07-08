Current State
================

Xylophone
-----------

In the mean time, due to limited time, we haven't modeled the real xylophone yet. We use simple unity cubes as xylophone keys for testing purpose. On the other hand, we only have 7 keys. however, the functionalities of the keys are basically complete. When they are hit, they emit the sound of the corresponding key (audio feedback), they would be "pressed down" and then bounce back and at the same time highlight a little (visual feedback), and they would also transfer the message through ROSBridge to receivers (for example Roboy).

Environment and Controllers
-----------------------------

We build a quite good looking environment for the user and the xylophone. The environment is of STARWARS theme. The xylophone sits on the back of a X-Wing Fighter, and surrounded by thousands of stars. Several TIE Fighters would fly by, and the death star is also in sight. It is a very fancy environment for the user to play xylophone.

There are currently 3 controllers, or, sticks for the user to use, which are a normal stick, a stick with a Roboy head, and a light saber. The user can switch between them and use each one of them to play the xylophone. When playing xylophone, different sticks give different sounds.

Communication
-----------------
For current state of communication part, please refer to `Communication <Communication.html>`_.

Song system (Xylophone Hero)
-------------------------------
For current state of song system part, please refer to `Song System <SongSystem.html>`_.