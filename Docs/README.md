**English** | [简体中文](README_zh-CN.md)

<h1 align="center">In The Void</h1>

A Metroidvania-style game based on Unity™, supporting keyboard and mouse, controller, and touchscreen (Android™ platform) operations. Designed for enterprise-level technical research.

## Features

- Professional-grade system abstraction design,
- Dynamic screen capture mechanism,
- Carefully polished numerical balancing,
- ......

## Feature Implementation Roadmap

- Controls
  - [x] Keyboard and mouse
  - [x] Controller
  - [x] Touchscreen
- Core Gameplay
  - [ ] Efficient movement
  - [ ] Skill tree
  - [x] Save
  - [ ] Inventory
  - [ ] Accessories
  - [ ] Shop
  - [ ] Teleportation network
  - [ ] Multiplayer
  - [ ] Chat
  - [ ] Internal commands
- Customer Support
  - [ ] Settings
  - [ ] Cloud sync
  - [ ] Activation verification

## Controls

Players can interact with the system in various ways.

> The controller example uses the version bundled with the Xbox Series S/X console.

| Button                                                                                                   | Function                                                                                                                                                                                                                                                    |
| -------------------------------------------------------------------------------------------------------- | ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| <kbd>A</kbd> or ![Left Stick](../Assets/30px-XboxOne_Left_Stick.png) Left                                | Move left                                                                                                                                                                                                                                                   |
| <kbd>D</kbd> or ![Left Stick](../Assets/30px-XboxOne_Left_Stick.png) Right                               | Move right                                                                                                                                                                                                                                                  |
| Left <kbd>Shift</kbd> or ![Left Stick](../Assets/30px-XboxOne_Left_Stick.png) Press down                 | Dash                                                                                                                                                                                                                                                        |
| <kbd>Space</kbd> or ![A](../Assets/30px-XboxOne_A.png)                                                   | Jump                                                                                                                                                                                                                                                        |
| ![Left Mouse Button](../Assets/40px-Keyboard_Black_Mouse_Left.png) or ![X](../Assets/30px-XboxOne_X.png) | Normal attack. No matter how fast the user presses, it is constrained by the attack speed system. When near an NPC, it initiates dialogue. When near a cross statue, it activates the teleportation network. When near a goddess statue, it opens the shop. |
| <kbd>Q</kbd> or ![Y](../Assets/30px-XboxOne_Y.png)                                                       | Charge attack, causing significant knockback. Releasing before the final strike will cause the attack to fail.                                                                                                                                              |
| <kbd>E</kbd> or ![B](../Assets/30px-XboxOne_B.png)                                                       | Use skill                                                                                                                                                                                                                                                   |
| <kbd>Tab</kbd> or ![Left Shoulder Button](../Assets/30px-XboxOne_LB.png)                                 | Open inventory                                                                                                                                                                                                                                              |

On devices with a touchscreen, the application features an explicit HUD and is therefore not described here.

## Entity System

Characters (Avatar), highly interactive objects in the environment, and enemies are collectively referred to as **Entities**.

