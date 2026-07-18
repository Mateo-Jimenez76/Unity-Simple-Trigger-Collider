#if UNITY_EDITOR
using UnityEditor;
using SimpleTriggerCollider.Editor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;
namespace SimpleTriggerCollider.Runtime.CommonUseCaseFunctions
{
    public class CommonUseCaseFunctions : ScriptableObject
    {
        /// <summary>
        /// Destroys the object corresponding to the collider passed in. The intended purpose is to be used as a
        /// dynamic function, meaning that the parameters get their data from UnityEvent<Collider,GameObject> automatically.
        /// </summary>
        /// <param name="collision">The Collider2D of the object to destroy</param>
        /// <param name="caller">The object that is calling this function</param>
        public static void DestroyObjectCollidedWith(Collider2D collision, GameObject caller)
        {
            PackageLogger.Log("Destroying " + collision.name + " because it collided with " + caller.name);
            Destroy(collision.gameObject);
        }

        /// <summary>
        /// Destroys the object corresponding to the collider passed in. The intended purpose is to be used as a
        /// dynamic function, meaning that the parameters get their data from UnityEvent<Collider,GameObject> automatically.
        /// </summary>
        /// <param name="collision">The Collider of the object to destroy</param>
        /// <param name="caller">The object that is calling this function</param>
        public static void DestroyObjectCollidedWith(Collider collision, GameObject caller)
        {
            PackageLogger.Log("Destroying " + collision.name + " because it collided with " + caller.name);
            Destroy(collision.gameObject);
        }

        /// <summary>
        /// Loads a scene asynchronously by name.
        /// </summary>
        /// <param name="sceneName">The name of the scene as listed in Build Settings</param>
        public static void LoadSceneAsync(string sceneName)
        {
            PackageLogger.Log("Loading scene " + sceneName + " asynchronously.");
            SceneManager.LoadSceneAsync(sceneName);
        }

        public static void Instantiate(Collider2D collision, GameObject caller)
        {
            if (!caller.TryGetComponent(out InstantiationInfo instantiationInfo))
            {
                Debug.LogError($"Cannot Instantiate object because no {nameof(InstantiationInfo)} component is found on '{caller.name}'",caller);
                return;
            }

            if(instantiationInfo.ObjectToInstantiate == null)
            {
                Debug.LogError($"Cannot Instantiate object because no object is assigned to '{nameof(instantiationInfo.ObjectToInstantiate)}' on '{caller.name}'", caller);
                return;
            }

            switch (instantiationInfo._LocationType)
            {
                case InstantiationInfo.LocationType.Transform:
                    Instantiate(instantiationInfo.ObjectToInstantiate, instantiationInfo.LocationTransform.position, Quaternion.identity);
                    break;
                case InstantiationInfo.LocationType.Vector3:
                    Instantiate(instantiationInfo.ObjectToInstantiate, instantiationInfo.Location, Quaternion.identity);
                    break;
                case InstantiationInfo.LocationType.Collision:
                    Instantiate(instantiationInfo.ObjectToInstantiate, collision.transform.position, Quaternion.identity);
                    break;
            }
        }

        public static void Instantiate(Collider collision, GameObject caller)
        {
            if (!caller.TryGetComponent(out InstantiationInfo instantiationInfo))
            {
                Debug.LogError($"Cannot Instantiate object because no {nameof(InstantiationInfo)} component is found on '{caller.name}'", caller);
                return;
            }

            if (instantiationInfo.ObjectToInstantiate == null)
            {
                Debug.LogError($"Cannot Instantiate object because no object is assigned to '{nameof(instantiationInfo.ObjectToInstantiate)}' on '{caller.name}'", caller);
                return;
            }

            switch (instantiationInfo._LocationType)
            {
                case InstantiationInfo.LocationType.Transform:
                    Instantiate(instantiationInfo.ObjectToInstantiate, instantiationInfo.LocationTransform.position, Quaternion.identity);
                    break;
                case InstantiationInfo.LocationType.Vector3:
                    Instantiate(instantiationInfo.ObjectToInstantiate, instantiationInfo.Location, Quaternion.identity);
                    break;
                case InstantiationInfo.LocationType.Collision:
                    Instantiate(instantiationInfo.ObjectToInstantiate, collision.transform.position, Quaternion.identity);
                    break;
            }
        }


