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

        [SerializeField]
        private CreatureState state;

        [SerializeField]
        private float speed = 1f;

        [SerializeField]
        private float fleeSpeedMultiplier = 1.5f;

        [Space]

        [SerializeField]
        public float minTravelTime = 2f;

        [SerializeField]
        public float maxTravelTime = 5f;

        public float travelTime = 3f;

        [SerializeField]
        private Vector3 travelDirection;
        private float timeStartedTravelingInDirection;
        [Header("References")]
        [SerializeField]
        private Rigidbody2D rb;
        // Start is called before the first frame update
        public virtual void Start()
        {
            PickTravelDirection();
        }

        // Update is called once per frame
        public virtual void FixedUpdate()
        {
            Move();
        }

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

        public void GetEaten()
        {
            //Spawn particles here 
            Destroy(gameObject);
        }
    }
}

