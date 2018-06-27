# ss18_xylophone_recording

This project is about capturing somebody playing xylophone in virtual reality. Processing the recording and then sending it to the real Roboy, so that he can play the same notes.

## Pre-requisites

## Setup

## Usage

Before running the Unity Project:
```
source path-to-roboy-repo/devel/setup.bash
roslaunch rosbridge_server rosbridge_websocket.launch
```

Check the MidiBridge for the correct Midi Device Number and Midi Channel. Also check the ROSBridge for the correct ROS Core IP and Port.

## Midi Third Party Programms for Testing

### Midi Monitor - [MIDIOX](http://www.midiox.com/)
This tool can be used to watch the Midi Output locally. It also helps to identify the Midi Device Numbers which have to be entered in the MidiBridge.

### Midi Loopback Driver - [loopMIDI](http://www.tobias-erichsen.de/software/loopmidi.html)
This Driver can be used if you don't own a Midi Device. It will loopback the local Midi Output to the local Midi Input.