| Name                | Description                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                              | Base Attack | Defense | Base Speed | Base Cooldown | Special Skill                      |
| ------------------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | ----------- | ------- | ---------- | ------------- | ---------------------------------- |
| Target              | The target does not take any actions on its own.                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                         | 0           | 0       | 0          | 999           | -                                  |
| Hyena               | The hyena continuously searches for nearby characters. If none are found, it wanders left and right; otherwise, it targets the nearest one, doubling its speed, moving towards the target on the x-axis, attempting to jump if it encounters a wall and is not airborne.                                                                                                                                                                                                                                                                                                                                                                                                                 | 10          | 0       | 1          | 3             | -                                  |
| Tortoise            | The tortoise continuously searches for nearby entities. If none are found, it wanders left and right; otherwise, it flies towards the sky, moving towards the target on the x-axis, causing massive damage upon landing.                                                                                                                                                                                                                                                                                                                                                                                                                                                                 | 25          | 30      | 1          | 5             | -                                  |
| Corrupted Sunflower | The Corrupted Sunflower (hereafter referred to as "Sunflower") ignores all collision bodies, freely traversing through air and terrain. Fearful of sunlight, it always hides in the terrain, watching the player from afar. Periodically, it rushes at the player, causing huge damage and knockback, then burrows back into the terrain. When its health drops below 50%, it summons plant missile projectiles from all directions to shoot at the player, significantly reducing the interval between dashes. If the player attempts to flee (more than 30f distance from the Sunflower's spawn point), the damage of each missile increases to 9999, and its speed increases by 300%. | 33          | 0       | 1          | 10            | "Desperate Dash"; "Tangible Wrath" |

## Skill System

Skill is the only way to actively affect other entities (including itself).

Skills are limited by both **Skill Points/Mana** and **Cooldown** (CD), requiring both to meet the consumption amount for a successful release.

All entities unconditionally master the **Plain Attack**, a special skill that does not consume skill points. For characters, each plain attack searches for all attackable targets within a small range (not limited to enemies) and deals damage once per target. Regardless of hitting the target, it resets the cooldown counter. For enemies, each time they touch a character, they attempt a plain attack, attacking the first character they touch if multiple are touched in a short time.

### Characters

Players can equip one of these in the skill slot and trigger it conditionally.

| Name    | Description                                                                                                                                                                                                            | Skill Point Cost | Cooldown (seconds) | Acquisition Method                     |
| ------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | ---------------- | ------------------ | -------------------------------------- |
| Shoot   | Throws a projectile towards the cursor or right stick direction, dealing 15 damage to all attackable units within a radius of 3 upon hitting a wall or unit.                                                           | 5                | 0.7                | Offer 50 tokens to the Goddess Statue  |
| Grapple | Throws a grappling hook towards the cursor or right stick direction, dragging the player to the attachment point if it hits a wall, then releases the hook. If the wall is more than 20 units away, nothing happens.   | 10               | 0.7                | Offer 120 tokens to the Goddess Statue |
| Enchant | Makes all enemy units within a radius of 30 around the player harmless (essentially making the player invisible and immune to enemy damage but still affected by the environment) for 5 seconds. Effects do not stack. | 50               | 20                 | Found in specific wooden chests        |
| Lament  | Consumes 50% of the player's max HP (down to 1 if insufficient), granting 70% damage reduction and 30% attack speed increase for 25 seconds. Effects do not stack.                                                     | 50               | 45                 | Defeat the Corrupted Sunflower         |

### Enemies

Enemies will continuously attempt to use these skills when they see a character.

| Name             | Description                                                                                                                                                                                                                                                                                                                                                                    | Skill Point Cost | Cooldown (seconds) | Initiator           |
| ---------------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ | ---------------- | ------------------ | ------------------- |
| "Desperate Dash" | Targets the player's current location O, finds the symmetrical point B relative to O, and moves rapidly towards B, dealing 25 damage to players along the path. If the player's position changes, the path does not change.                                                                                                                                                    | 10               | 10                 | Corrupted Sunflower |
| "Tangible Wrath" | When health is below 50%, summons a plant missile outside the screen that tracks the nearest player, ignoring damage, dealing 1 damage on hit and disappearing. If the player is more than 30f from the spawn point, the missile's damage increases to 9999, and its speed increases by 300%. While missiles are present, all the entity's skill cooldowns are reduced by 70%. | 1                | 1                  | Corrupted Sunflower |

## Effect System

Effects are objects that attach to entities for a limited time, equivalent to class decorators.

| Name | Description                                                                        |
| ---- | ---------------------------------------------------------------------------------- |
| Fear | Increases entity's attack power and speed by 30%, but doubles the damage received. |

## IoC System

Communication between entities primarily relies on the inversion of control system, ensuring type safety.

| Participant Name                        | Description                                         |
| --------------------------------------- | --------------------------------------------------- |
| Character                               | Player-controlled entity                            |
| Character Camera                        | The screen view seen by the player                  |
| World                                   | Manages creature spawning and double-jump detection |
| Inventory                               | Collection of items held by the player              |
| Shop                                    | Collection of items available for tokens            |
| Shop Statue                             | Triggers events related to the shop                 |
| Global Notification Component           | Main subtitles                                      |
| Secondary Global Notification Component | Secondary subtitles                                 |

## About

1. Unity is a registered trademark of Unity Technologies.
2. Android is a registered trademark of Google Inc.
3. Except for parts related to various commercial companies, Penyo owns all rights to the source code. Unauthorized use of any part is prohibited. Legal responsibilities arising from self-compilation or modification are entirely borne by the final editor.
