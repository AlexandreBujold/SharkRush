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

        [Space]
        [Header("Stamina")]
        [SerializeField]
        private float stamina = 100f;
        [SerializeField]
        private float maxStamina = 100f;
        [SerializeField]
        private float staminaRecoveryRate = 10f;
        [SerializeField]
        private float rushCost = 10f;

        private float finalSpeed;
        [Header("References")]
        [SerializeField]
        private Rigidbody2D rb;
        [SerializeField]
        private Camera gameCamera;
        [SerializeField]
        private SpriteRenderer sprite;
        [SerializeField]
        private StaminaIndicator staminaIndicator;
        // Start is called before the first frame update
        void Start()
        {
            //Get References
            rb = rb == null ? GetComponent<Rigidbody2D>() : rb;
            gameCamera = gameCamera == null ? Camera.main : gameCamera;

            //Initialize Stamina
            stamina = maxStamina;
        }

        // Update is called once per frame
        void Update()
        {
            HandleRush();
            ControlShark();
        }

        private void FlipSharkSprite(Vector3 direction)
        {
            if (sprite)
                sprite.flipY = direction.x < 0 ? true : false;
        }

        private void HandleRush()
        {
            //If left mouse button is pressed...
            if (Input.GetMouseButton(0) == true)
            {
                finalSpeed = speed * speedMultiplier;
                stamina = Mathf.Clamp(stamina - (rushCost * Time.deltaTime), 0, maxStamina);
            }
            else
            {
                finalSpeed = speed;
                stamina = Mathf.Clamp(stamina + (staminaRecoveryRate * Time.deltaTime), 0, maxStamina);
            }

            if (staminaIndicator)
                staminaIndicator.UpdateUI(stamina, 0, maxStamina);
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
