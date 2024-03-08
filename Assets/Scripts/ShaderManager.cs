using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class ShaderManager : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    public AnimationCurve animationCurve;
    private static readonly int HoneyAmount = Shader.PropertyToID("_HoneyAmount");
    private static readonly int ColorStrength = Shader.PropertyToID("ColorStrength");
    
    [SerializeField] private float honeyAmountPhase1 = 0f;
    [SerializeField] private float honeyAmountPhase2 = 1f;
    [SerializeField] private float honeyAmountPhase3 = 2f;
    [SerializeField] private float honeyAmountPhase4 = 3f;
    private int _currentPhase;

    [SerializeField] private float colourStrengthPhase1 = 0.25f;
    [SerializeField] private float colourStrengthPhase2 = 0.5f;
    [SerializeField] private float colourStrengthPhase3 = 0.75f;
    [SerializeField] private float colourStrengthPhase4 = 1f;
    
    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();

        _spriteRenderer.material = new Material(_spriteRenderer.material);
        PhaseChange();
    }
    
    // TODO: Create function that regularly updates and clamps the values until the next phase. : )
    // Maybe Lerp?

    private void Update()
    {
        var currentHoneyAmount = Mathf.Lerp(CurrentHoneyPhase(_currentPhase), CurrentHoneyPhase(_currentPhase+1), TimeUntilNextPhase(_currentPhase));
        var currentColorStrength = Mathf.Lerp(CurrentColorStrength(_currentPhase), CurrentColorStrength(_currentPhase+1), TimeUntilNextPhase(_currentPhase));
        _spriteRenderer.material.SetFloat(HoneyAmount, currentHoneyAmount);
        _spriteRenderer.material.SetFloat(ColorStrength, currentColorStrength);
    }

    private float CurrentHoneyPhase(int phase)
    {
        switch (phase)
        {
            case 1:
                return honeyAmountPhase1;
            case 2:
                return honeyAmountPhase2;
            case 3:
                return honeyAmountPhase3;
            case 4:
                return honeyAmountPhase4;
            case 5:
                return honeyAmountPhase4;
        }
        return 0;
    }
    
    private float CurrentColorStrength(int phase)
    {
        switch (phase)
        {
            case 1:
                return colourStrengthPhase1;
            case 2:
                return colourStrengthPhase2;
            case 3:
                return colourStrengthPhase3;
            case 4:
                return colourStrengthPhase4;
            case 5:
                return colourStrengthPhase4;
        }
        return 0;
    }

    private float TimeUntilNextPhase(int phase)
    {
        switch (phase)
        {
            case 1:
                return 20f;
            case 2:
                return 36f;
            case 3:
                return 36f;
            case 4:
                return 40f;
            case 5:
                return 0;
        }
        return 0;
    }
    

    public void PhaseChange()
    {
        switch (GameManager.currentPhase)
        {
            case 1:
                _spriteRenderer.material.SetFloat(HoneyAmount, honeyAmountPhase1);
                _spriteRenderer.material.SetFloat(ColorStrength, colourStrengthPhase1);
                _currentPhase = 1;
                break;
            case 2:
                _spriteRenderer.material.SetFloat(HoneyAmount, honeyAmountPhase2);
                _spriteRenderer.material.SetFloat(ColorStrength, colourStrengthPhase2);
                _currentPhase = 2;
                break;
            case 3:
                _spriteRenderer.material.SetFloat(HoneyAmount, honeyAmountPhase3);
                _spriteRenderer.material.SetFloat(ColorStrength, colourStrengthPhase3);
                _currentPhase = 3;
                break;
            case 4:
                _spriteRenderer.material.SetFloat(HoneyAmount, honeyAmountPhase4);
                _spriteRenderer.material.SetFloat(ColorStrength, colourStrengthPhase4);
                _currentPhase = 4;
                break;
        }
    }
}
