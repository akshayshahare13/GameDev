using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    bool hasGameEnded = false;
    float gameRestartDelay = 1f;
    public void EndGame()
    {
        if (!hasGameEnded)
        {
            hasGameEnded = true;
            Debug.Log("End game");
            gameRestart();
            Invoke("gameRestart", gameRestartDelay);
        }
        
    }

    void gameRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Debug.Log("Game restarted");
    }
}
