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

        private Vector3 travelDirection;
        [Header("References")]
        [SerializeField]
        private Rigidbody2D rb;
        // Start is called before the first frame update
        public virtual void Start()
        {

        }

        // Update is called once per frame
        public virtual void FixedUpdate()
        {

        }

        public virtual void Move()
        {
            float travelSpeed = speed;

            //Increase the speed while fleeing
            if (state == CreatureState.Fleeing)
                travelSpeed = speed * fleeSpeedMultiplier;


            if (rb)
            {

            }
        }

        public virtual void PickTravelDirection()
        {

        }
    }
}

