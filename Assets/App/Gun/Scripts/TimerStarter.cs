using UnityEngine;

namespace App.Samples.Gun
{
    public class TimerStarter : MonoBehaviour
    {
        public ScoreManager ScoreManager;

        void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Bullet"))
            {
                ScoreManager.SetTimerFlag(true);
                gameObject.SetActive(false);
            }
        }
    }
}