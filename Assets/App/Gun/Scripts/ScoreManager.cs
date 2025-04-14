using TMPro;
using UnityEngine;

namespace App.Samples.Gun
{
    public class ScoreManager : MonoBehaviour
    {
        static ScoreManager instance;

        public TextMeshPro Tmp = null!;
        public TextMeshPro TmpEnemy = null!;
        public TextMeshPro TmpTimer = null!;
        public EnemyBoard[] EnemyBoard;
        float totalScore;

        int enemyNum;
        int defeatedEnemyNum;

        bool isTimerFlag;
        float timer;

        void Start()
        {
            ResetScore();
            enemyNum = EnemyBoard.Length;
        }

        void Update()
        {
            if (isTimerFlag)
            {
                timer += Time.deltaTime;
            }

            if (defeatedEnemyNum == enemyNum)
            {
                isTimerFlag = false;
            }

            Tmp.text = "TOTAL SCORE: " + totalScore;
            TmpEnemy.text = "DEFEATED ENEMIES: " + defeatedEnemyNum + " / " + enemyNum;
            TmpTimer.text = "TIME: " + timer;
        }

        public void ResetScore()
        {
            totalScore = 0;
        }


        public void AddScore(float addScore)
        {
            // Score���Z
            totalScore += addScore;
        }

        public void AddDefeatEnemyNum(int addNum)
        {
            defeatedEnemyNum += addNum;
        }

        public void SubtractScore(float subScore)
        {
            // Score���Z
            totalScore -= subScore;
        }

        public static float GetScore()
        {
            return instance.totalScore;
        }

        public void ResetEnemyCount()
        {
            defeatedEnemyNum = 0;
        }

        public void SetTimerFlag(bool timerFlag)
        {
            isTimerFlag = timerFlag;
        }

        public void ResetTimer()
        {
            timer = 0;
        }
    }
}