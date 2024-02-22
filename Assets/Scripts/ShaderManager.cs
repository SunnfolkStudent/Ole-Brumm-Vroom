using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderManager : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        spriteRenderer.material = new Material(spriteRenderer.material);

    }

    // Update is called once per frame
    void Update()
    {
        spriteRenderer.material.SetFloat("_HoneyAmount", 1);
    }
}
