V1
# Project Log

## Project Overview
- ASP.NET Core Razor Pages web application.
- Targeted `.NET 10`.
- Generated random doodle and drawing ideas.
- Displayed prompts inside procedurally generated ASCII-art backgrounds.
- Used a fullscreen black-and-white visual style.

## Original User Experience
- Landing page displayed a centered logo.
- Included the message:
  ```text
  No art ideas? No problem.
  Just generate and draw.
  ```
- A rounded white button generated new drawing prompts.
- Pressing Enter also generated a prompt.
- Button text counted generations using phrases such as:
  ```text
  Gimmi the second art idea!
  ```
- Prompts were generated through normal form POST requests.
- Using the browser Back button could display “Document has expired.”

## Original Prompt Display
- Generated ASCII canvases were approximately `120×50` characters.
- Prompt text and emojis were centered inside a cleared rectangular area.
- The entire ASCII block was scaled down with CSS transforms when it did not fit.
- On smaller screens, scaling could make the prompt unreadably small.
- The layout used fixed desktop-oriented dimensions.
- No decorative prompt borders existed.

## Original Animation
- JavaScript revealed each ASCII row using a scrambling effect.
- Characters gradually transformed into the final output.
- The animation expanded from centered rows, causing visible horizontal movement.
- The original animation delay was approximately `45ms`.
- Scramble characters included letters, numbers, symbols, currencies, Greek letters, and Cyrillic characters.
- No sounds accompanied the animation.

## Original Prompt Generator
- `PromptBuilder.cs` contained large dictionaries pairing words with emojis.
- Original categories included:
  - `things`
  - `foods`
  - `countries`
  - `expressions`
  - `genders`
  - `characters`
  - `animals`
  - `vibes`
  - `holdingItems`
  - `actions`
- Many categories were initialized but never used.
- The categories mixed grammatical and thematic roles.
- `things` included weapons, vehicles, instruments, flowers, technology, and tools.
- `actions` contained already-conjugated phrases such as:
  - `running`
  - `laughing`
  - `hiding in shadows`
  - `drawing in a notebook`

## Original Sentence Structures
The generator originally used a small random switch containing structures such as:

```text
A [character] [action] while holding a [held item].
A [vibe] [character] with a [thing].
A [character] and a [character] [action] together.
A [character] [action] near a [thing].
A [vibe] scene of a [character] holding an item beside a thing.
A [character], but with a baby [character].
A [character] attempting to [action] while another character is [action].
```

- Some structures combined incompatible grammar.
- Example possible output:
  ```text
  A pirate attempting to laughing while a punk is hiding in shadows.
  ```
- The generator did not account for base, continuous, or third-person verb forms.
- No difficulty or challenge selection existed.
- Rare special prompts did not exist.

## Original Prompt Failure
- The switch contained a visible fallback:
  ```text
  BROKEM!.
  ```
- When the random sentence selection fell outside supported cases, users received this as their prompt.
- One unused `foodsEmojiMap` field caused compiler warnings.

## Original ASCII Generator
- `AsciiPatternGenerator.cs` generated a `120×50` character canvas by default.
- Half of generated backgrounds used named patterns.
- Half used a procedural cellular automaton.
- Included patterns such as:
  - Wave
  - Diamond
  - Spiral
  - Maze
  - Checkerboard
  - Zigzag
  - Ripple
  - Crosshatch
  - Pulse
  - Weave
  - Sierpiński
  - Dune
  - Helix
  - Static
  - Vortex
- Pattern documentation described only seven patterns despite fifteen existing.

## Original Prompt Formatter
- `PromptFormatter.cs` generated the prompt and ASCII background.
- It cleared a centered rectangular region.
- Emoji and sentence lines were inserted directly into the ASCII character arrays.
- Centering relied on raw C# string length.
- Complex emoji sequences could shift or shorten the surrounding ASCII design.
- It contained multiple unused formatting helper methods.

## Original Styling
- Used a black background and white text.
- Used Consolas or Courier New for ASCII output.
- Prevented page scrolling with `overflow: hidden`.
- Included basic desktop and mobile font-size rules.
- No Challenge slider, Copy button, terminal overlay, sound controls, or mobile safe-area handling existed.

