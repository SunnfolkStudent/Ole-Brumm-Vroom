using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderManager : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private static readonly int HoneyAmount = Shader.PropertyToID("_HoneyAmount");

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        spriteRenderer.material = new Material(spriteRenderer.material);

    }

    // Update is called once per frame
    void Update()
    {
        spriteRenderer.material.SetFloat(HoneyAmount, 1);
    }
}
