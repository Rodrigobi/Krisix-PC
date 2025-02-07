using UnityEngine;

public class FullscreenManager : MonoBehaviour
{
    void Update()
    {
        // Presiona "F" para alternar pantalla completa
        if (Input.GetKeyDown(KeyCode.F))
        {
            Screen.fullScreen = !Screen.fullScreen; // Cambiar modo
        }
    }
}
