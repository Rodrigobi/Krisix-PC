using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // Velocidad de movimiento
    public GameObject bombPrefab; // Prefab de la bomba
    public Transform bombSpawnPoint; // Punto de generación de la bomba
    public int maxHealth = 3; // Vida máxima del jugador
    public Tilemap tilemap; // Referencia al Tilemap


    private Rigidbody2D rb;
    private Vector2 moveDirection;
    private int currentHealth;
    private Animator animator; // Referencia al Animator

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); // Obtener el Animator
        currentHealth = maxHealth;
    }

    void Update()
    {
        // Manejo del movimiento
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        moveDirection = new Vector2(moveX, moveY).normalized;

        // Actualizar animaciones
        UpdateAnimations(moveX, moveY);

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
    // Verificar si existe un prefab asignado
    if (bombPrefab != null && tilemap != null)
    {
        // Obtener la posición actual del jugador en coordenadas de celda
        Vector3Int cellPosition = tilemap.WorldToCell(transform.position);

        // Convertir la posición de celda nuevamente a coordenadas de mundo (el centro del tile)
        Vector3 bombPosition = tilemap.GetCellCenterWorld(cellPosition);

        // Instanciar la bomba en el centro del tile
        Instantiate(bombPrefab, bombPosition, Quaternion.identity);
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
        Destroy(gameObject);
    }

    void UpdateAnimations(float moveX, float moveY)
    {
        // Resetear todos los parámetros
        animator.SetBool("movD", false);
        animator.SetBool("movIz", false);
        animator.SetBool("movA", false);
        animator.SetBool("movArr", false);
        animator.SetBool("movNe", false);

        // Actualizar según la dirección del movimiento
        if (moveX > 0) // Moviendo a la derecha
        {
            animator.SetBool("movD", true);
        }
        else if (moveX < 0) // Moviendo a la izquierda
        {
            animator.SetBool("movIz", true);
        }
        else if (moveY > 0) // Moviendo hacia arriba
        {
            animator.SetBool("movArr", true);
        }
        else if (moveY < 0) // Moviendo hacia abajo
        {
            animator.SetBool("movA", true);
        }
        else // Sin movimiento
        {
            animator.SetBool("movNe", true);
        }
    }
}
