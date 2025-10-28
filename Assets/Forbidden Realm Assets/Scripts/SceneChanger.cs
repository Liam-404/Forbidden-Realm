using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void LoadStartScene() // This is the function the button will call
    {
        SceneManager.LoadScene(0); // Loads the scene at index 0 (your starting level)
    }
}