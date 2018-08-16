----------------------------------------------------------------------------------------------------------------------------------------------------------------------
Goals
----------------------------------------------------------------------------------------------------------------------------------------------------------------------
Originally I wanted to create a Rogue-like dungeon crawler with some RPG elements.
I had planned on having a multi-floor dungeon full of loot, enemies, deadly traps, and boss fights for the player to work through.
In terms of combat, I had planned to have a single enemy who functions similarily to the player in terms of combat and structure.
Both the player and enemies were planned to follow a basic RPG style level up/stats system.
I had also planned for the player to have a sword and shield for the player's combat mechanics.
These mechanics included the following: light-attacks, heavy-attacks, and the ability to block enemy strikes with the shield.
The player was also planned to be able to pick up loot like health potions, better equipment, and armor.
Optionally I specified that I would like to try and create a save system and/or a dungeon generation system if I had the time.

The only things I was not able to accomplish by this beta was the implementation of traps and the implementation of multiple floors/boss fights.
This was due to the sheer amount of time it took me to build/populate the first floor of the dungeon, so I was unable to build more within the time constraints.
I also did not accomplish any of my optional goals stated above.

----------------------------------------------------------------------------------------------------------------------------------------------------------------------
Experiences
----------------------------------------------------------------------------------------------------------------------------------------------------------------------
My experience with Unity has been a bumpy one. I found that following along with the tutorials throughout the semester, and working off of projects by myself that were already set up were pretty easy.
I didn't run into very many issues during this time period; however, over the span of time that I have been working on this project I have ran into more problems than I can count.
Due to these issues I've had to do a lot of extra research and touch on topics that we never discussed in class.
While this was very time consuming and frustrating at times, I've learned a lot about how most games function and the game development process/struggles.
Of course I have also become very familiar with Unity, how it functions, and generally how to solve most issues pretty quickly.
Overall I think this independent study was worth every second that I put into it;
I base this on the amount of this knowledge that I can apply day-to-day as someone who loves video games and how useful this knowledge will be to me in my independent ventures(Like making games to integrate into an android app).

----------------------------------------------------------------------------------------------------------------------------------------------------------------------
Controls
----------------------------------------------------------------------------------------------------------------------------------------------------------------------
(w,a,s,d) and (Up,Down,Left,Right):	Moves the player around the world.

SpaceBar: This will make the player jump, although it doesn't really have much purpose at the moment.

c:	Opens a menu which displays your character's information.
	This menu will have extra options when the player levels up, so make sure to check this menu every time you level up.

Esc:	Opens a menu which gives the player some gameplay options, such as "Return to main menu", and "Exit Game".

Left-Click:	This initiates the player's Light-Attack at the cost of stamina.

Shift+Left-Click:	This initiates the player's Heavy-Attack at a greater stamina cost and slower speed.

Hold Right-Click:	This raises the player's shield and reduces the damage of attacks that hit the shield at the cost of stamina.
			The player cannot regenerate stamina while the shield is raised.

q:	This will make the player drink a health potion(If they have any), restoring 60% of the player's maximum health.

g:	This toggles God Mode(Cheat code), which makes the player unkillable.

----------------------------------------------------------------------------------------------------------------------------------------------------------------------
Win Conditions
----------------------------------------------------------------------------------------------------------------------------------------------------------------------
This game is rogue-like, so your player will be permanently deleted upon death;
therefore, the only real objective of this game is to not die and get stronger.
The game was thought of with replayablility in mind (Randomly generated dungeons) even though that hasn't been implemented yet,
	so the only real win condition is to defeat all of the enemies in the game and to make your character much stronger..
Defeating all of the enemies on a floor will unlock the boss of that floor, upon defeating that boss you will gain access to the next floor (Of which there is only 1 right now).
