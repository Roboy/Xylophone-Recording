Communication
=============

Structure
---------

.. image:: _static/presentationOverview.jpg
	:alt: Communication Structure

There are multiple communication/output channels in order to save or communicate the played xylophone notes:

MidiBridge
^^^^^^^^^^
	Live local Midi Output of the Xylophone notes. The Midi note Velocity is static.
	The MidiBridge Gameobject is used to configure the **Midi Device Number** and the **Midi Channel**.
	`Jack2 <https://github.com/jackaudio/jack2>`_ can be used to transport the Midi Data via Ethernet to another device (see Jack2 Network Setup).

MidiRecording
^^^^^^^^^^^^^
	Writes the Midi NoteOn and NoteOff events of the xylophone to a Midi file.
	The recording can be started and stoped inside the VR environment.
	Via the MidiRecording Gameobject the **Midi File Path** and the option to overwrite this file can be set.

ROSBridge
^^^^^^^^^
	ROS Message (/roboy/control/musicalNote) sent in a Subscriper/Publisher fashion.
	The message is sent when the note is triggered and just contains the note as Midi Integer representation and the UNIX time in Milliseconds when it was played.

	.. image:: _static/ROS_messages.jpg
		:alt: ROS Midi messages


Jack2 Network Setup
-------------------

In the following subsections it's explained how you can use the local Midi Output of the MidiBridge and send it to other computers with Jack2.

Requirements
^^^^^^^^^^^^
- LAN with Multicast Support (IGMP Snooping and IGMP Querier)
- two computers capable of running jack2

Setup Steps
^^^^^^^^^^^^

TODO: Add pictures and steps

ROS Cheatsheet
--------------

This was used for debugging/demo purposes to see the Midi messages::

	cd path/to/Roboy
	source devel/setup.bash
	rostopic list
	rostopic echo /roboy/control/musicalNote


Current State
-------------

Sofar there are three ways of communication/output via ROSBridge, MidiRecording and via MidiBridge.

The ROS Messages are pretty basic sofar and can be extended if needed.
We didn't extend the ROS approach as the Jack2 approach seems to have a better performance as Jack2 is based on UDP packets and not on TCP packets like ROS with the Unity ROSBridge.

Jack2 could probably be integrated more tightly on a library level in Unity and not just on a programm level which uses the Midi Data coming from the MidiBridge as Input.