Jared Willets
April 21, 2025
Sprint 5
Game/Rooms/Door.cs
Erich Sullivan Boschert, Jared Willets, Ty Fredrick, Mingyu Choi
10 minutes + time developing

I think throughout this sprint, the door code has become significantly more readable. Every variable name now seems to fit the functionality described and because the code is more complete, it is much easier to understand. Fixes to player.HasKey() has also made this code much more readable and adapting it to the new players has been made easier by the already readable code that existed before that feature was implemented. We now have the door get toggled to open and have it be walkable in the update command instead of just the door initialization in LoadRoom, which has improved smoothness and overall readability for room switching systems. More comments have been added to make the code even more easy to read, along with the tilenums being clearly associated with the door's position in the room.