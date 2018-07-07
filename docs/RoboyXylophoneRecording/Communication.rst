Communication
=============

Structure
---------

There are multiple communication/output channels in order to save or communicate the played xylophone notes:

MidiBridge
^^^^^^^^^^
	Live local Midi Output of the Xylophone notes. The Midi note Velocity is static.

ROSBridge
^^^^^^^^^
	ROS Messages sent in a Subscriper/Publisher fashion.
	The message is sent when the note is triggered and just contains the note as Midi Integer representation and the UNIX time in Milliseconds when it was played.


Current State
-------------

Sofar there are two ways of communication/output via ROSBridge and via MidiBridge.

In the next release there should be another option to save to a Midi file. Also the ROS Messages will probably be extended and be used as a full Midi Wrapper.