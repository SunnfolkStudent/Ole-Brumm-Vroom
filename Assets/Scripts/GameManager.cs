using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private PlayerController _playerController;
    private PlayerInput _playerInput;
    private Spawner _spawner;
    [SerializeField] private TextMeshProUGUI distanceNumberText;

    [Header("Adjust how quickly the distance increases in each phase:")]
    [SerializeField] private float phase1Multiplier = 2;
    [SerializeField] private float phase2Multiplier = 20;
    [SerializeField] private float phase3Multiplier = 175;
    [SerializeField] private float phase4Multiplier = 1500;
    
    public static float CurrentScore;
    public static bool IsPlaying = true;

    public static bool phase1Active { get; private set; }
    public static bool phase2Active { get; private set; }
    public static bool phase3Active { get; private set; }
    public static bool phase4Active { get; private set; }
        
    #region Singleton

    public static GameManager Instance;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    #endregion
    
    private void Start()
    {
        _playerInput = GameObject.FindWithTag("Player").GetComponent<PlayerInput>();
        _playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        _spawner = GameObject.FindWithTag("Spawner").GetComponent<Spawner>();
        distanceNumberText = GetComponentInChildren<TextMeshProUGUI>();
        phase1Active = true;
    }

    private void Update()
    {
        if (IsPlaying)
        {
            Time.timeScale = 1;
            CurrentScore += (ScoreMultiplier() * Time.deltaTime);
            distanceNumberText.text = PrettyScore();
        }
        else
        {
            Time.timeScale = 0;
        }
    }

    #region ---ScoreManager---

        private float ScoreMultiplier()
        {
            if (CurrentScore is >= 0 and < 100)
            {
                Debug.Log("Multiplier:" + phase1Multiplier);
                PhaseChange();
                return phase1Multiplier;
            }
            if (CurrentScore is >= 100 and < 1000)
            {
                PhaseChange();
                Debug.Log("Multiplier:" + phase2Multiplier);
                return phase2Multiplier;
            }
            if (CurrentScore is >= 1000 and < 10000)
            {
                PhaseChange();
                Debug.Log("Multiplier:" + phase3Multiplier);
                return phase3Multiplier;
            }
            if (CurrentScore is >= 10000 and < 100000)
            {
                PhaseChange();
                Debug.Log("Multiplier:" + phase4Multiplier);
                return phase4Multiplier;
            }
            if (CurrentScore >= 100000)
            {
                CurrentScore = 100000;
            }
            Debug.Log("Multiplier: 1");
            return 1;
        }
    
        private string PrettyScore()
        {
            return (Mathf.RoundToInt(CurrentScore) + " m");
        }

        private void PhaseChange()
        {
            switch (CurrentScore)
            {
                case >= 0 and < 100:
                    phase1Active = true;
                    _spawner.PhaseUpdate();
                    break;
                case >= 100 and < 1000:
                    phase2Active = true;
                    phase1Active = false;
                    _spawner.PhaseUpdate();
                    break;
                case >= 1000 and < 10000:
                    phase3Active = true;
                    phase2Active = false;
                    _spawner.PhaseUpdate();
                    break;
                case >= 10000 and < 100000:
                    phase4Active = true;
                    phase3Active = false;
                    _spawner.PhaseUpdate();
                    break;
                case >= 100000:
                    CurrentScore = 100000;
                    LoadVictoryScene();
                    break;
            }
        }

    #endregion

    #region ---SceneController---

    public void LoadRunEndedScene()
        {
            SceneManager.LoadScene("RunEnded", LoadSceneMode.Additive);
            IsPlaying = false;
            _playerInput.ChangeInputToResetRun();
        }
        
        public void LoadNewRun()
        {
            SceneManager.UnloadSceneAsync("RunEnded");
            SceneManager.LoadScene("GameScene");
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
    
        public void UnloadPauseScreen()
        {
            SceneManager.UnloadSceneAsync("PauseScreen");
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
