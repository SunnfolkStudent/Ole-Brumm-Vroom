using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI distanceNumberText;
    
    public static float CurrentScore;
    public static bool IsPlaying = true;
        
    #region Singleton

    public static ScoreManager Instance;

    private void Start()
    {
        distanceNumberText = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    #endregion
    
    private void Update()
    {
        if (IsPlaying)
        {
            CurrentScore += (ScoreMultiplier() * Time.deltaTime);
            distanceNumberText.text = PrettyScore();
        }
    }

    private float ScoreMultiplier()
    {
        if (CurrentScore is >= 0 and < 100)
        {
            Debug.Log("Multiplier = 2.");
            return 2;
        }
        else if (CurrentScore is >= 100 and < 1000)
        {
            Debug.Log("Multiplier = 20.");
            return 20;
        }
        else if (CurrentScore is >= 1000 and < 10000)
        {
            Debug.Log("Multiplier = 175.");
            return 175;
        }
        else if (CurrentScore is >= 10000 and < 100000)
        {
            Debug.Log("Multiplier = 1500.");
            return 1500;
        }
        else if (CurrentScore >= 100000)
        {
            IsPlaying = false;
            SceneController.LoadVictoryScene();
        }
        Debug.Log("Multiplier = 1.");
        return 1;
    }

    private string PrettyScore()
    {
        //return Mathf.RoundToInt(CurrentScore).ToString();
        return (Mathf.RoundToInt(CurrentScore) + " m");
    }


}
