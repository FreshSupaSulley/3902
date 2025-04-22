Author of the code review
Date of the code review
Sprint number
Name of the .cs file being reviewed
Author of the .cs file being reviewed
Specific comments on code quality
A hypothetical change to make to the game related to file being reviewed and how the current implementation could or could not easily support that change 

Jared Willets
April 21, 2025
Sprint 5
Game/Entities/Player.cs
Erich Sullivan Boschert, Ty Fredrick

I think this class has developed a lot throughout this sprint and the previous one and it now seems to be much more functional and maintainable than before. The key system now works very seamlessly, we have excellent functions for health handling, our item use system now works flawlessly and I am very proud of how it has all worked out. It also seems that the use of various dictionaries for keyboard command initialization has made this section of the code significantly more maintainable than it was before.

If there was one change that I were to make to it, I would make it so that there is not just one set of sprites that is loaded in statically, and instead there were a static load player sprites function that would create a dictionary of dictionaries of sprites, and each player would instead be assigned a dictionary of sprites for the body parts and alignments instead of what is currently done. I think this could be somewhat easily done with more time if the game were to continue growing, but given the functionality that is already in the game now, I don't think it is necessary for this sprint at least.