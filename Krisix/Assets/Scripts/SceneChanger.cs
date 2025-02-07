using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    // Nombre de la escena a la que quieres cambiar
    public string Stage1;

    public void ChangeScene()
    {
        SceneManager.LoadScene(Stage1);
    }
}
