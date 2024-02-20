using UnityEngine;

namespace From_Other_Projects.Programming101
{
    public class Programming102: MonoBehaviour
    {
        float health;
        int score;
        bool canTakeDamage;
        string placeHolderString;

        private Collider2D collision;

        // Start is called before the first frame update
        private void Start()
        {
            health = 100f;
            canTakeDamage = false;
        }

        // Update is called once per frame
        private void Update()
        {
            if (health < 0)
            {
                RestartLevel();
            }

            if (health > 100f)
            {
                health = 100f;
            }

            if (collision.CompareTag("HealthPack"))
            {
                GetHealthPack();
            }

            if (collision.CompareTag("Enemy"))
            {
                if (canTakeDamage == false)
                {
                    TakeDamage();
                }
            
            }
        }

        private void RestartLevel()
        {
            // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private void GetHealthPack()
        {
            health += 20f;
        }

        private void TakeDamage()
        {
            health -= 10f;
        }
    
    }
}
