Environments
=============

Scenes
-----------------------
This section would describe the xylophone playing environment in VR and how to interact with that both on users and developer view.
You can play xylophone in two different environments the first one called Star Wars and the second one is Cinema. In the first scene the xylophone player is in the space and there are moving object around him/her. The player can activate the songboard and play with that in this environment. In the second scene, a concert environment simulated for the xylophone player. Behind the xylophone player there are simulated faces sitting on chairs and waiting for you to play the xylophone.
In front of the xylophone player there is a big scene which shows the output camera from VR. So when you are playing xylophone you can also see what is actually happening in front of you.

**Notes:**

- When You start the program, it will automatically transform the center of all objects coordinate to the HTC Vive position.
- Both Controller should be activated.

Developer view
^^^^^^^^^^^^^^^^^^

Every Object inside the Star Wars scene are under StarWarsObject unity object and the cinema objects are under CinemaObject. 
Objects movement inside Star Wars environment are defined in scripts/StarWars/MovingObjectsInStarWarsSkyBox.cs. 

And if you want to add an object which you want to move like tieFighters or xwing just put it inside StarWarsObject or if you want to add more laser-bolt create your objects inside StarWarsObject and assign laserBolt tag for them. In the scripts/StarWars folder there is another script named FixStartPosition.cs, which would translate every object in the scene based on the location of VR.
In the cinema scene when you move the sticks some chips would be throwed out of the box. The script related to this action is at script/Cinema/ThrowedChipsHandler.cs.
Another script under script/Cinema is WebCam.cs, in this script you can choose other source of camera output which connected to the PC, would be shown on the front scene.
Just locate the following code and change the value of "HTC Vive" to the name of your camera source. "if (cameraDevices[viveCameraIndex].name.Equals("HTC Vive"))"


Controllers
---------------

In the VR you can play xylophone with different sticks which each one of them produce different sound, for example the Lightsaber stick would create 
more electronic xylophone sound and the chips stick would create classical xylophone sound. 

For changing sticks model in VR, you must press Grip button of Vive controller (button number 8 in the picture) then a menu which contains 3 controller model,
would appear. You can choose each model with controller trackpad (button number 2 in picture). The controller would disappear as soon as you do not touch the trackpad.

.. image:: _static/vive_controllers.jpg
  :width: 400
  :alt: normal stick

When the xylophone player changes the sticks model the environments also change for him/her to base on the sticks.
The lightsaber (the one that is like a sword like Star Wars movie) would change the environment to the Star Wars scene.
The sticks with chips would change the environment to the cinema scene.
The sticks with Roboy head on it would not change any scene, but you can grab cassette with this stick.  
  
  
Developer view
^^^^^^^^^^^^^^^^^

If you want to add new controller model you should add the model at position (0,0,0) in both controller (Controller (left) and Controller (right)) object inside cameraRig in unity. Also, you have to add the model picture in controller menu, so users could be able to change the sticker to that model. 
For modifying the controller menu functionality, you have to change controller.cs.

If you want to change the song for each note when changing the stick, you have to modify 
StarWarsObjects/Self/Xylophone/KeyManager/XylophoneKeyWrapper##/XylophoneKeyCollider. First Audio source is for the normal stick, 
second one is for the stick which had Roboy head on that, and the third one is for Lightsaber.
