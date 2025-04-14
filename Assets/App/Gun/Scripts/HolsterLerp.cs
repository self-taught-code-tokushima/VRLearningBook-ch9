using UnityEngine;

namespace App.Samples.Gun
{
    public class HolsterLerp : MonoBehaviour
    {
        // XR Origin (XR Rig)直下のCameraOffsetを
        [SerializeField] Transform cameraOffset;

        // ソケットの角度をカメラの向きに合わせる
        [SerializeField] Transform playerHead;
        [SerializeField] float timeRatio = 0.5f;
        float timeCount;

        void Start()
        {
            // タイマーを初期化
            timeCount = 0.0f;
        }

        void Update()
        {
            // 回転の処理にはクォータニオンを使う
            transform.rotation = Quaternion.Slerp(cameraOffset.rotation, playerHead.rotation, timeCount * timeRatio);
            // rotationをY軸以外0にする処理
            transform.rotation = Quaternion.Euler(new Vector3(0f, transform.eulerAngles.y, 0f));
            timeCount = timeCount + Time.deltaTime;
        }
    }
}