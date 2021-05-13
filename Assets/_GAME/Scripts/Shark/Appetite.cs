using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SharkRush
{
    /// <summary>
    /// Responsible for determining the Shark's desired fish and checking if satiated.
    /// </summary>
    public class Appetite : MonoBehaviour
    {
        [SerializeField]
        private CreatureType desiredFish;

        [Header("References")]
        [SerializeField]
        private SharkController sharkController;
        [SerializeField]
        private Score score;
        [SerializeField]
        private AppetiteIndicator indicator;
        // Start is called before the first frame update
        void Start()
        {
            //Pick a random fish to start the game off with
            PickNextFish();
        }

        public bool CheckMeal(CreatureType typeEaten)
        {
            //Return if it isn't the correct fish
            if (typeEaten != desiredFish)
            {
                RewardIncorrectFish();
                return false;
            }
            RewardCorrectFish();
            return true;
        }

        private void RewardCorrectFish()
        {
            PickNextFish();

            if (score)
                score.IncreaseScore();
        }

        private void RewardIncorrectFish()
        {
            PickNextFish();

            if (score)
                score.DecreaseScore();
        }

        private void PickNextFish()
        {
            this.desiredFish = (CreatureType)Random.Range(0, System.Enum.GetValues(typeof(CreatureType)).Length);
            if (indicator)
                indicator.UpdateIndicator(desiredFish);
        }
    } 
}
