using UnityEngine;
using UnityEngine.UI;

namespace From_Other_Projects
{
    public class HealthScript : MonoBehaviour
    {
        [Header("Health")] 
        public int lives = 3;
        public int maxLives = 3;

        [Header("IFrames")] 
        public bool canTakeDamage;
        public float canTakeDamageTime = 0.1f;
        public float canTakeDamageCounter;

        private AudioSource _audioSource;
        public AudioSource hurtClip;
        public AudioClip[] pickupClips;
        //private SceneController _sceneController;
        public Image[] hearts;
        // private GameObject[] Hearts;
    
        [SerializeField] private AudioSource hurtSoundEffect;
    
        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
            //_sceneController = GetComponent<SceneController>();
        }
    
   
    

        private void Update()
        {
            if (Time.time > canTakeDamageCounter && !canTakeDamage)
            {
                canTakeDamage = true;
            }
        }
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("SuckHazard"))
            {
                TakeDamage();
                other.gameObject.SetActive(false);
            }
            if (other.gameObject.CompareTag("Heart"))
            {
                if (lives >= maxLives) return;
                lives++;
                SetHealthUI();
                Destroy(other.gameObject);
            }
        }
  

        public void TakeDamage()
            //Gå gjennom array med forloop uten gameobject.find! (Setactive to false or destroy?)
        {
            if (!canTakeDamage) return;
            lives -= 1;
            hurtClip.Play(0);
            SetHealthUI();
        
            if (lives <= 0)
            {
                // SceneController.LoadScoreScreen();
            }

            canTakeDamage = false;
            canTakeDamageCounter = Time.time + canTakeDamageTime;
        }

        private void SetHealthUI()
        {
            for (int i = 0; i < hearts.Length; i++)
            {
                if (i < lives)
                {
                    hearts[i].color = new Color(1, 0, 0, 1);
                }
                else
                {
                    hearts[i].color = new Color(1, 1, 1, 0.1f);
                }
            }
        }
    }
}