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
This is just one way of installing ROS for Roboy. There are probably more suited approaches which are more elegant. But this one worked for us. ROS can be installed on another computer then were Unity is installed.

Install ROS like described in the `Roboy repository <https://github.com/Roboy/Roboy>`_.

Change to the Roboy directory and checkout our `branch <https://github.com/Roboy/roboy_communication/tree/ss18_xylophone_recording>`_ of the roboy_communication repository::

	cd path/to/Roboy/src/roboy_communication
	git checkout ss18_xylophone_recording

If you just want to run our program you can move all the other submodules/folders inside src (everything except CMakeLists.txt and roboy_communication) to another folder outside src so that they won't be built::
	
	cd path/to/Roboy
	mkdir donotbuild
	mv src/common_utilities donotbuild
	...

Build the project::

	source devel/setup.bash
	catkin_make

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

.. image:: _static/GameObject_MidiBridge.png
		:alt: GameObject MidiBridge

Check the GameObject ROSBridge for the correct ROS Core IP (computer where you started the ROSBridge Server) and Port.

.. image:: _static/GameObject_ROSBridge.png
		:alt: GameObject ROSBridge