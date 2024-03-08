using UnityEngine;

public class ShaderManager : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private static readonly int HoneyAmount = Shader.PropertyToID("_HoneyAmount");
    private static readonly int ColorStrength = Shader.PropertyToID("ColorStrength");
    
    [SerializeField] private float honeyAmountPhase1 = 0f;
    [SerializeField] private float honeyAmountPhase2 = 0.5f;
    [SerializeField] private float honeyAmountPhase3 = 1.5f;
    [SerializeField] private float honeyAmountPhase4 = 3f;
    private float currentIncrementHoney;
    private float currentIncrementColor;
    private int _currentPhase;
    private float _currentHoneyAmount;
    private float _nextHoneyAmount;
    private float _currentColorStrength;
    private float _nextColorStrength;
    private float _timeUntilNextPhase;

    // private float _startTime;

    [SerializeField] private float colourStrengthPhase1 = 0f;
    [SerializeField] private float colourStrengthPhase2 = 0.1f;
    [SerializeField] private float colourStrengthPhase3 = 0.6f;
    [SerializeField] private float colourStrengthPhase4 = 1f;
    
    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();

        _spriteRenderer.material = new Material(_spriteRenderer.material);
        PhaseChange();

        // _startTime = Time.time;
    }

    private void Update()
    {
        _currentHoneyAmount += currentIncrementHoney;
        _currentColorStrength += currentIncrementColor;
        // _currentHoneyAmount += Mathf.Lerp(_currentHoneyAmount, _nextHoneyAmount, Time.time/_timeUntilNextPhase);
        // _currentColorStrength += Mathf.Lerp(_currentColorStrength, _nextColorStrength, Time.time/_timeUntilNextPhase);
        _spriteRenderer.material.SetFloat(HoneyAmount, _currentHoneyAmount);
        _spriteRenderer.material.SetFloat(ColorStrength, _currentColorStrength);
    }
    
    private void PhaseUpdateShaderVariables(int phase)
    {
        switch (phase)
        {
            case 1:
                currentIncrementHoney = 0.00008f;
                currentIncrementColor = 0.00008f;
                // _currentHoneyAmount = honeyAmountPhase1;
                // _nextHoneyAmount = honeyAmountPhase2;
                // _currentColorStrength = colourStrengthPhase1;
                _nextColorStrength = colourStrengthPhase2;
                _timeUntilNextPhase = 20f-1;
                break;
            case 2:
                currentIncrementHoney = 0.00015f;
                currentIncrementColor = 0.00015f;
                // _currentHoneyAmount = honeyAmountPhase2;
                // _nextHoneyAmount = honeyAmountPhase3;
                // _currentColorStrength = colourStrengthPhase2;
                _nextColorStrength = colourStrengthPhase3;
                _timeUntilNextPhase = 20f+36f-1;
                break;
            case 3:
                currentIncrementHoney = 0.00030f;
                currentIncrementColor = 0.00030f;
                //_currentHoneyAmount = honeyAmountPhase3;
                // _nextHoneyAmount = honeyAmountPhase4;
                // _currentColorStrength = colourStrengthPhase3;
                _nextColorStrength = colourStrengthPhase4;
                _timeUntilNextPhase = 20f+36f+36f-1;
                break;
            case 4:
                currentIncrementHoney = 0.00040f;
                currentIncrementColor = 0.00040f;
                //_currentHoneyAmount = honeyAmountPhase4;
                // _nextHoneyAmount = honeyAmountPhase4;
                // _currentColorStrength = colourStrengthPhase4;
                _nextColorStrength = colourStrengthPhase4;
                _timeUntilNextPhase = 20f+36f+36f+40f-1;
                break;
            case 5:
                //_currentHoneyAmount = honeyAmountPhase1;
                //_nextHoneyAmount = honeyAmountPhase2;
                // _currentColorStrength = colourStrengthPhase1;
                // _nextColorStrength = colourStrengthPhase2;
                _timeUntilNextPhase = 1;
                break;
        }
    }
    

    public void PhaseChange()
    {
        switch (GameManager.currentPhase)
        {
            case 1:
                _currentPhase = 1;
                PhaseUpdateShaderVariables(_currentPhase);
                break;
            case 2:
                _currentPhase = 2;
                PhaseUpdateShaderVariables(_currentPhase);
                break;
            case 3:
                _currentPhase = 3;
                PhaseUpdateShaderVariables(_currentPhase);
                break;
            case 4:
                _currentPhase = 4;
                PhaseUpdateShaderVariables(_currentPhase);
                break;
        }
    }
}
