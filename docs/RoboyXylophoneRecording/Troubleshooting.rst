Troubleshooting
==================

Unity Errors concerning MidiBridge or ROSBridge
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

Make sure you have a Midi Device and selected an existing one. Also make sure the ROSBridge is reachable and running. If you disconnect the Midi Device or RosBridge Connection while running the program it will probably crash.

Problems with the Jack2 Installation and Config
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

If our given Setup Guide doesn't work for you and you're running Linux `the Arch Wiki <https://wiki.archlinux.org/index.php/JACK_Audio_Connection_Kit>`_ good a lot of hints on how to get it to work.
Our Setup Guide was specified for Ubuntu so a few steps aren't the same but maybe if you're using another distro this will help you.

Debugging without a HTC Vive
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

In the Unity Xylophone model we added debugging support for playing the xylophone without having a HTC Vive at hand.
You can set a computer keyboard key to a xylophone key in the **XylophoneKeyCollider** GameObject.

.. image:: _static/xylophone_key_debugging.png
		:alt: GameObject XylophoneKeyCollider