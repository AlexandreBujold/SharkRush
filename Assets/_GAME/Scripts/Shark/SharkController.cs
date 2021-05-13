using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SharkRush
{
    public class SharkController : MonoBehaviour
    {
        [SerializeField]
        private float speed = 5f;

        [SerializeField]
        [Range(0f, 5f)]
        [Tooltip("Only active when left mouse button is pressed.")]
        private float speedMultiplier = 1f;

        private float finalSpeed;
        [Header("References")]
        [SerializeField]
        private Rigidbody2D rb;
        [SerializeField]
        private Camera gameCamera;
        [SerializeField]
        private SpriteRenderer sprite;
        // Start is called before the first frame update
        void Start()
        {
            rb = rb == null ? GetComponent<Rigidbody2D>() : rb;
            gameCamera = gameCamera == null ? Camera.main : gameCamera;
        }

        // Update is called once per frame
        void Update()
        {
            //Increase speed by multiplier if LMB is held
            finalSpeed = Input.GetMouseButton(0) == true ? speed * speedMultiplier : speed;
            ControlShark();
        }

        private void FlipSharkSprite(Vector3 direction)
        {
            if (sprite)
                sprite.flipY = direction.x < 0 ? true : false;

        }

        /// <summary>
        /// Handles moving and rotating the shark towards the mouse.
        /// </summary>
        private void ControlShark()
        {
            //Calculate travel direction
            Vector3 travelDirection = (gameCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;

            //Rotate the shark towards movement
            float theta = Mathf.Atan2(travelDirection.y, travelDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(theta, Vector3.forward);

            FlipSharkSprite(travelDirection);
            if (rb != null)
            {
                rb.AddForce(travelDirection * finalSpeed);
            }
        }
    } 
}