        /// <summary>
        /// Deactivates the trigger associated with the specified collider when a collision occurs.
        /// </summary>
        /// <remarks>If the specified collider does not have a TriggerCollider component, no action is
        /// taken. This method logs the deactivation event for diagnostic purposes.</remarks>
        /// <param name="collision">The collider involved in the collision. Must contain a TriggerCollider component to be deactivated.</param>
        /// <param name="caller">The game object that initiated the collision event.</param>
        public void DeactivateTrigger(Collider collision, GameObject caller)
        {
            if(collision.TryGetComponent<TriggerCollider>(out TriggerCollider triggerCollider))
            {
                triggerCollider.enabled = false;
                PackageLogger.Log("Deactivated " + triggerCollider.name + " because it collided with " + caller.name);
            }
            else
            {
                PackageLogger.LogWarning("No TriggerCollider component found on " + caller.name + ". No trigger deactivated.");
            }
        }

        public void DeactivateTrigger(Collider2D collision, GameObject caller)
        {
            if(collision.TryGetComponent<TriggerCollider2D>(out TriggerCollider2D triggerCollider))
            {
                triggerCollider.enabled = false;
                PackageLogger.Log("Deactivated " + triggerCollider.name + " because it collided with " + caller.name);
            }
            else
            {
                PackageLogger.LogWarning("No TriggerCollider2D component found on " + caller.name + ". No trigger deactivated.");
            }
        }

        /// <summary>
        /// Logs collision information to the console. The intended purpose is to be used as a
        /// dynamic function, meaning that the parameters get their data from UnityEvent<Collider,GameObject> automatically.
        /// </summary>
        /// <param name="collision">The Collider of the object that entered the caller's collider</param>
        /// <param name="caller">The object that is calling this function</param>
        public static void LogCollision(Collider collision, GameObject caller)
        {
            PackageLogger.Log($"{collision.name} collided with {caller.name}(caller) at {collision.transform.position}.");
        }

        /// <summary>
        /// Logs collision information to the console. The intended purpose is to be used as a
        /// dynamic function, meaning that the parameters get their data from UnityEvent<Collider,GameObject> automatically.
        /// </summary>
        /// <param name="collision2D">The Collider2D of the object that entered the caller's collider</param>
        /// <param name="caller">The object that is calling this function</param>
        public static void LogCollision(Collider2D collision, GameObject caller)
        {
            PackageLogger.Log($"{collision.name} collided with {caller.name}(caller) at {collision.transform.position}.");
        }

#if UNITY_EDITOR
        public class Initializer : AssetPostprocessor
        {
            static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths, bool didDomainReload)
            {
                //Check that there is a valid location
                if (!AssetDatabase.IsValidFolder("Assets/Resources/Simple Trigger Collider"))
                {
                    Debug.LogWarning("Created Resources folder for 'Common Use Case Functions'");
                }

                if (!AssetDatabase.IsValidFolder("Assets/Resources"))
                {
                    AssetDatabase.CreateFolder("Resources","Simple Trigger Collider");
                    Debug.LogWarning("<color=yellow>Created</color> <color=cyan>Simple Trigger Collider</color> folder at <color=cyan>Assets/Resources/Simple Trigger Collider</color>");
                }

                //Try to load
                var UseCaseFunctions = AssetDatabase.LoadAssetAtPath<CommonUseCaseFunctions>($"Assets/Resources/Simple Trigger Collider/{nameof(CommonUseCaseFunctions)}.asset");

                //If the settings asset does not exist...
                if (UseCaseFunctions == null)
                {
                    Debug.LogWarning($"<color=yellow>Created '<color=lime>{nameof(CommonUseCaseFunctions)}</color>' asset</color> at <color=cyan>Assets/Resources/Simple Trigger Collider/{nameof(CommonUseCaseFunctions)}.asset</color>");
                    UseCaseFunctions = ScriptableObject.CreateInstance<CommonUseCaseFunctions>();
                    AssetDatabase.CreateAsset(UseCaseFunctions, $"Assets/Resources/Simple Trigger Collider/{nameof(CommonUseCaseFunctions)}.asset");
                }
            }
        }
#endif
    }

}
