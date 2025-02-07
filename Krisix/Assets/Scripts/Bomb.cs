using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float explosionDelay = 2f; // Tiempo antes de la explosión
    public float explosionRadius = 2f; // Radio máximo de la explosión
    public int damage = 1; // Daño que causa la explosión
    public LayerMask wallLayer; // Capa que representa los muros
    public LayerMask damageableLayer; // Capa de objetos que pueden recibir daño

    void Start()
    {
        // Iniciar la explosión después de un tiempo
        Invoke("Explode", explosionDelay);
    }

    void Explode()
    {
        // Explosión en las cuatro direcciones (arriba, abajo, izquierda, derecha)
        ExplodeInDirection(Vector2.up);
        ExplodeInDirection(Vector2.down);
        ExplodeInDirection(Vector2.left);
        ExplodeInDirection(Vector2.right);

        // Destruir la bomba tras la explosión
        Destroy(gameObject);
    }

    void ExplodeInDirection(Vector2 direction)
    {
        // Posición inicial de la explosión
        Vector2 startPos = transform.position;

        // Recorrer la explosión en segmentos, hasta alcanzar el radio máximo
        for (float distance = 0; distance <= explosionRadius; distance += 1f)
        {
            Vector2 currentPos = startPos + direction * distance;

            // Verificar si la explosión choca con un muro
            RaycastHit2D wallHit = Physics2D.Raycast(startPos, direction, distance, wallLayer);
            if (wallHit.collider != null)
            {
                // Si hay un muro, detener la explosión en esa dirección
                break;
            }

            // Verificar si hay objetos dañables en esta posición
            Collider2D[] hitObjects = Physics2D.OverlapCircleAll(currentPos, 0.2f, damageableLayer);
            foreach (var obj in hitObjects)
            {
                PlayerController player = obj.GetComponent<PlayerController>();
                if (player != null)
                {
                    player.TakeDamage(damage); // Aplicar daño al jugador
                }

                // Si el objeto es destructible, destruirlo
                Destructible destructible = obj.GetComponent<Destructible>();
                if (destructible != null)
                {
                    destructible.DestroyObject();
                }
            }

            // Opcional: Crear un efecto visual de explosión en esta posición
            CreateExplosionEffect(currentPos);
        }
    }

    void CreateExplosionEffect(Vector2 position)
    {
        // Aquí puedes instanciar un prefab de explosión, si tienes uno
        Debug.Log($"Explosión en: {position}");
    }

    void OnDrawGizmosSelected()
    {
        // Mostrar el radio máximo de explosión en la vista del editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}

