using UnityEngine;

namespace From_Other_Projects
{
    public class CameraFollow : MonoBehaviour
    {
        public Transform target;
        public float smoothSpeed = 0.125f;
        public Vector3 locationOffset;
        public Vector3 rotationOffset;

        private void Start()
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }

        void FixedUpdate()
        {
            var rotation = target.rotation;
            Vector3 desiredPosition = target.position + rotation * locationOffset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;

            Quaternion desiredRotation = rotation * Quaternion.Euler(rotationOffset);
            Quaternion smoothedRotation = Quaternion.Lerp(transform.rotation, desiredRotation, smoothSpeed);
            rotation = smoothedRotation;
        }
    }
}
