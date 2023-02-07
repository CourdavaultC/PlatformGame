using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;

    public GameObject pauseMenuUI;
    public GameObject settingsWindow;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Paused();
            }
        }
    }

    void Paused()
    {
        // Arreter le PlayerMovement
        PlayerMovement.instance.enabled = false;
        // Afficher menu Pause
        pauseMenuUI.SetActive(true);
        // Arreter le temps
        Time.timeScale = 0;
        // Changer le statut du jeu
        gameIsPaused = true;
    }

    public void Resume()
    {
        // Reprendre le PlayerMovement
        PlayerMovement.instance.enabled = true;
        // Revenir au jeu
        pauseMenuUI.SetActive(false);
        // Reprendre le court du temps
        Time.timeScale = 1;
        // Changer le statut du jeu
        gameIsPaused = false;
    }

    public void LoadMainMenu()
    {
        Resume();
        SceneManager.LoadScene("MainMenu");
    }

    public void OpenSettingsWindow()
    {
        settingsWindow.SetActive(true);
    }

    public void CloseSettingsWindow()
    {
        settingsWindow.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
