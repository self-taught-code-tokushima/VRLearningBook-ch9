using UnityEngine;

namespace App.Samples.Gun
{
    public class SpectatorCameraManager : MonoBehaviour
    {
        public bool EnableSwitchCam;
        float timer;
        public float SwitchTime = 8.0f;
        public Camera[] SpectatorCams;

        public int SpectatorCamNum;

        void Start()
        {
            // エティターでSpectatorCamNumに数値を入れると、
            // それがゲーム開始時のカメラとなる

            if (SpectatorCams[0] == null)
            {
                EnableSwitchCam = false;
                return;
            }

            // すべてのスペクテーターカメラの優先度を0にリセット
            for (int i = 0; i < SpectatorCams.Length - 1; i++)
            {
                SpectatorCams[i].depth = 0;
            }

            // 指定したSpectatorCamNumを、spectatorCamsの大きさに収める
            if (SpectatorCamNum > SpectatorCams.Length)
            {
                SpectatorCamNum = SpectatorCams.Length - 1;
            }
            else if (SpectatorCamNum < 0)
            {
                SpectatorCamNum = 0;
            }

            // エディターで指定したスペクテーターカメラの優先度を高くする
            SpectatorCams[SpectatorCamNum].depth = 1;
        }

        void Update()
        {
            // カメラの切り替え処理を行わないのであれば、以下の処理はすべて無視する
            if (!EnableSwitchCam)
            {
                return;
            }

            // timer処理を行う
            timer += Time.deltaTime;

            // 指定時間を超えた場合
            if (timer > SwitchTime)
            {
                SpectatorCamNum++;

                // 今映していたスペクテーターカメラの優先度を下げる

                // spectatorCamNumが配列の大きさを超えた場合、0にリセットする。
                if (SpectatorCamNum > SpectatorCams.Length - 1)
                {
                    SpectatorCamNum = 0;
                    SpectatorCams[SpectatorCams.Length - 1].depth = 0;
                }
                else
                {
                    SpectatorCams[SpectatorCamNum - 1].depth = 0;
                }

                // 次のスペクテーターカメラの優先度を1にする
                SpectatorCams[SpectatorCamNum].depth = 1;

                timer = 0;
            }
        }
    }
}