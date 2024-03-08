using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameEvent gameEvent;
    private PlayerController _playerController;
    private PlayerInput _playerInput;
    [SerializeField] private TextMeshProUGUI distanceNumberText;

    [Header("Adjust how quickly the scoreDistance increases in each phase:")]
    [SerializeField] private float phase1Multiplier = 4;
    [SerializeField] private float phase2Multiplier = 20;
    [SerializeField] private float phase3Multiplier = 175;
    [SerializeField] private float phase4Multiplier = 1500;
    
    public static float CurrentScore;
    public static bool IsPlaying = true;

    public static int currentPhase { get; private set; }
    public static bool phase1Active { get; private set; }
    public static bool phase2Active { get; private set; }
    public static bool phase3Active { get; private set; }
    public static bool phase4Active { get; private set; }
    
    private void Start()
    {
        currentPhase = 1;
        _playerInput = GameObject.FindWithTag("Player").GetComponent<PlayerInput>();
        _playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        distanceNumberText = GetComponentInChildren<TextMeshProUGUI>();
        phase1Active = true;

        StartCoroutine(CheckScore());

        // Debug.Log("Phase 1 lasts for: " + 100/phase1Multiplier + " seconds.");
        // Debug.Log("Phase 2 lasts for: " + 900/phase2Multiplier + " seconds.");
        // Debug.Log("Phase 3 lasts for: " + 9000/phase3Multiplier + " seconds.");
        // Debug.Log("Phase 4 lasts for: " + 90000/phase4Multiplier + " seconds.");
    }

    private void Update()
    {
        if (IsPlaying)
        {
            Time.timeScale = 1;
            CurrentScore += (PhaseChange() * Time.deltaTime);
            distanceNumberText.text = PrettyScore();
        }
        else
        {
            Time.timeScale = 0;
        }
    }

    private IEnumerator CheckScore()
    {
        gameEvent.TriggerNewPhaseEvent();
        yield return new WaitUntil(() => CurrentScore > 99);
        gameEvent.TriggerNewPhaseEvent();
        yield return new WaitUntil(() => CurrentScore > 999);
        gameEvent.TriggerNewPhaseEvent();
        yield return new WaitUntil(() => CurrentScore > 9999);
        gameEvent.TriggerNewPhaseEvent();
        // yield return new WaitUntil(() => CurrentScore > 99999);
    }

    #region ---ScoreManager---

        private float PhaseChange()
        {
            if (CurrentScore is >= 0 and < 100)
            {
                currentPhase = 1;
                // gameEvent.TriggerNewPhaseEvent();
                // Debug.Log("Multiplier:" + phase1Multiplier);
                // Debug.Log("Phase 1 lasts for: " + 100/phase1Multiplier + " seconds.");
                return phase1Multiplier;
            }
            
            if (CurrentScore is >= 100 and < 1000)
            {
                currentPhase = 2;
                // gameEvent.TriggerNewPhaseEvent();
                // Debug.Log("Multiplier:" + phase2Multiplier);
                // Debug.Log("Phase 2 lasts for: " + 900/phase2Multiplier + " seconds.");
                return phase2Multiplier;
            }
            if (CurrentScore is >= 1000 and < 10000)
            {
                currentPhase = 3;
                // gameEvent.TriggerNewPhaseEvent();
                // Debug.Log("Multiplier:" + phase3Multiplier);
                // Debug.Log("Phase 3 lasts for: " + 9000/phase3Multiplier + " seconds.");
                return phase3Multiplier;
            }
            if (CurrentScore is >= 10000 and < 100000)
            {
                currentPhase = 4;
                // gameEvent.TriggerNewPhaseEvent();
                // Debug.Log("Multiplier:" + phase4Multiplier);
                // Debug.Log("Phase 4 lasts for: " + 90000/phase4Multiplier + " seconds.");
                return phase4Multiplier;
            }
            if (CurrentScore >= 100000)
            {
                CurrentScore = Mathf.Clamp(CurrentScore, 100000, 100000);
                LoadVictoryScene();
                return 0;
            }
            return 1;
        }
    
        private string PrettyScore()
        {
            return (Mathf.RoundToInt(CurrentScore) + " m");
        }

    #endregion

    #region ---SceneController---

        public void LoadRunEndedScene()
        {
            SceneManager.LoadScene("RunEnded", LoadSceneMode.Additive);
            IsPlaying = false;
            _playerInput.ChangeInputToResetRun();
        }
        
        public IEnumerator LoadNewRun()
        {
            AsyncOperation asyncOperation = SceneManager.UnloadSceneAsync("RunEnded");
            yield return new WaitUntil(() => asyncOperation.isDone);
            SceneManager.LoadScene("GameSceneDum");
            var scene = SceneManager.GetSceneByName("GameSceneDum");
            SceneManager.SetActiveScene(scene);
            PlayerController.PlayerHasCrashed = false;
            CurrentScore = 0;
            IsPlaying = true;
            _playerInput.ChangeToPlayer();
        }
    
        public void LoadPauseScreen()
        {
            IsPlaying = false;
            SceneManager.LoadScene("PauseScreen", LoadSceneMode.Additive);
            _playerInput.ChangeToPauseScreen();
        }
    
        public IEnumerator UnloadPauseScreen()
        {
            AsyncOperation asyncOperation = SceneManager.UnloadSceneAsync("PauseScreen");
            yield return new WaitUntil(() => asyncOperation.isDone);
            IsPlaying = true;
        }

        private void LoadVictoryScene()
        {
            IsPlaying = false;
            SceneManager.LoadScene("Victory", LoadSceneMode.Single);
        }
    
        public void QuitGame()
        {
            Debug.Log("Quitting the Game");
            Application.Quit();
        }

    #endregion
}
