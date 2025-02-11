using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            Debug.LogError("No se encontró AudioSource en " + gameObject.name);
            return;
        }

        if (audioSource.clip == null)
        {
            Debug.LogError("No hay AudioClip asignado al AudioSource.");
            return;
        }

        audioSource.loop = true; // Hace que la música se repita
        audioSource.volume = 1.0f; // Asegura que el volumen esté bien
        audioSource.Play();
    }
}
