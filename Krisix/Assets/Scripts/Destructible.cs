using UnityEngine;

public class Destructible : MonoBehaviour
{
    public void DestroyObject()
    {
        // Puedes agregar más lógica aquí, como efectos de destrucción
        Destroy(gameObject); // Destruir el objeto cuando se detona la bomba
    }
}
