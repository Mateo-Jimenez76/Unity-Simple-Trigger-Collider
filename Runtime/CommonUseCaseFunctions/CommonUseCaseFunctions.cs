#if UNITY_EDITOR
using UnityEditor;
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
            Debug.Log($"<color=lime>{nameof(CommonUseCaseFunctions)}</color> <color=yellow>destroyed</color> <color=cyan>{collision.name}</color> because it collided with <color=cyan>{caller.name}</color>.", caller);
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
            Debug.Log($"<color=lime>{nameof(CommonUseCaseFunctions)}</color> <color=yellow>destroyed</color> <color=cyan>{collision.name}</color> because it collided with <color=cyan>{caller.name}</color>.", caller);
            Destroy(collision.gameObject);
        }

        /// <summary>
        /// Loads a scene asynchronously by name.
        /// </summary>
        /// <param name="sceneName">The name of the scene as listed in Build Settings</param>
        public static void LoadSceneAsync(string sceneName)
        {
            Debug.Log($"<color=lime>{nameof(CommonUseCaseFunctions)}</color> <color=yellow>loading</color> scene <color=cyan>{sceneName}</color> asynchronously.");
            SceneManager.LoadSceneAsync(sceneName);
        }

        public static void Instantiate(Collider2D collision, GameObject caller)
        {
            if (!caller.TryGetComponent(out InstantiationInfo instantiationInfo))
            {
                Debug.LogError($"<color=lime>{nameof(CommonUseCaseFunctions)}</color> could not instantiate an object for <color=cyan>{caller.name}</color>, because <color=red>no {nameof(InstantiationInfo)} component was found</color>. Add an {nameof(InstantiationInfo)} component to <color=cyan>{caller.name}</color>.", caller);
                return;
            }

            if(instantiationInfo.ObjectToInstantiate == null)
            {
                Debug.LogError($"<color=lime>{nameof(CommonUseCaseFunctions)}</color> could not instantiate an object for <color=cyan>{caller.name}</color>, because <color=red>no object is assigned to {nameof(instantiationInfo.ObjectToInstantiate)}</color>. Assign an object to <color=cyan>{nameof(instantiationInfo.ObjectToInstantiate)}</color> on <color=cyan>{caller.name}</color>.", caller);
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
                Debug.LogError($"<color=lime>{nameof(CommonUseCaseFunctions)}</color> could not instantiate an object for <color=cyan>{caller.name}</color>, because <color=red>no {nameof(InstantiationInfo)} component was found</color>. Add an {nameof(InstantiationInfo)} component to <color=cyan>{caller.name}</color>.", caller);
                return;
            }

            if (instantiationInfo.ObjectToInstantiate == null)
            {
                Debug.LogError($"<color=lime>{nameof(CommonUseCaseFunctions)}</color> could not instantiate an object for <color=cyan>{caller.name}</color>, because <color=red>no object is assigned to {nameof(instantiationInfo.ObjectToInstantiate)}</color>. Assign an object to <color=cyan>{nameof(instantiationInfo.ObjectToInstantiate)}</color> on <color=cyan>{caller.name}</color>.", caller);
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
                Debug.Log($"<color=lime>{nameof(CommonUseCaseFunctions)}</color> <color=yellow>deactivated</color> <color=cyan>{triggerCollider.name}</color> because it collided with <color=cyan>{caller.name}</color>.", triggerCollider);
            }
            else
            {
                Debug.LogWarning($"<color=lime>{nameof(CommonUseCaseFunctions)}</color> did not deactivate a trigger, because <color=red>no {nameof(TriggerCollider)} component was found on {collision.name}</color> (collided with <color=cyan>{caller.name}</color>). Add a {nameof(TriggerCollider)} component to <color=cyan>{collision.name}</color> if it should deactivate on collision.", caller);
            }
        }

        public void DeactivateTrigger(Collider2D collision, GameObject caller)
        {
            if(collision.TryGetComponent<TriggerCollider2D>(out TriggerCollider2D triggerCollider))
            {
                triggerCollider.enabled = false;
                Debug.Log($"<color=lime>{nameof(CommonUseCaseFunctions)}</color> <color=yellow>deactivated</color> <color=cyan>{triggerCollider.name}</color> because it collided with <color=cyan>{caller.name}</color>.", triggerCollider);
            }
            else
            {
                Debug.LogWarning($"<color=lime>{nameof(CommonUseCaseFunctions)}</color> did not deactivate a trigger, because <color=red>no {nameof(TriggerCollider2D)} component was found on {collision.name}</color> (collided with <color=cyan>{caller.name}</color>). Add a {nameof(TriggerCollider2D)} component to <color=cyan>{collision.name}</color> if it should deactivate on collision.", caller);
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
            Debug.Log($"<color=lime>{nameof(CommonUseCaseFunctions)}</color> <color=cyan>{collision.name}</color> collided with <color=cyan>{caller.name}</color> at <color=cyan>{collision.transform.position}</color>.", caller);
        }

        /// <summary>
        /// Logs collision information to the console. The intended purpose is to be used as a
        /// dynamic function, meaning that the parameters get their data from UnityEvent<Collider,GameObject> automatically.
        /// </summary>
        /// <param name="collision2D">The Collider2D of the object that entered the caller's collider</param>
        /// <param name="caller">The object that is calling this function</param>
        public static void LogCollision(Collider2D collision, GameObject caller)
        {
            Debug.Log($"<color=lime>{nameof(CommonUseCaseFunctions)}</color> <color=cyan>{collision.name}</color> collided with <color=cyan>{caller.name}</color> at <color=cyan>{collision.transform.position}</color>.", caller);
        }

#if UNITY_EDITOR
        public class Initializer : AssetPostprocessor
        {
            static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths, bool didDomainReload)
            {
                if (!AssetDatabase.IsValidFolder("Assets/Resources"))
                {
                    AssetDatabase.CreateFolder("Assets", "Resources");
                    Debug.Log($"<color=lime>{nameof(CommonUseCaseFunctions)}</color> <color=yellow>created</color> the <color=cyan>Assets/Resources</color> folder.");
                }


                //Check that there is a valid location
                if (!AssetDatabase.IsValidFolder("Assets/Resources/Simple Trigger Collider"))
                {
                    AssetDatabase.CreateFolder("Assets/Resources", "Simple Trigger Collider");
                    Debug.Log($"<color=lime>{nameof(CommonUseCaseFunctions)}</color> <color=yellow>created</color> the <color=cyan>Simple Trigger Collider</color> folder at <color=cyan>Assets/Resources/Simple Trigger Collider</color>.");
                }

                //Try to load
                var UseCaseFunctions = AssetDatabase.LoadAssetAtPath<CommonUseCaseFunctions>($"Assets/Resources/Simple Trigger Collider/{nameof(CommonUseCaseFunctions)}.asset");

                //If the settings asset does not exist...
                if (UseCaseFunctions == null)
                {
                    Debug.Log($"<color=lime>{nameof(CommonUseCaseFunctions)}</color> <color=yellow>created</color> a <color=lime>{nameof(CommonUseCaseFunctions)}</color> asset at <color=cyan>Assets/Resources/Simple Trigger Collider/{nameof(CommonUseCaseFunctions)}.asset</color>.");
                    UseCaseFunctions = ScriptableObject.CreateInstance<CommonUseCaseFunctions>();
                    AssetDatabase.CreateAsset(UseCaseFunctions, $"Assets/Resources/Simple Trigger Collider/{nameof(CommonUseCaseFunctions)}.asset");
                }
            }
        }
#endif
    }

}