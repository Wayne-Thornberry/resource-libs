Proline.Engine is a engine and framework designed to allow creators to create a new game using the FiveM MP framework.
This engine aims to provide robust Level script support, component and extension support that makes creating a new game on top of the Rage engine 
more structured that base functionality FiveM core provides.

Extended off FiveM core. The engine framework will load custom components into the game and provide an easy way for the server and other resources to communicate
with them components. Components are fully synced between client and server.

The engine is broken down into 3 main parts
Extensions
- Higher level listener classes that can listen for engine events, engine api calls and core functionality support
- Extensions can be used for auditing or special functionality that you want to happen when any of the core Engine events happen
Components
- Mid level logic that provides 4 access points for you to call your code
- Call your code via commands by extending the component command class
- Call your code via Server event calling which supports returning values and delegets
- Call your code via API level within Level scripts
- Call your code via Component level handling on Load, Start, Stop and Unload
- Components will be retained as long the engine is told about them. Components can be started and stopped at any time, preventing any of the 4 above from being called
Level Scripts
- Lower level scripts that have one entry point, Execute. When execute is passed, a task is created to handle that.
- Level scripts are one and done sitation, so if you want a level script to retain itself, it must be lopped and awaited
- Level scripts can call Component APIs or core engine functionality, as well subscribe to engine level events that components can invoke.

Proline Engine supports libraries that wrap around existing Rage functionality or FiveM core functionality. You can use libraries to interact with Rage scaleforms or FiveM debug writing.
