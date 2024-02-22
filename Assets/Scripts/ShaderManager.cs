using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderManager : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private static readonly int HoneyAmount = Shader.PropertyToID("_HoneyAmount");

    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();

        _spriteRenderer.material = new Material(_spriteRenderer.material);

    }

    // Update is called once per frame
    void Update()
    {
        _spriteRenderer.material.SetFloat(HoneyAmount, 1);
    }
}
