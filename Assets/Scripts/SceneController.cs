using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneController
{
    public static void LoadRunEndedScene()
    {
        SceneManager.LoadScene("RunEnded", LoadSceneMode.Additive);
        ScoreManager.IsPlaying = false;
    }
    
    public static void LoadNewRun()
    {
        SceneManager.UnloadSceneAsync("RunEnded");
        SceneManager.LoadScene("GameScene");
        ScoreManager.CurrentScore = 0;
        ScoreManager.IsPlaying = true;
    }

    public static void LoadPauseScreen()
    {
        Time.timeScale = 0;
        ScoreManager.IsPlaying = false;
        SceneManager.LoadScene("PauseScreen", LoadSceneMode.Additive);
    }

    public static void UnloadPauseScreen()
    {
        SceneManager.UnloadSceneAsync("PauseScreen");
        Time.timeScale = 1;
        ScoreManager.IsPlaying = true;
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