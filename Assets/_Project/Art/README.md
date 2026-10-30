# Production art import guide

This folder is reserved for original, licensed production assets. The current prototype scene intentionally labels its simple sprites as temporary blockout; they are not the intended visual quality.

## Required deliverables for the first vertical slice

### Fighter sheets
- Two original fighters, designed specifically for this game.
- Consistent frame canvas, documented pivot, and transparent background.
- Separate sheets for idle, walk, dash, jump, crouch, light attack, heavy attack, block, hit reaction, knockdown, victory, and defeat.
- Keep silhouette and costume details readable at gameplay scale.

### Arena
- 16:9 background plate plus separate parallax layers.
- Foreground architecture, playable floor, midground structures, distant architecture, and atmospheric layer.
- Lighting and palette must support fighter readability.

### VFX and UI
- Hand-painted impact bursts, dust, trails, sparks, and special-move effects.
- Designed health bars, round markers, timer, character portraits, and versus screen.

## Import settings
Use Sprite (2D and UI), Multiple for sprite sheets, consistent pixels-per-unit per character set, Filter Mode according to the art finish, and documented compression settings. Set sprite pivots consistently before slicing animation frames.
