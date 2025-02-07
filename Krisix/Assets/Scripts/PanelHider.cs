using UnityEngine;

public class PanelHider : MonoBehaviour
{
    public GameObject panel; // Arrastra el Panel desde el Inspector

    public void HidePanel()
    {
        panel.SetActive(false); // Desactiva el panel
    }
}
