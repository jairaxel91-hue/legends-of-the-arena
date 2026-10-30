# LEGENDS OF THE ARENA

A 2D fighting game built with Unity. The project is structured as a real Unity project, with gameplay code separated into combat, characters, camera, and presentation systems.

## Project status

**Foundation / vertical-slice setup.** This repository currently contains the Unity project configuration and gameplay systems. Character illustrations, arena artwork, animation clips, audio, and final UI assets are production assets that still need to be imported and integrated.

## Requirements

- Unity **6000.0 LTS** (or a compatible Unity 6 release)
- 2D feature set
- Input System package

## Open the project

1. Clone or download this repository.
2. In Unity Hub, choose **Add > Add project from disk** and select the repository folder.
3. Open with Unity 6.
4. Open the scene you create under `Assets/_Project/Scenes` (the first playable scene is planned as `Arena.unity`).

## Current architecture

- `Scripts/Core`: game state and bootstrap.
- `Scripts/Characters`: fighter health, movement, and facing.
- `Scripts/Combat`: attacks, hit detection, hit reactions, and combo state.
- `Scripts/Camera`: arena framing.
- `Scripts/UI`: match HUD and round flow.
- `Art`: imported character, environment, VFX, and UI assets.
- `Scenes`: Unity scenes.

## Controls (prototype defaults)

| Action | Player 1 | Player 2 |
|---|---|---|
| Move | A / D | Left / Right |
| Jump | W | Up |
| Crouch | S | Down |
| Light attack | J | NumPad 1 |
| Heavy attack | K | NumPad 2 |
| Block | L | NumPad 3 |

The input layer is intentionally isolated so controls can later be rebound and replaced with gamepad support.

## Visual direction

The target is premium hand-painted 2D fighting-game presentation: detailed original fighters, layered architectural arenas, controlled rim lighting, atmospheric depth, expressive hit effects, and readable silhouettes. Temporary primitives are not considered final art.

## Development notes

See [docs/ART_DIRECTION.md](docs/ART_DIRECTION.md) and [docs/ROADMAP.md](docs/ROADMAP.md) for the production targets and implementation sequence.
