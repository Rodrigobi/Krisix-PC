using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float destroyDelay = 0.5f;

    void Start()
    {
        Destroy(gameObject, destroyDelay);
    }
}
