_<details><summary>Original README</summary>_
Welcome to the AF Interview Project. This test consists of 2 parts. The first one requires just some final touches to the existing code, while the second one is a bit more complex and requires you to implement a new system from scratch.

Your tasks for today are as follows:
Part 1:
a) Find a bug in the code logic and fix it
b) Optimize the ItemsManager class code
c) Extend the whole system by introducing a concept of a consumable item, that upon using, will either add money or a different item to the inventory.

Part 2:
Implement a system that will be used to execute fights between certain army units.

Unit types: Long Sword Knight, Archer, Druid, Catapult, Ram

Unit should specify its:
-unit attributes (Light, Armored, Mechanical - each unit can have one or more attributes at once)
-health points
-armor points
-attack interval (the number of turns that need to pass to be able to perform an attack again, so a value of 1 means that a unit can perform the attack each turn)
-attack damage
-an optional attack damage override value against units with a specific attribute

The damage dealt should be calculated by taking the attack damage and subtracting the armor points. The final damage dealt can not fall below the value of 1 in any case.
When the health points of a unit reach 0, the unit should be considered unable to fight and should be removed from the army.

Set up a turn-based fight between 2 armies, one spawned on the left side of the screen and the other on the right side:
2 Long Sword Knights
1 Druid
1 Ram

versus

3 Archers
1 Catapult
1 Ram

The fight turn order should be determined randomly at the start of the fight and kept for its whole duration.
Assume that all of the units are always within the required range to attack any enemy unit.
Make sure that editing all of the balance-related values is designer-friendly.
Providing an animated visual representation of the fight will be considered a plus.

You'll be graded based on how well you've completed these tasks and on the quality of your code. 
If you're not 100% sure how something should work, try to make it work however *you* would see fit.

One more thing: get a public git repository for this task and commit your changes as you go. We'd like to see your repository work ethic in action.

Email us at work@ancientforgestudio.com with the repository link and a report of what you've managed to do within the next 24 hours.

Good luck!
</details>

# Welcome to the AF Interview Project.
This test consists of 2 parts. The first one requires just some final touches to the existing code, while the second one is a bit more complex and requires you to implement a new system from scratch. You'll be graded based on how well you've completed these tasks and on the quality of your code. If you're not 100% sure how something should work, try to make it work however *you* would see fit.

Email us at work@ancientforgestudio.com with the repository link and a report of what you've managed to do within the next 24 hours.
> I've taken care of everything specified in this document, and even a little extra. I missed out on `health bars`, `damage pop-ups` and some light `decoupling` out of my personal goals, though. I could definitely do those in an hour or two, but I've already spent more time on this interview assigment than all the other ones summed up ;) Well, to be honest, it was the most fun one!

## Part 1:
- [x] get a public git repository for this task and commit your changes as you go. We'd like to see your repository work ethic in action.
  > I wouldn't imagine it any other way ;)
- [x] Find a bug in the code logic and fix it
  > the bug was caused by removing elements from the list while still iterating over. I fixed it by iterating over a COPY of the list. Another solution is to make a sublist of to-be-removed elements in one loop, then remove them in another loop.
- [x] Optimize the ItemsManager class code
  > `ItemManager` was calling `FindObjectOfType<T>()` in its `Update()` to print the inventory. Not only is this bad approach (as it assumes the component you're looking for is the only/first one in the scene), but it's also very inefficient. `Find` methods should never be used in production code, let alone in `Update()`. The rest of `ItemManager` could use some refactoring, but nothing else seemed to be as heavy performance-wise.
- [x] Extend the whole system by introducing a concept of a consumable item, that upon using, will either add money or a different item to the inventory.
  > I implemented a simple inspector-friendly (SO-driven) system for specifying the effects of a usable item. Each item has a list of `ItemEffects` which will be trigger when the item gets used. It consists of:
  > - `ItemEffectScript` - SO that defines the logic of the effect
  > - `string` - semicolon-separated list of values that will get parsed contextually by the above script
  > 
  > there is an example item (called `Treasure Chest`, with a sphere mesh) that makes use of the system. When it's used, it gives you 500$ and a `Cursed Coin` item which sells for -666$.

## Part 2:
- [x] Implement a system that will be used to execute fights between certain army units.
  - Unit types: Long Sword Knight, Archer, Druid, Catapult, Ram
  > I implemented a fairly general (as time allowed) system that allows for any number of armies (I call them "teams") to fight. The combat goes on as long as there's at least 2 opposing units alive. Each `Unit` component consists of:
  > - `UnitDefinition` - SO that contains constant data required to create the unit (basically base stats)
  > - `UnitVisuals` - 1 of 5 prefabs that differ only by mesh and scale
  > - `Team` - color-coded team to which the unit belongs. Units from the same team are allies, while from other teams - enemies.

Unit should specify its:
- [x] unit attributes (Light, Armored, Mechanical - each unit can have one or more attributes at once)
- [x] health points
- [x] armor points
- [x] attack interval (the number of turns that need to pass to be able to perform an attack again, so a value of 1 means that a unit can perform the attack each turn)
  > I decided to name it `Cooldown`, but made it 1-based as specified
- [x] attack damage
- [x] an optional attack damage override value against units with a specific attribute
  > instead of an absolute damage override, I decided to implement `DamageModifier`s - positive or negative modifiers to the base damage based on the target's attributes. This allows for stacking multiple modifiers, for example if a certain unit deals +3 damage to `Armored` units and -2 damage to `Mechanical` units, it will deal +1 damage to `Armored Mechanical` unit. 
  - [x] The damage dealt should be calculated by taking the attack damage and subtracting the armor points. The final damage dealt can not fall below the value of 1 in any case.
When the health points of a unit reach 0, the unit should be considered unable to fight and should be removed from the army.
- [x] Set up a turn-based fight between 2 armies, one spawned on the left side of the screen and the other on the right side:  
_2x Long Sword Knight, 1x Druid, 1x Ram_ **VERSUS** _3x Archer, 1x Catapult, 1x Ram_
  - [x] The fight turn order should be determined randomly at the start of the fight and kept for its whole duration. Assume that all of the units are always within the required range to attack any enemy unit. Make sure that editing all of the balance-related values is designer-friendly. Providing an animated visual representation of the fight will be considered a plus.
  > I went the extra mile and made simple animations with DOTween ;)

# Good luck!
> Thank you and likewise <3
