using System.Collections;
using UnityEngine;

public class PlayerOneWayPlatform : MonoBehaviour
{
    /*private GameObject currentOneWayPlatform;

    [SerializeField] private BoxCollider2D playerBoxCollider;
    [SerializeField] private CapsuleCollider2D playerHeadCollider;

    private void Start()
    {
        playerHeadCollider = GameObject.FindWithTag("Player").GetComponent<CapsuleCollider2D>();
        playerBoxCollider = GameObject.FindWithTag("Player").GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (PlayerInput.DropBelow)
        {
            Debug.Log("Player attempts to drop down.");
            if (currentOneWayPlatform != null)
            {
                Debug.Log("Player is dropping down.");
                StartCoroutine(DisableCollision());
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            currentOneWayPlatform = collision.gameObject;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            currentOneWayPlatform = null;
        }
    }

    private IEnumerator DisableCollision()
    {
        BoxCollider2D platformCollider = currentOneWayPlatform.GetComponent<BoxCollider2D>();

        Physics2D.IgnoreCollision(playerBoxCollider, platformCollider);
        Physics2D.IgnoreCollision(playerHeadCollider, platformCollider);
        yield return new WaitForSeconds(4f);
        Physics2D.IgnoreCollision(playerBoxCollider, platformCollider, false);
        Physics2D.IgnoreCollision(playerHeadCollider, platformCollider, false);
    }*/
}