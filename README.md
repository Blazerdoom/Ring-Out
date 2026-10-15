# Ring Out

![Ring Out](docs/gifs/g3-knock.gif)

## About the game

A 3D arena game made in Unity 6. You're a knight on a round platform, and skeletons drop in and push toward you. You have no weapon. Your dash is your attack and your dodge, on the same button.

If your dash hits a skeleton, it gets knocked off the edge. If you're careless near the edge, you go off yourself. Your score is how many skeletons you knock off.

- **Controls:** WASD to move, Space to dash, R to restart
- **Engine:** Unity 6 (6000.6.0f1), URP
- **Time:** 2 days (28 to 29 September 2026), solo. Polish added on 30 September, character models on 13 October.

## Gameplay

<table>
  <tr>
    <td width="50%" valign="top"><img src="docs/gifs/g3-knock.gif" width="100%" alt="Two dashes knock two skeletons off the edge"><br>Two dashes, two skeletons knocked off the edge</td>
    <td width="50%" valign="top"><img src="docs/gifs/g3-crowd.gif" width="100%" alt="Skeletons drop in and crowd you"><br>Skeletons drop in and crowd you</td>
  </tr>
  <tr>
    <td width="50%" valign="top"><img src="docs/gifs/g3-miss.gif" width="100%" alt="Going too close to the edge and falling off"><br>Too close to the edge: you fall off</td>
    <td width="50%" valign="top"><img src="docs/gifs/g3-restart.gif" width="100%" alt="R starts a new round"><br>Game over card, then R starts a new round</td>
  </tr>
</table>

## Features

- Skeletons chase you by pushing toward your position with physics forces.
- Dash with a cooldown. A skeleton only gets knocked back if you hit it right after a dash.
- Anything that falls below the platform is out: a skeleton gives you +1, and you falling ends the game.
- The player is an animated knight and the enemies are animated skeletons.
- Hit feedback: a short freeze, sparks, camera shake, the ring flashes and the skeleton flashes white.
- A dash bar shows the cooldown. The sky changes color as you score. Sounds for dash, knock, land, fall and game over.
- I tuned it by playing: knock power 5 to 20, dash cooldown 2 s to 0.5 s, chase force 3 to 10.

## My contribution

- I wrote the player and enemy scripts. I reused my GameManager from Wobble Tower and my spawner and camera shake from Hole in the Wall, and changed them to fit this game.
- Claude (an AI assistant) set up the scene, UI and look, and gave me a first version of the knockback code. I typed it and changed it to use my own dash cooldown.
- Later I asked Claude to do the polish (sparks, shake, freeze, ring flash, dash bar, sky color, sounds) and to swap in the knight and skeleton models. That code is in `Fx.cs` and in the polish parts of the other scripts.

## Architecture

| Script | What it does |
|:-|:-|
| `PlayerMovement.cs` | Moving, the dash and its cooldown, the knockback, the dash bar, checking if you fell off |
| `EnemyChase.cs` | On every skeleton: pushes it toward the player, flashes when hit, gives a point when it falls off |
| `EnemySpawner.cs` | Drops a skeleton at a random spot every 2 seconds (reused from Hole in the Wall) |
| `GameManager.cs` | Score, best score, slow motion game over, restart, sounds, the freeze and the ring flash (reused from Wobble Tower) |
| `CameraShake.cs` | Camera shake and a small zoom on a dash (reused from Hole in the Wall) |
| `Fx.cs` | A static helper that spawns a particle burst anywhere |

How the scripts connect:

- 3 of the scripts came from my earlier games with small changes. That's the main reason this game took 2 days and my first one took 8.
- `GameManager` and `CameraShake` are singletons and `Fx` is a static class, so the player and the skeletons can use them without references.
- Every skeleton handles its own chasing and its own scoring, so the spawner just creates them and forgets about them.

What I'd change: the knockback lives inside `PlayerMovement`, and I'd move it into its own script. `GameManager` also does too much now (score, sounds, freeze, flash, sky). Every skeleton finds the player with `GameObject.Find("Player")`, which breaks if the player gets renamed. It would be better if the spawner passed the player in.

## How to open it

1. Open the project with Unity 6 (6000.6.0f1).
2. Open `Assets/Scenes/SampleScene.unity` and press Play.

## Credits

- Knight and skeleton models: KayKit by Kay Lousberg (CC0).
- Font: Rubik (SIL Open Font License, the license file is in `Assets/Fonts`).
- Sounds are made with code, no outside files.
