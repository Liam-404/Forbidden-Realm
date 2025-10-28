using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneComplete : MonoBehaviour
{
    // You can call this method directly from your cutscene's end event.
    public void LoadLevel1()
    {
        // Option 1: Load by scene name (recommended for readability)
        SceneManager.LoadScene("Level1");
    }

    /*
    // Option 2: Load by build index (good if you're sure of the order)
    public void LoadLevel1ByIndex()
    {
        // Replace '1' with the actual index of your "Level1" scene in the build settings
        SceneManager.LoadScene(1); 
    }
    */
}