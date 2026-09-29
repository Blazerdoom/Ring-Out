# Ring Out

![Ring Out](docs/gifs/g3-knock.gif)

## About the game

You stand on a round platform. Enemies drop in and push toward you. You have no weapon. Your dash is your attack and your dodge, on the same button.

If your dash hits an enemy, it gets knocked off the edge. If you miss, the dash can carry you off the edge yourself. Your score is how many enemies you knock off.

## Project info

| | |
|:-|:-|
| **Engine** | Unity 6 (6000.6.0f1), URP, 3D |
| **Role** | Solo: game idea and programming |
| **Development time** | 2 days (28–29 September 2026) |
| **Controls** | WASD to move, Space to dash, R to restart |

## My contribution

- I wrote the player and enemy scripts. I reused my own GameManager from Wobble Tower and my spawner and camera shake from Hole in the Wall, and changed them to fit this game.
- An AI assistant (Claude) set up the scene, UI and look, and gave me a first version of the knockback code. I typed it and changed it to use my own dash cooldown.

## Key features

- Enemies chase you by pushing toward your position.
- Dash with a cooldown. An enemy only gets knocked back if you hit it right after a dash.
- Anything that falls below the platform is out: an enemy gives you +1, and you falling ends the game.
- Camera shake on every hit, a dash trail, and slow motion on game over.
- Tuned by playing: knock power 5 → 20, dash cooldown 2 s → 0.5 s, chase force 3 → 10.

## Gameplay

<table>
  <tr>
    <td width="50%" valign="top"><img src="docs/gifs/g3-knock.gif" width="100%" alt="Dashing into an enemy knocks it off the edge"><br>Dashing into an enemy knocks it off the edge</td>
    <td width="50%" valign="top"><img src="docs/gifs/g3-crowd.gif" width="100%" alt="Enemies drop in and crowd you"><br>Enemies drop in and crowd you</td>
  </tr>
  <tr>
    <td width="50%" valign="top"><img src="docs/gifs/g3-miss.gif" width="100%" alt="A risky dash toward the edge carries you off"><br>A risky dash toward the edge carries you off</td>
    <td width="50%" valign="top"><img src="docs/gifs/g3-restart.gif" width="100%" alt="R starts a new round"><br>R starts a new round</td>
  </tr>
</table>

## Scripts

| Script | What it does |
|:-|:-|
| `PlayerMovement.cs` | Moving, the dash and its cooldown, the knockback, checking if you fell off |
| `EnemyChase.cs` | Pushes the enemy toward the player, gives a point when it falls |
| `EnemySpawner.cs` | Drops an enemy at a random spot every 2 seconds (from Hole in the Wall) |
| `GameManager.cs` | Score, slow motion game over, restart (from Wobble Tower) |
| `CameraShake.cs` | Shakes the camera for a moment (from Hole in the Wall) |

If I kept working on it, I would move the knockback out of PlayerMovement into its own script.

## How to open it

1. Open the project with Unity **Unity 6** (6000.6.0f1).
2. Open `Assets/Scenes/SampleScene.unity` and press Play.

## Credits

- Font: Rubik (SIL Open Font License, the license file is in `Assets/Fonts`).
