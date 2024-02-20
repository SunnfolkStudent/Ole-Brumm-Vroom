using UnityEngine;

namespace From_Other_Projects
{
    public class ParallaxObject : MonoBehaviour
    {
        private Material _mat;
        private float _distance;

        [Header("Adjust this range according to distance away from player in 3D:")][Range(0f, 30f)] 
        public float speed = 2f;

        void Update()
        {
            transform.Translate(Vector3.left * (Time.deltaTime * speed));
        }
    }
}

