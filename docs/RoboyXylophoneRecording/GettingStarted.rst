Getting Started
====================

This section explains how to install the project and later on which steps have to be done when you want to execute the program.

Installation
--------------------

Part 1: Install Unity
~~~~~~~~~~~~~~~~~~~~~~~~~

The Xylophone Recording Project is developped and tested in Unity 2018.1.5.f1. Any Versions later than this should be compatible with this project. Using earlier versions should be avoided, because it may cause "missing prefab" problems.

Unity can be downloaded from here: https://unity3d.com/get-unity/download/archive

Select the correct version and download the installer. After downloading, run the installer and follow the instructions to install Unity.

Part 2: Setup HTC Vive and SteamVR
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
Follow the official `SteamVR HTC Vive PRE installation Guide <https://support.steampowered.com/kb_article.php?ref=2001-UXCM-4439>`_ to set up HTC Vive and SteamVR.

Part 3: Clone the project from Github
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
Use the following command in Git Bash or command line to clone the Xylophone Recording repository from Github::

	git clone git@github.com:Roboy/ss18_xylophone_recording.git

Part 4: Setup ROS
~~~~~~~~~~~~~~~~~~
*TODO by Ludwig clarify this*

Install ROS and build our `branch of ROS communication <https://github.com/Roboy/roboy_communication/tree/ss18_xylophone_recording>`_. This could be installed on another computer then were Unity is installed.


Starting the Program
--------------------

Step 1: Launch ROSBridge Server
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

Before running the Unity Project launch the ROSBridge Server on a device which has ROS installed like this::

	source path-to-roboy-repo/devel/setup.bash
	roslaunch rosbridge_server rosbridge_websocket.launch

Step 2: Check for an available Midi Device
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

Check if you have any available Midi Devices. This can be done by using a Midi Management/Monitoring tool like `Midi-OX <http://www.Midiox.com/>`_. In Midi-OX use Options > Midi Devices to see the current Midi Devices available.

Note down the Midi Device Number of your prefered Midi Output as you need it later on.

If you don't have a local Midi Device you can use a loopback Driver like `loopMIDI <http://www.tobias-erichsen.de/software/loopMidi.html>`_ for testing purposes.

Step 3: Launch Unity
~~~~~~~~~~~~~~~~~~~~

Launch Unity and check the GameObjects MidiBridge for the correct Midi Device Number which you wrote down earlier and your desired Midi Channel.

Check the GameObject ROSBridge for the correct ROS Core IP (computer where you started the ROSBridge Server) and Port.