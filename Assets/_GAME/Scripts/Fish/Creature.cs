using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SharkRush
{
    /// <summary>
    /// Responsible for controlling the creature and containing all relevant information.
    /// </summary>
    public class Creature : MonoBehaviour
    {
        public CreatureType type;

        private enum CreatureState
        { 
            Swimming,
            Fleeing
        }

        [Header("Creature Properties")]
        [SerializeField]
        private CreatureState state;
        [SerializeField]
        private float speed = 1f;
        [SerializeField]
        private float fleeSpeedMultiplier = 1.5f;

        [Space]
        [SerializeField]
        private float minTravelTime = 2f;
        [SerializeField]
        private float maxTravelTime = 5f;
        [SerializeField]
        private float travelTime = 3f;

        [Space]
        [SerializeField]
        [Tooltip("Tags that when detected will make the creature want to flee.")]
        private List<string> fleeTags;

        [SerializeField]
        [Range(0f, 10f)]
        private float fleeRange = 5f;

        [SerializeField]
        private GameObject predator;

        [SerializeField]
        private Vector3 travelDirection;
        private float timeStartedTravelingInDirection;
        [Header("References")]
        [SerializeField]
        private Rigidbody2D rb;
        [SerializeField]
        private SpriteRenderer sprite;
        [SerializeField]
        public GameObject eatEffectPrefab;
        // Start is called before the first frame update
        public virtual void Start()
        {
            PickTravelDirection();
            fleeTags = fleeTags == null ? new List<string>() : fleeTags;
            sprite = sprite == null ? GetComponentInChildren<SpriteRenderer>() : sprite;
        }

        // Update is called once per frame
        public virtual void FixedUpdate()
        {
            CheckForPredator();
            Move();
        }

        #region Movement

        public virtual void Move()
        {
            float travelSpeed = speed;

            //Increase the speed while fleeing
            if (state == CreatureState.Fleeing)
                travelSpeed = speed * fleeSpeedMultiplier;

            //Determine travel direction if its time
            if ((Time.time - timeStartedTravelingInDirection) >= travelTime)
                PickTravelDirection();

            if (rb)
                rb.AddForce(travelDirection * travelSpeed);

            float theta = Mathf.Atan2(travelDirection.y, travelDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(theta, Vector3.forward);

            if (sprite)
                sprite.flipY = travelDirection.x < 0 ? true : false;
        }

        public virtual void PickTravelDirection()
        {
            travelDirection = GetRandomDirection();
            timeStartedTravelingInDirection = Time.time;
            travelTime = Random.Range(minTravelTime, maxTravelTime);
        }

        private Vector2 GetRandomDirection()
        {
            return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        }

        #endregion

        #region State & Interaction

        private void SetState(CreatureState newState)
        {
            if (state != newState)
                state = newState;
        }

        public void GetEaten()
        {
            if (eatEffectPrefab != null)
                Instantiate(eatEffectPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        private void CheckForPredator()
        {
            if (predator)
            {
                if (Vector3.Distance(predator.transform.position, transform.position) <= fleeRange)
                {
                    SetState(CreatureState.Fleeing);
                }
                else
                {
                    SetState(CreatureState.Swimming);
                }
            }
        }

        #endregion
    }
}

