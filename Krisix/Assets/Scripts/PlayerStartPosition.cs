using UnityEngine;

public class PlayerStartPosition : MonoBehaviour
{
    public Transform startPoint; // Referencia al punto de inicio

    void Start()
    {
        // Si se asigna un punto de inicio, ajusta la posición del jugador
        if (startPoint != null)
        {
            transform.position = startPoint.position;
        }
        else
        {
            Debug.LogWarning("No se ha asignado un punto de inicio para el jugador.");
        }
    }
}
