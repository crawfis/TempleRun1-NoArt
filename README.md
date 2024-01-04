# Temple Run without Art

I recently watched a tutorial video by one of my favorite youtuber’s for Unity game Development, [samyam](https://www.youtube.com/@samyam). What struck me was the high dependency on various Unity components and the need to search for and find these components. I was also intrigued that the core mechanic of the Temple Run swipe-left or swipe-right was not a turn, but a teleportation (more on this later). I thought I would try to create an infinite runner that was more **event** focused.

## Rethinking Temple Run

If we look at the initial part of Temple Run, the player has to try to go to the left or right within a very specific time (or alternatively within a very specific distance). If they time this correctly the player is abruptly turned to go in the corresponding direction and is centered on this _new_ path. As was shown in the video, the player’s position and orientation is set to the start of this new path. The camera also changes abruptly such that the path is always oriented in the vertical screen space.

Thus, for a successful turn, we need an event to trigger before the player reaches a specific distance. The event should not trigger if the player is too far away from this distance. The simplest game we can thus make has an input event that is mapped to a turn event if the input is valid for the current state of the game. This state should include the distance the player has traversed or is from the end of the path and whether a left or right input event is valid. The game should provide some indication of how far or soon the player will reach the end of the path.
