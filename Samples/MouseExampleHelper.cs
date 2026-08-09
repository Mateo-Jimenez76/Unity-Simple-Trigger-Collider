using UnityEngine;

namespace SimpleTriggerCollider.Demo 
{
    [AddComponentMenu("")]
    public class MouseExampleHelper : MonoBehaviour
    {
        public void ChangeSpriteColor()
        {
            GetComponent<SpriteRenderer>().color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        }

        public void Grow()
        {
            transform.localScale = Vector3.one * 3;
        }

        public void Shrink()
        {
            transform.localScale = Vector3.one;
        }
    }

}
