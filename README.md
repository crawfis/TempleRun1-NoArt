# Temple Run without Art

I recently watched a tutorial video by one of my favorite youtuber’s for Unity game Development, [samyam](https://www.youtube.com/@samyam). What struck me was the high dependency on various Unity components and the need to search for and find these components. I was also intrigued that the core mechanic of the Temple Run swipe-left or swipe-right was not a turn, but a teleportation (more on this later). I thought I would try to create an infinite runner template that was more **event** focused. This will also not require any physics, graphics, or audio. I have written several infinite runners over the years. I recall my two-year-old grandson playing Temple Run when it first came out. He loved the game but could not play it very well. I teach video game design and development and often tell my students to think about how to make their games playable for different audiences: young, old, physically challenged, cognitively challenged. Simple separation of concerns and data-driven configurations. The company I helped found ([www.rxgames.com](http://www.rxgames.com/)) focuses on using the human body to perform game mechanics to aid recovery from traumatic brain injury (TBI), stroke, knee replacements and other neuro- or ortho-rehabilitation. These movements take considerable amount of time to trigger the game action compared to traditional inputs. For example, going into and out of a deep squat can take a couple of seconds to perform. Allowing for deep configuration of your games allows swapping out various inputs, even as in our case using the same game but with different exercises which have differing times to execute (e.g., arm raise versus a squat).

## Rethinking Temple Run

If we look at the initial part of Temple Run, the player has to try to go to the left or right within a very specific time (or alternatively within a very specific distance). If they time this correctly the player is abruptly turned to go in the corresponding direction and is centered on this _new_ path. As was shown in the video, the player’s position and orientation is set to the start of this new path. The camera also changes abruptly such that the path is always oriented in the vertical screen space.

Thus, for a successful turn, we need an event to trigger before the player reaches a specific distance. The event should not trigger if the player is too far away from this distance. The simplest game we can thus make has an input event that is mapped to a turn event if the input is valid for the current state of the game. This state should include the distance the player has traversed or is from the end of the path and whether a left or right input event is valid. The game should provide some indication of how far or soon the player will reach the end of the path.

## Model-View-Controller (MVC) Approach

### Model

We have several distances we can track and utilize for _gameplay_:

1) **Total distance:** The distance the player has traveled across all paths. This is primarily used for the Score. Since we teleport as we switch paths, this is updated to include the entire segment length when we jump to a new path.

2) **Segment distance:** The length of the current path.

3) **Turn distance:** The distance before the death distance where a turn request will be considered valid. This can be adjusted to control the difficulty either as a constant or a progression.

4) **Turn direction(s):** In the original, this is whether the turn is to the left, right or either. In general, this can be abstracted to a set of possible next paths. Which path is selected depends on the input or controller. A better name for this model may be Enabled Teleporations.

5) **Track distance:** The summation of all segment distances the player has encountered, including the one it is currently on.

6) **Death distance:** The distance between the total track distance and the player’s total distance.

7) **Relative player distance:** The distance the player is along the current segment.

The last three can be computed from the first three. The Track Manager has configurations on the track distance minimum and maximum lengths as well as a starting track length. It is responsible for providing a new segment distance upon a successful turn.

Distances to obstacles and their type (e.g., head / slide, foot / jump or perhaps body / double jump) can also be added.

Note: If your _visual model_ allows the player to see several track segments ahead, this can be separated from the _gameplay model_ as a layer on top that controls the _gameplay model_. Try not to confuse the two, as the entire game can be played with no visuals at all.

### View

At a minimum, we need something to indicate to the player that a “turn” (teleportation) is coming up. We also need to know what the valid set of “turns” (teleportation ports) are. This is typically achieved through graphical models, but what if you are blind? A different view that uses a sound model could allow these games to be just as easily played without sight. Even with visual models, the gameplay can be enhanced with some SFX and VFX _juice_.

This sample simply displays the death distance of the model as a text. It does this for each valid teleportation and uses Unity’s UI Toolkit for this. It also displays the total distance. Obstacle distances can be easily added.

### Controller / Presenter

The controller maps input requests to actions. If a turn is requested, it will determine whether the request is valid: within the valid turn distance and a valid teleportation. If so, it will trigger a turn successfully event to update the model and the view. If the player distance reaches the end of the current path or the death distance, then an event is also triggered that will update the model.

## Changing the Input Controls

The original had a fun input of swiping left or swiping right for turns and then used the accelerometer for moving the character to one of the three lanes the character would be in. It used a swipe down to slide under obstacles or at any time just for fun. It used a swipe up to jump over obstacles and gaps. The swipe input is a rather long input, so performing consecutive turns requires a fair bit of time. Mapping this to a keyboard or gamepad key leads to a somewhat different experience. I can simply smash the keyboard long before the turn is available. To counteract this, I added an input cool-down.

In contrast, changing lanes using the accelerometer is very fast compared to even a keyboard press, at least for the minimal orientation changes required in the original game.

### Alternative inputs

· Assistive devices like large buttons or keyboards:

o [https://www.youtube.com/watch?v=iOIk4nRe87I](https://www.youtube.com/watch?v=iOIk4nRe87I)

o [https://www.youtube.com/watch?v=YU-RP4IJaLY](https://www.youtube.com/watch?v=YU-RP4IJaLY)

o [https://www.amazon.com/stores/AbleNet/page/C241332A-1019-4429-A853-BB455D2D3FE9?ref\_=ast\_bln](https://www.amazon.com/stores/AbleNet/page/C241332A-1019-4429-A853-BB455D2D3FE9?ref_=ast_bln)

· VR head swipe

· Video gesture tracking

· Voice

· Facial expression

· DDR or Dance Mat

· Pressure plate

· Bosa ball with accelerator

## Projects

Here is a list of experiments to use this framework to create better variants.

### UI Toolkit-based

1. Progress bar: linear or radial (think of the power in golf or fishing games). Player may stop at a turn until a successful input is achieved.
2. Color-based: Go from green to yellow to red to flashing red. Valid when color reaches a particular hue.
3. Visual (or SFX) pops up only when a turn is valid similar to Whack a Mole.
4. Quantize the distance to make it harder.
5. Display random distances greater than the death distance for invalid turns to make it harder (visual clutter).

### Sound-based

6. Change pitch.
7. Change speed.
8. Add sound clutter to make it harder.

### Multi-player

9. Local multi-player: Everyone stays in sync, but they die (or lose a life) if they fail a teleportation. Similar to red-light, green-light.
10. Cooperative multi-player (locally): It takes N out of M valid teleportation events to be successful.
11. Individual displays either split screen, networked, or using handheld devices and a shared master display using AirConsole ([https://assetstore.unity.com/packages/tools/game-toolkits/airconsole-42776](https://assetstore.unity.com/packages/tools/game-toolkits/airconsole-42776)).

### Graphics

12. Spline-based paths
13. Tiles
14. Problem solving

a. Math

b. Color

c. Shape

15. Endless possibilities for endless runners

### Other Games

16. Flappy Bird
17. Red-light, Green-light
18. All inputs (turns / teleportation) are initially valid, but are used up eventually, requiring the user to perform all actions (e.g., exercises).
19. Swipe left, swipe right games with a timer. Select favorites from random that get folded into back into the random algorithm with increasing probability. Timer runs out and you are stuck with that selection. Incentive to build up your favorites before time expires.

a. Avatar selection

b. Marry Me: possible spouse.

20. Any single tap game
