using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // Velocidad de movimiento
    public GameObject bombPrefab; // Prefab de la bomba
    public Transform bombSpawnPoint; // Punto de generación de la bomba
    public int maxHealth = 3; // Vida máxima del jugador

    private Rigidbody2D rb;
    private Vector2 moveDirection;
    private int currentHealth;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth; // Inicializar la vida del jugador
    }

    void Update()
    {
        // Manejo del movimiento
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        moveDirection = new Vector2(moveX, moveY).normalized;

        // Colocar una bomba al presionar la tecla Espacio
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlaceBomb();
        }
    }

    void FixedUpdate()
    {
        // Aplicar movimiento al Rigidbody2D
        rb.linearVelocity = moveDirection * moveSpeed;
    }

    void PlaceBomb()
    {
        // Generar una bomba si existe un prefab asignado
        if (bombPrefab != null && bombSpawnPoint != null)
        {
            Instantiate(bombPrefab, bombSpawnPoint.position, Quaternion.identity);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Jugador dañado. Vida restante: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Jugador muerto.");
        // Aquí puedes reiniciar el nivel, mostrar una pantalla de Game Over, etc.
        Destroy(gameObject);
    }
}
