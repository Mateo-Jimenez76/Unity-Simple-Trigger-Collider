using UnityEngine;

public static class CommonTriggerBehaviors
{
    public static void DestroyOther(GameObject other)
    {
        Object.Destroy(other);
    }

#if HAS_HEALTH_SYSTEM
    public static void DamageOther(Collider2D other, int damage)
    {
        if(other.TryGetComponent<Health>(out Health health))
        {
            health.Damage(damage);
        }
        else
        {
            Debug.LogWarning($"No <color=lime>Health</color> component found on {other.name}. Skipping damage dealing...");
        }
    }
#endif
}
