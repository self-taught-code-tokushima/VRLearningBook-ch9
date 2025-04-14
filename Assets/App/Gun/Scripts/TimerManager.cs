using UnityEngine;

namespace App.Samples.Gun
{
    public class TimerManager : MonoBehaviour
    {
        float timer;
        public float TimerLimit = 60.0f;
        bool startTimer;

        void Start()
        {
            ResetTimer();
        }

        void Update()
        {
            if (startTimer)
            {
                timer -= Time.deltaTime;
            }

            if (timer < 0)
            {
                timer = 0.0f;
                SetStartTimer(false);
            }
        }

        void SetStartTimer(bool activate)
        {
            // timerのスタートを外部から呼び出す
            //もしくは止める
            startTimer = activate;
        }

        void ResetTimer()
        {
            timer = TimerLimit;
        }
    }
}