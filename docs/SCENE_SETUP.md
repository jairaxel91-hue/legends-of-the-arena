# Unity scene assembly — prototype

The repository contains the Unity project foundation and gameplay scripts. It does **not** yet contain a finished playable scene or final character/arena art. This guide describes the minimum scene wiring needed to test the scripts after opening the project in Unity 6.

## Required layers

Create a layer named `Ground` in **Edit > Project Settings > Tags and Layers**. Assign the arena floor to this layer. Set each fighter's `groundMask` to include only `Ground`.

## Arena scene

1. Create and save a scene at `Assets/_Project/Scenes/Arena.unity`.
2. Add a 2D orthographic camera at (0, 2, -10). Add `ArenaCamera`.
3. Create an arena floor with a `BoxCollider2D`; assign it to the `Ground` layer.
4. Create two fighter roots. Each needs a `Rigidbody2D` (Dynamic, Freeze Rotation Z), a `CapsuleCollider2D`, `FighterController`, `FighterHealth`, and `FighterCombat`.
5. On Player 1, leave both `playerTwo` toggles off. On Player 2, enable `playerTwo` on both `FighterController` and `FighterCombat`.
6. Add a child transform named `GroundCheck` near each fighter's feet and assign it to `FighterController.groundCheck`. Keep the check just below the collider and set its radius small enough not to overlap the fighter.
7. Set each `FighterCombat.fighterMask` to the layer used by fighters (not the Ground layer). If both fighters use Default, select Default.
8. Add a `MatchManager` object and assign both `FighterHealth` references.
9. Assign both fighter transforms to `ArenaCamera`.

## Prototype controls

- Player 1: A/D move, W jump, S crouch, J light, K heavy, L block.
- Player 2: Left/Right move, Up jump, Down crouch, NumPad 1 light, NumPad 2 heavy, NumPad 3 block.
- Press R after a round ends to restart the round.

## Important limitations

This is a code foundation, not a finished game. The current combat system is deliberately a first-pass hitbox prototype: it does not yet implement a full animation-state-driven combo graph, hit-stop, frame advantage, input buffering, throws, special moves, CPU AI, controller rebinding, or rollback/netcode. Production character illustrations, animation sprite sheets, arena artwork, VFX, sound, and the final HUD still need to be created and integrated. Do not present the prototype shapes or temporary art as final visual quality.
