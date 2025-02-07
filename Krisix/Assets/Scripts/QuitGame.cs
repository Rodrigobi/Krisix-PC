using UnityEngine;

public class QuitGame : MonoBehaviour
{
    public void ExitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false; // Para detener el juego en el editor
        #else
            Application.Quit(); // Cierra la aplicación en la versión compilada
        #endif
    }
}
