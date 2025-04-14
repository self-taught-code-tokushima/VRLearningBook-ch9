using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace App.Samples.Golf
{
    public class SampleScoreItem : MonoBehaviour
    {
        [SerializeField] Image background;
        [SerializeField] TMP_Text scoreText;

        public void UpdateScore(int score, bool isClear, bool isHighlight)
        {
            background.enabled = isHighlight;
            scoreText.color = isClear ? Color.red : isHighlight ? Color.blue : Color.black;
            scoreText.text = score == 0 ? "-" : score.ToString();
        }
    }
}