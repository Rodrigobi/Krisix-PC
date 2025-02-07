using UnityEngine;

public class PanelController : MonoBehaviour
{
    public GameObject panel; // Arrastra el panel desde el Inspector

    public void TogglePanel()
    {
        panel.SetActive(!panel.activeSelf); // Activa o desactiva el panel
    }
}
