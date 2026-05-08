using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    private bool playGameLaunched = false;
    private bool exitGameLaunched = false;
    
    public void PlayGame()
    {
        if (playGameLaunched) return;
        playGameLaunched = true;

        Debug.Log("Play Game");
        
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        if (exitGameLaunched) return;
        exitGameLaunched = true;
        
        Debug.Log("Exit Game");
        
        Application.Quit();
    }
}
