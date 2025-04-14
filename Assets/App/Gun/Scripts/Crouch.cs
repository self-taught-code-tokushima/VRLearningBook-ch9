using Unity.XR.CoreUtils;
using UnityEngine;

namespace App.Samples.Gun
{
    public class Crouch : MonoBehaviour
    {
        public XROrigin XROrigin = null!;

        // VR内で立っているときの目線の高さ
        public float crouchHeight = 0.9f;

        // VR内でしゃがんでいるときの目線の高さ
        public float standHeight = 1.7f;

        // VR内のプレイヤーはしゃがんでいるか？
        bool isCrouch;

        void Awake()
        {
            // トラッキングの基準を床ではなくデバイスにする
            XROrigin.RequestedTrackingOriginMode = XROrigin.TrackingOriginMode.Device;
            // VR内のプレイヤーの目線の高さを「立っているとき」にする
            XROrigin.CameraYOffset = standHeight;
            // VR内のプレイヤーの目線の高さを「しゃがんでいるとき」にする
            isCrouch = false;
        }

        void Update()
        {
            // 右スティックを押し込んだとき
            if (InputManagerLR.ThumbstickR_OnPress())
            {
                // しゃがんでいるか？
                if (isCrouch)
                {
                    // 立っているときの目線の高さにする
                    XROrigin.CameraYOffset = crouchHeight;
                    isCrouch = false;
                }
                else
                {
                    // しゃがんでいるときの目線の高さにする
                    XROrigin.CameraYOffset = standHeight;
                    isCrouch = true;
                }
            }
        }
    }
}