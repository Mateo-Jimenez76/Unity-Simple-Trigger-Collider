using UnityEngine;
using UnityEngine.SceneManagement;
public class CommonTriggerBehaviors : ScriptableObject
{
    /// <summary>
    /// Destroys the object corresponding to the collider passed in. The intended purpose is to be used as a
    /// dynamic function, meaning that the parameters get their data from UnityEvent<Collider,GameObject> automatically.
    /// </summary>
    /// <param name="collision">The Collider2D of the object to destroy</param>
    /// <param name="caller">The object that is calling this function</param>
    public static void DestroyObjectCollidedWith(Collider2D collision, GameObject caller)
    {
        Debug.Log("Destroying " + collision.name + " because it collided with " + caller.name);
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
        Debug.Log("Destroying " + collision.name + " because it collided with " + caller.name);
        Destroy(collision.gameObject);
    }

    /// <summary>
    /// Loads a scene asynchronously by name.
    /// </summary>
    /// <param name="sceneName">The name of the scene as listed in Build Settings</param>
    public static void LoadSceneAsync(string sceneName)
    {
        Debug.Log("Loading scene " + sceneName + " asynchronously.");
        SceneManager.LoadSceneAsync(sceneName);
    }

    /// <summary>
    /// Logs collision information to the console. The intended purpose is to be used as a
    /// dynamic function, meaning that the parameters get their data from UnityEvent<Collider,GameObject> automatically.
    /// </summary>
    /// <param name="collision">The Collider of the object that entered the caller's collider</param>
    /// <param name="caller">The object that is calling this function</param>
    public static void LogCollision(Collider collision, GameObject caller)
    {
        Debug.Log($"{collision.name} collided with {caller.name}(caller) at {collision.transform.position}.");
    }

    /// <summary>
    /// Logs collision information to the console. The intended purpose is to be used as a
    /// dynamic function, meaning that the parameters get their data from UnityEvent<Collider,GameObject> automatically.
    /// </summary>
    /// <param name="collision2D">The Collider2D of the object that entered the caller's collider</param>
    /// <param name="caller">The object that is calling this function</param>
    public static void LogCollision(Collider2D collision, GameObject caller)
    {
        Debug.Log($"{collision.name} collided with {caller.name}(caller) at {collision.transform.position}.");
    }


#if HAS_HEALTH_SYSTEM

    public static void DamageObjectCollidedWith(Collider2D other, int damage, GameObject caller)
    {
        if (other.TryGetComponent<Health>(out Health health))
        {
            Debug.Log($"Dealing {damage} damage to {other.name} because they collided with {caller.name}.");
            health.Damage(damage);
        }
        else
        {
            Debug.LogWarning($"No Health component found on {other.name} which collided with {caller.name}. Skipping damage dealing...");
        }
    }
#endif
}
