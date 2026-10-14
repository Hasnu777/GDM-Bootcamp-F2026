using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Restart : MonoBehaviour 
{

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // loads current scene
        print("The game has been restarted.");
    }

}