## Original Supporting Pages
- Included default Razor Pages files:
  - Privacy page
  - Error page
  - Shared layout
  - Bootstrap
  - jQuery
- The main page set `Layout = null`, so most shared layout assets were unused.

## Original Verification State
- Project built successfully.
- Build produced two warnings from the unused and uninitialized `foodsEmojiMap`.
- No automated tests were present.
- `PromptBuilder.cs` had an existing uncommitted change expanding the reachable sentence cases.

V2
# Development Log

## Initial Cleanup
- Removed unused fields and helper methods.
- Fixed incorrectly wired prompt categories.
- Updated outdated ASCII generator documentation.
- Resolved all compiler warnings.
- Confirmed clean builds with zero errors.

## Responsive ASCII Layout
- Replaced fixed `120×50` ASCII dimensions with viewport-calculated dimensions.
- Browser now measures available space and monospace character width.
- Added responsive prompt wrapping.
- Added responsive font sizing and comfortable outer margins.
- Prevented horizontal overflow on desktop and mobile.

## Prompt Generation Fixes
- Fixed `BROKEM!.` prompts caused by unbounded `random.Next()`.
- Corrected mismatched vibe, action, and held-item selections.
- Replaced the visible failure prompt with an internal exception.

## Browser History
- Implemented Post/Redirect/Get behavior.
- Added local session-based history for the latest 30 prompts.
- Browser Back and Forward now return to previous generated prompts without “Document has expired.”

## Animation
- Doubled the scramble animation speed.
- Changed animation alignment so text reveals from left to right.
- Restricted scramble characters to fixed-width ASCII.
- Assigned stable row widths to prevent mobile shifting.

## ASCII Borders
- Added randomly selected border styles.
- Removed the `▓▒░▓▒░` border.
- Added box-drawing, rounded, heavy, terminal, decorative, and borderless styles.

## Emoji Alignment
- Improved server-side emoji grapheme handling.
- Separated emojis from fixed-width ASCII rows.
- Emojis are now visually overlaid at the exact center without shifting the surrounding design.

## Terminal Effects and Sounds
- Added a terminal-style generation sequence:
  ```text
  > generating idea...
  > scanning imagination...
  > found prompt...
  ```
- Added local synthesized button sounds.
- Added continuous ASCII-driven generation audio.
- ASCII characters influence pitch, density, voice, and stereo position.
- Changed sound toward an old-school square-wave computer style.
- Added a retro completion beep.
- Added a clear low-to-high pitch sweep throughout generation.
- Increased generation sound volume slightly.
- Everything remains fully local.

## Copy Button
- Added one-click prompt copying.
- Copies only the readable prompt, not the entire ASCII design.
- Added copied/failure feedback.
- Preserved copy text through prompt history.

## Prompt Builder Refactor
- Reorganized vocabulary into grammatical categories:
  - Character nouns
  - Creature nouns
  - Object nouns
  - Props
  - Foods
  - Emotion adjectives
  - Atmosphere adjectives
  - Appearance adjectives
  - Places, scenes, concepts, materials, tools, and modifiers
- Added structured verb forms:
  - Base: `run`
  - Continuous: `running`
  - Third-person: `runs`
- Split verbs into transitive, activity, movement, and transformative groups.
- Eliminated malformed prompts such as “attempting to laughing.”
- Added automatic `A`/`An` correction.
- Added significantly more unusual characters, creatures, objects, settings, and props.

## Challenge Slider
- Added a responsive `0–10` Challenge slider.
- Added live labels from `Tiny Doodle` through `Nightmare`.
- Replaced the monolithic prompt switch with difficulty-aware families:
  - Simple subject
  - Modifier
  - Scene
  - Story moment
  - Composition study
  - Weird/contradiction
  - Quantity challenge
  - Compact technical challenge
  - Extreme challenge
- Higher levels vary complexity through quantity, composition, lighting, weather, constraints, and narrative.
- Challenge level persists through prompt generation and browser history.

## Mobile Improvements
- Reserved viewport space for the complete controls area.
- Prevented Copy from falling below mobile browser controls.
- Added safe-area padding.
- Made controls more compact on small screens.
- Recalculated canvas dimensions when Copy appears.
- Fixed JavaScript regressions that briefly stopped prompt generation and audio.

## Current Verification
- Project builds successfully with **0 warnings and 0 errors**.
