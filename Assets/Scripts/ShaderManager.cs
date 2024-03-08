using UnityEngine;

public class ShaderManager : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private static readonly int HoneyAmount = Shader.PropertyToID("_HoneyAmount");
    private static readonly int ColorStrength = Shader.PropertyToID("ColorStrength");
    
    [SerializeField] private float amountPhase1 = 0f;
    [SerializeField] private float amountPhase2 = 1f;
    [SerializeField] private float amountPhase3 = 2f;
    [SerializeField] private float amountPhase4 = 3f;

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

    public void PhaseChange()
    {
        switch (GameManager.currentPhase)
        {
            case 1:
                _spriteRenderer.material.SetFloat(HoneyAmount, amountPhase1);
                _spriteRenderer.material.SetFloat(ColorStrength, colourStrengthPhase1);
                break;
            case 2:
                _spriteRenderer.material.SetFloat(HoneyAmount, amountPhase2);
                _spriteRenderer.material.SetFloat(ColorStrength, colourStrengthPhase2);
                break;
            case 3:
                _spriteRenderer.material.SetFloat(HoneyAmount, amountPhase3);
                _spriteRenderer.material.SetFloat(ColorStrength, colourStrengthPhase3);
                break;
            case 4:
                _spriteRenderer.material.SetFloat(HoneyAmount, amountPhase4);
                _spriteRenderer.material.SetFloat(ColorStrength, colourStrengthPhase4);
                break;
        }
    }
}
