Song System (Xylophone Hero)
================================

Motivation and Introduction
--------------------------------

When the user is playing the xylophone in virtual reality, he may sometimes feel lost and don't know what to play. Therefore, we decided to add a song system, which can tell the user which note should he/she play and when to play. Furthermore, we considered that some gaming features can be added to the system, such as a score system and more fancy visual effects.

Inspired by the famous video game serious Guitar Hero, we name our song system Xylophone Hero.

Structure
--------------

.. image:: _static/XylophoneHeroStructure.png
	:alt: Roboy Xylophone Recording Project Structure

The Song System, aka. Xylophone Hero, consists of 3 Major Parts: **Song System Manager**, **Key Indicator** and **Note Destroyer**. It reads song files from the disk and generate in game playable songs. Users interact with the song system through the xylophone. Details are described below.

Song File
~~~~~~~~~~~

The song files are not actual audio recordings of the song (``.mp3``, ``.wav``, etc.). They contain only the sequences of the notes which will be used to generate the notes in the song system. The format is inspired by the ``.sm`` file of `StepMania <https://www.stepmania.com/>`_ which looks as follows::

	100000000000
	000000000000
	000000000000
	010000000000
	001000000000
	000000000000
	000000000000
	100000000000

Each line represents one beat. There are 12 keys in the xylophone now so each row has 12 digits, each one corresponds to one key. 1 means this key should be played on this beat, 0 means it should not. So the note generator can generate the notes of the song according to this song file.

Song System Manager
~~~~~~~~~~~~~~~~~~~~~

Song System Manager is the most important and core part of the song system. It consists of 3 parts: **Note Generator**, **Cassette System** and **Score and Feedback System**. And these parts are all organized and managed by the **Song Manager**. The Song System Manager manages the state of the song system, displays useful information, and generates the notes of songs. 

Song Manager
^^^^^^^^^^^^^^^^^^
The SongManager mainly manages the state of song playing. When the song system starts, it loads the song files from the designated path, prepares songs for the cassette system. It has functions to switch between state of playing and stop as well as switch songs. These functions also control the Note Generator to generate proper notes. In addition, the Song Manager monitors how good the player plays the xylophone then calculate scores and give proper feedback.

.. ATTENTION::
	The ``SongManager`` class is a Singleton. Only one instance of this class is allowed. Therefore don't put two song boards in one scene! In the case of multiple environments (like the cinema and STARWARS), either only one environment having the song board or all environments sharing one song board is the correct solution. 

Note Generator
^^^^^^^^^^^^^^^^^^

Just as its name indicates, the note generator generates the notes according to the song file. It starts a coroutine, which respawns the notes at a certain rate according to the set BPM (Beats Per Minute). The notes then fall down. When they reach the corresponding key indicator, the User should play the key.

Cassette System
^^^^^^^^^^^^^^^^^^

We seek to make the song system more interactive, more intuitive and more interesting to play. Therefore, instead of having a song list displaying all songs and player selecting songs and controlling the system by pressing buttons, we decided to use cassettes to represent songs and a cassette player to control the song playing. 

After the Song Manager loads the songs, it will generate several cassette, each one carries the information of one song, on the desk. The player can use the stick with Roboy's head to grab a cassette and then put it in the cassette player, which will make the Song Manager start the song. 

There are two buttons above the cassette player. The left one is *Reload Song*. By pressing this button the Song Manager clears the current loaded songs, destroys all existing cassettes, and then loads the songs and generates the cassettes again. This is useful when the player drops some cassettes to the space or can not reach the cassettes for some reasons. The right button is *Eject* button. This makes the cassette player eject the current cassette and stop playing the song.

Score and Feedback System
^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^

Scores and Feedbacks help players become xylophone masters. When the player keep playing the right keys at the right time, he/she gets *combos*, otherwise he/she gets *misses*. When combos or misses reaches certain level, the player will hear cheers or boos and see texts of praise or disappointment. In addition, the player gets scores with every good hit, and combos make more score per good hit.

Key Indicator
~~~~~~~~~~~~~~~~~

The key indicator indicates which key should be play now. When the song note falls and hits the key indicator, the corresponding key should be played. if the key is played, the song note would be destroyed and the key indicator would tell song system manager that the user has scored. If not, the song note would fall through the indicator and reach the note destroyer.

Note Destroyer
~~~~~~~~~~~~~~~~

The Note Destroyer destroys all game objects with the tag ``SongNote`` which enter its trigger zone. This basically "cleans up" the notes that are not played by the user.

Some Other Things
~~~~~~~~~~~~~~~~~~~

There are two buttons on the left side of the player (when facing the xylophone). One is *Toggle Recording*. This one toggles converting the played notes to midi. The other one is *Toggle Board*. This one shows or hides the song board.

Under the ``SongSystemManager`` Game Object there is a set of **Control Buttons** which are disabled. They are not used in the release version of Xylophone Hero, but they are good helpers for debugging. When they are enabled, the player or developer can use the keyboard to start, stop, play the previous or play the next song. Of course the player can also interact with these buttons using the sticks.

Current State
----------------------

The song system is playable and fun to play. It can load multiple songs from the designated path and generate cassettes for the songs. the cassette system is ready to use. Score and feedback system works fine, but still has room for improvements (better rules, ranking system, etc.). In addition, the models of the song system can be further polished or beautified.