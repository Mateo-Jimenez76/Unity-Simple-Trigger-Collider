# Features

### Components
| Name | Purpose |
|-------------|---------|
| TriggerCollider2D | Listens for OnTrigger2D functions and invokes the corresponding UnityEvent |
| TriggerCollider | Listens for OnTrigger functions and invokes the corresponding UnityEvent |

### Common Use Case Functions
I have included a scriptable object that contains a handful of helpful functions that are often used in the context of trigger colliders.
The reason I included this feature is so you can get implimenting and testing right away! 
And then later replace these functions with more polished and fine tuned versions if you wish.

- LoadSceneAsync(string sceneName)
- DestroyObjectCollidedWith(Collider2D collision)
- DestroyObjectCollidedWith(Collider collision)
- LogCollision2D(Collider2D collision)
- LogCollision(Collider collision)

>[!NOTE]
> The 2D/3D versions of functions will only appear when being used with their corresponding trigger colliders. 
> Thus it is impossible to use LogCollision(Collider collision), for example, with a TriggerCollider2D component

### Simple Health System Compatibility

This package has built in support for the [Simple Health System Package](https://github.com/Mateo-Jimenez76/Unity-Health-Script).
When the Simple Health System Package is present, additional use case functions will be available
- DamageObjectCollidedWith(Collider2D, int amount)
- DamageObjectCollidedWith(Collider, int amount)

The inspector will also change slighty in the TriggerCollider(2D) scripts to accommodate this change. 
A new sections labeled "Simple Health System" will appear with an int value below it titled "Damage Amount".
And before all UnityEvents there will be an enum labeled "Event Type" which controls the information that is passed in to the functions passed.
If you do not wish to you use the triggers for the purpose of damage dealing you can ignore this change.

<img width="699" height="570" alt="Screenshot of custom inspector for TriggerCollider2D.cs" src="https://github.com/user-attachments/assets/522813c3-b241-49a8-b00d-12643adc7aa0" />

### Package Settings
You can control the kind of logs that get put into the console(debug, warning, error) and the Collider(2D) that is created by default if one is missing.

### Documentation and Tooltips
Tooltips are included for variables in the inspector of all scripts and in the package settings.
and documentation comments are above all functions which describe their behavior and the parameters expected.

# How To Use

### Setting up the script

Add a TriggerCollider or TriggerCollider2D component to an object.

>[!NOTE]
> The only difference between TriggerCollider and TriggerCollider2D is what physics functions they listen to, OnTriggerEnter or OnTriggerEnter2D and so on, and the corresponding Collider or Collider2D component that they rely on. Functionality and flexibility are otherwise the same between scripts.

A component of type Collider(2D) is required by the script. If one is not already present, then a BoxCollider(2D) will be added to the object and marked as trigger.

>[!TIP]
> The Collider(2D) that is created by default can be changed in the settings menu of the package found in "Edit/Project Settings/Simple Trigger Colliders"

Once a component of type Collider(2D) is added it cannot be removed if it is the only Collider(2D) on the game object. If it does get removed than another Collider(2D) will be added to the object(only works in edit more). This is done to prevent the script from breaking as it relies on the precense of a Collider(2D) component.

### Using The Defined Unity Events

Each [UnityEvent<Collider(2D)>](https://docs.unity3d.com/6000.3/Documentation/ScriptReference/Events.UnityEvent.html) in the inspector corresponds with their respective OnTrigger functions.
- OnTriggerEnter(2D)
- OnTriggerStay(2D)
- OnTriggerExit(2D)

There are two ways to pass in data to functions called by the UnityEvent<Collider(2D)> 
1. Writing the data explicitly in the inspector
2. Having the data be passed in automatially to the OnTrigger calls.

The first option is fairly simple, assign the object, pick the function you wish to call and input the values.

>[!TIP]
> When using method 1 the function must only have 0-1 parameters, and be public in order to appear as an option in the inspector.

In order to use the Collider(2D) that is passed automatically to functions called by the events, the function must only take in one parameter 
and that one parameter must be a Collider(2D).

### Unity Event Execution Order

The execution order of functions listed within any given UnityEvent is relatively unknown, and thus it cannot be relied on. For example,

<img width="674" height="388" alt="UnityEvent Execution Example" src="https://github.com/user-attachments/assets/52f48d95-1492-4cab-9935-b24cc4128077" />

looking at the above image it would make sense that the object would...

1. Play a sound named "pickupCoin"
2. Play a Particle System effect
3. Log the collision
4. Deactivate the object

...all in that order when something enters its trigger collider. However when testing, the observed execution order is as follows

1. Log the collision
2. Deactivate the object

The sound and particle effect functions are ignored because those two components are on the object.
And since the object is now innactive the functions have no effect.

> [!NOTE]
> To read more about what an object being "innactive", "disabled", or "deactivated" means visit the official Unity documentation on [GameObject.SetActive](https://docs.unity3d.com/6000.3/Documentation/ScriptReference/GameObject.SetActive.html) 

<b>Solution 1</b>

You can move functionality away from the object that gets deactivated thus allowing the functions to have their effects go though.

> [!NOTE]
> Every function in a UnityEvent is called whether or not the invoking object becomes innactive due to one of those calls. Through personal investigations it would appear that the functions are called nearly at the same time, possibly [asynchronously](https://learn.microsoft.com/en-us/dotnet/csharp/asynchronous-programming/).

<b>Solution 2</b>

You can also take advantage of the OnTriggerStay and OnTriggerExit events. These events DO have an explicit execution order relative to each other that goes as follows

1. OnTriggerEnter
2. OnTriggerStay
3. OnTriggerExit

Thus we can move the SetActive(false) function call to OnTriggerStay to ensure that all other functions are called before we deactivate the object.

<b>Solution 3</b>

You can move the functionality into your own custom functions, thus allowing you to more directly control execution order and functionality.
Although this takes more work I do recommened it, once you are past initial testing and prototyping, as you can implement [error handling](https://www.geeksforgeeks.org/dsa/error-handling-in-programming/) and [conditional operations](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/boolean-logical-operators) to make more robust and full proof products.

### Using The Common Use Case Functions

In order to use the functions, you must first create the scriptable object in your project.
1. Right click and go to Create/SimpleTriggerColliders/CummonUseCaseFunctions to create the scriptable object at the current path in your project
2. Drag the scriptable object into the object field of the [UnityEvent](https://docs.unity3d.com/6000.3/Documentation/ScriptReference/Events.UnityEvent.html)
3. Select the function you wish to use
4. And done :D

> [!NOTE]
> The reason for the use of a scriptable object is to circumvent the limitation that UnityEvents can only call functions from object references and not scripts.

Another method of using these functions is directly through code. You can reference the functions by making a reference to CommonUseCaseFunctions.[Function Name] as all functions are static.







