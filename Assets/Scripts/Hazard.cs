using UnityEngine;
using UnityEngine.SceneManagement;

public class Hazard : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the hazard is the Player
        if (other.CompareTag("Player"))
        {
            // Simple restart: reload the current active scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}