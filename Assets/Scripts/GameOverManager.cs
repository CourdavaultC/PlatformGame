using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverUI;

    public static GameOverManager instance;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance GameOverManager dans la scène");
            return;
        }

        instance = this;
    }

    public void OnPlayerDeath()
    {       
        gameOverUI.SetActive(true);
    }

    public void RetryButton()
    {
        Inventory.instance.RemoveCoins(CurrentSceneManager.instance.coinsPickedUpInThisSceneCount);
        // Recommencer le niveau
        // Recharger la scène
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        // Replacer le joueur
        PlayerHealth.instance.Respawn();
        // Réactiver mouv + vie du joueur
        gameOverUI.SetActive(false);
    }

    public void MainMenuButton()
    {
        // Retour au menu principal
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
