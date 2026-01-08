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

### Simple Health System Compatibility

This package has built in support for the [Simple Health System Package](https://github.com/Mateo-Jimenez76/Unity-Health-Script).
When the Simple Health System Package is present, additional use case functions will be available
- DamageObjectCollidedWith(Collider2D, int amount)
- DamageObjectCollidedWith(Collider, int amount)

The inspector will also change slighty in the TriggerCollider(2D) scripts to accommodate this change. 
A new sections labeled "Simple Health System" will appear with an int value below it titled "Damage Amount".
And before all unityEvents there will be an option 
If you do not wish to you use the triggers for the purpose of damage dealing you can ignore this change.

IMAGE OF CHANGED INSPECTOR

### Documentation and Tooltips
Tooltips are included for the varibles in the inspector of all scripts 
and documentation comments are above all functions which describe their behavior and the parameters expected.

# How To Use

### Setting up the script

Add a TriggerCollider or TriggerCollider2D component to an object.

>[!NOTE]
> The only difference between TriggerCollider and TriggerCollider2D is what physics functions they listen to, OnTriggerEnter or OnTriggerEnter2D and so on, and the corresponding Collider or Collider2D component that they rely on. Functionality and flexibility are otherwise the same between scripts.

A component of type Collider(2D) is required by the script. If one is not already present, then a BoxCollider(2D) will be added to the object and marked as trigger.

>[!TIP]
> The Collider(2D) that is created by default can be changed in the settings menu of the package found in "Edit/Project Settings"

Once a component of type Collider is added it cannot be removed (if it is the only Collider(2D) on the game object) due to the effect of [RequireComponent(typeof(Collider))](https://docs.unity3d.com/ScriptReference/Collider.html). This is done to prevent the script from breaking as it relies on the precense of a Collider(2D) component.

### Using The Defined Unity Events

Each [UnityEvent<Collider(2D)>](https://docs.unity3d.com/6000.3/Documentation/ScriptReference/Events.UnityEvent.html) in the inspector corresponds with their respective OnTrigger functions.
- OnTriggerEnter(2D)
- OnTriggerStay(2D)
- OnTriggerExit(2D)

There are two ways to pass in data to functions called by the UnityEvent<Collider(2D)> 
1. Writing the data explicitly in the inspector
2. Having the data be passed in through the OnTrigger calls.

The first option is fairly simple, assign the object, pick the function you wish to call and input the values.

>[!TIP]
> When using method 1 the function must only have 0-1 parameters, and be public in order to appear as an option in the inspector.

In order to use the Collider(2D) that is passed automatically to functions called by the events, the function must only take in one parameter 
and that one parameter must be a Collider(2D).

### Using The Common Use Case Functions

In order to use the functions, place the CommonUseCaseFunctions scriptable object found at /SimpleTriggerCollider/Runtime/CommonUseCase in the object field of the Unity Event you wish to use the function in. Then select the corresponding function you wish to use. \
The reason for the use of a scriptable object is to circumvent the limitation that UnityEvents can only call functions from object references and not scripts. You cannot create this scriptable object nor does it have any parameters in the inspector to change.

