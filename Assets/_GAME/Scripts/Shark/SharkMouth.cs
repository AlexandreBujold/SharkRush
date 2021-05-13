using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SharkRush
{
    /// <summary>
    /// Handles collision with fish and communicating that information to the appetite controller.
    /// </summary>
    public class SharkMouth : MonoBehaviour
    {
        [SerializeField]
        private List<string> foodTags;

        [Header("References")]
        [SerializeField]
        private Appetite sharkAppetite;
        // Start is called before the first frame update
        void Start()
        {
            if (foodTags == null)
                foodTags = new List<string>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            HandleCollision(collision.gameObject);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            HandleCollision(collision.gameObject);
        }

        private void HandleCollision(GameObject target)
        {
            //If its food, eat it.
            if (foodTags.Contains(target.tag) == true)
            {
                Creature creature = target.GetComponent<Creature>();
                if (creature)
                {
                    //Check if the creature is the appropriate type
                    if (sharkAppetite)
                        sharkAppetite.CheckMeal(creature.type);

                    //Eat the creature
                    creature.GetEaten();
                }
            }
        }
    } 
}
