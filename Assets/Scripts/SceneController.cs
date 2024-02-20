using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneController
{
    public static void LoadRunEndedScene()
    {
        SceneManager.LoadScene("RunEnded", LoadSceneMode.Additive);
    }
    
    public static void LoadNewRun()
    {
        SceneManager.UnloadSceneAsync("RunEnded");
        SceneManager.LoadScene("GameScene");
    }

    public static void LoadPauseScreen()
    {
        Time.timeScale = 0;
        SceneManager.LoadScene("PauseScreen", LoadSceneMode.Additive);
    }

    public static void UnloadPauseScreen()
    {
        SceneManager.UnloadSceneAsync("PauseScreen");
        Time.timeScale = 1;
    }

    public static void LoadVictoryScene()
    {
        SceneManager.LoadScene("Victory", LoadSceneMode.Single);
    }

    public static void QuitGame()
    {
        Debug.Log("Quitting the Game");
        Application.Quit();
    }
    
}