# Combat design specification

## Core match
- Two fighters share one camera and one arena.
- Best of three rounds, with configurable round timer.
- Round ends on knockout or timer expiration.
- Match ends when a fighter reaches the configured round wins.

## Neutral game
- Walk, dash, jump, crouch, and block.
- Light attacks are quick with lower reward.
- Heavy attacks are slower with higher reward and recovery.
- Attacks must have explicit startup, active, and recovery phases.

## Required next combat systems
1. Buffered input and command recognition.
2. Animation event-driven hitboxes and hurtboxes.
3. Hit stun, pushback, hit-stop, and cancel windows.
4. Combo scaling and combo counter.
5. Throws, special moves, and meter.
6. CPU opponent with difficulty profiles.
7. Gamepad support and rebinding.

## Fairness rules
- Block only protects the front-facing side.
- A hit can only be applied once per attack instance.
- Simultaneous knockouts must resolve deterministically.
- Pausing must restore the previous time scale correctly.
