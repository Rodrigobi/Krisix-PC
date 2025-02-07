using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // El personaje a seguir
    public float smoothSpeed = 0.125f; // Velocidad de suavizado
    public Vector3 offset; // Desplazamiento de la cámara respecto al personaje
    public Transform minBound; // Objeto vacío que define el límite mínimo
    public Transform maxBound; // Objeto vacío que define el límite máximo

    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            
            // Obtener los valores de los límites desde los objetos vacíos
            float clampedX = Mathf.Clamp(smoothedPosition.x, minBound.position.x, maxBound.position.x);
            float clampedY = Mathf.Clamp(smoothedPosition.y, minBound.position.y, maxBound.position.y);
            
            transform.position = new Vector3(clampedX, clampedY, transform.position.z);
        }
    }
}
