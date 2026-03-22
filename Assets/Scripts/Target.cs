using UnityEngine;

public class Target : MonoBehaviour
{
    [Header("Destroy Settings")]
    public float destroyDelay = 0.1f;

    public void GetHit()
    {
        Destroy(gameObject, destroyDelay);
    }
}