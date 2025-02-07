using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 2f; // Velocidad de movimiento del enemigo
    public int maxHealth = 5; // Vida máxima del enemigo
    public int damageToPlayer = 1; // Daño que hace al jugador
    public Transform[] waypoints; // Puntos de patrullaje
    private int currentWaypointIndex = 0; // Índice del waypoint actual
    private bool movingForward = true; // Dirección del patrullaje (true = hacia adelante, false = hacia atrás)

    private int currentHealth; // Vida actual del enemigo
    private Rigidbody2D rb;
    private Animator animator; // Referencia al componente Animator
    private Vector3 originalScale; // Escala original del enemigo

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); // Obtener el componente Animator
        originalScale = transform.localScale; // Guardar la escala original del enemigo
        currentHealth = maxHealth; // Inicializar la vida del enemigo

        if (waypoints.Length > 0)
        {
            // Inicializar la posición objetivo en el primer waypoint
            transform.position = waypoints[currentWaypointIndex].position;
        }
    }

    void Update()
    {
        if (waypoints.Length > 1)
        {
            Patrol();
        }
    }

    void Patrol()
    {
        // Obtener la posición objetivo
        Transform targetWaypoint = waypoints[currentWaypointIndex];
        Vector2 direction = (targetWaypoint.position - transform.position).normalized;

        // Mover al enemigo hacia el waypoint
        rb.linearVelocity = direction * moveSpeed;

        // Activar el parámetro de animación si el enemigo se está moviendo
        bool isWalking = rb.linearVelocity.magnitude > 0.1f; // Si hay movimiento significativo
        animator.SetBool("estaCaminando", isWalking);

        // Verificar si el enemigo llegó al waypoint actual
        if (Vector2.Distance(transform.position, targetWaypoint.position) < 0.2f)
        {
            // Cambiar waypoint según la dirección
            if (movingForward)
            {
                currentWaypointIndex++;
                if (currentWaypointIndex >= waypoints.Length)
                {
                    currentWaypointIndex = waypoints.Length - 2; // Cambiar dirección
                    movingForward = false;
                }
            }
            else
            {
                currentWaypointIndex--;
                if (currentWaypointIndex < 0)
                {
                    currentWaypointIndex = 1; // Cambiar dirección
                    movingForward = true;
                }
            }
        }

        // Controlar el giro del enemigo en el eje X
        FlipSprite(direction.x);
    }

    void FlipSprite(float directionX)
    {
        // Si se mueve a la derecha
        if (directionX > 0)
        {
            transform.localScale = new Vector3(originalScale.x, originalScale.y, originalScale.z);
        }
        // Si se mueve a la izquierda
        else if (directionX < 0)
        {
            transform.localScale = new Vector3(-originalScale.x, originalScale.y, originalScale.z);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Verificar si el objeto que tocó es el jugador
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        if (player != null)
        {
            player.TakeDamage(damageToPlayer); // Aplicar daño al jugador
        }
    }

    // Método para recibir daño
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Enemigo dañado. Vida restante: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Enemigo eliminado.");
        // Detener animación de movimiento
        animator.SetBool("estaCaminando", false);
        Destroy(gameObject);
    }
}
