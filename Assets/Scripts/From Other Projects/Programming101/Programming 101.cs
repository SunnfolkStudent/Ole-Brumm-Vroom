using UnityEngine;
using UnityEngine.InputSystem;

public class Programming101 : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 4f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.aKey.isPressed)
        {
            transform.Translate((Vector3.left) * (moveSpeed*1/2 * Time.deltaTime));
        }

        if (Keyboard.current.wKey.isPressed)
        {
            transform.Translate((Vector3.forward) * (moveSpeed * Time.deltaTime));
        }

        if (Keyboard.current.dKey.isPressed)
        {
            transform.Translate((Vector3.right) * (moveSpeed*1/2 * Time.deltaTime));
        }

        if (Keyboard.current.sKey.isPressed)
        {
            transform.Translate((Vector3.back) * (moveSpeed * Time.deltaTime));
        }
    }
}