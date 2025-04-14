using UnityEngine;

namespace App.Samples.Golf
{
    /// <summary>
    /// ゴールの当たり判定を行うクラス
    /// </summary>
    public class SampleGoalCollider : MonoBehaviour
    {
        [SerializeField] GameObject resultUI;

        void Start()
        {
            resultUI.SetActive(false);
        }

        void OnCollisionEnter(Collision other)
        {
            var ball = other.gameObject.GetComponent<SampleBall>();
            if (ball != null)
            {
                FindFirstObjectByType<SamplePauseHUD>().Clear(ball.StageIndex);
                resultUI.SetActive(true);
            }
        }
    }
}