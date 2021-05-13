using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace SharkRush
{
    /// <summary>
    /// Handles tracking the score of the game and displaying it.
    /// </summary>
    public class Score : MonoBehaviour
    {
        private int score;

        [SerializeField]
        private int increaseAmount = 100;
        [SerializeField]
        private int decreaseAmount = 50;
        [SerializeField]
        private int maxScore = 10000;

        [Header("References")]
        public TextMeshProUGUI scoreText;

        // Start is called before the first frame update
        void Start()
        {
            this.score = 0;
        }

        public void IncreaseScore()
        {
            AddScore(increaseAmount);
            UpdateText();
        }

        public void DecreaseScore()
        {
            AddScore(decreaseAmount);
            UpdateText();
        }

        private void AddScore(int amount)
        {
            this.score = Mathf.Clamp(score + amount, 0, maxScore);
        }

        private void UpdateText()
        {
            if (scoreText)
                scoreText.SetText(score.ToString());
        }
    } 
}
