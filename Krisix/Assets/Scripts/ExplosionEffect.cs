using UnityEngine;

public class ExplosionEffect : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verificar si el objeto es destructible
        Destructible destructible = other.GetComponent<Destructible>();
        if (destructible != null)
        {
            destructible.DestroyObject(); // Destruir el objeto
        }
    }
}
