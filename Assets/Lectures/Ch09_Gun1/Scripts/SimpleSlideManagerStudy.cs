using UnityEngine;

namespace App.Samples.Gun
{
    public class SimpleSlideManagerStudy : MonoBehaviour
    {
        bool isGrabbed;
        Vector3 handPos = Vector3.zero;

        public GameObject ParentGun = null!;
        public GameObject SlideBody = null!;
        public GameObject VirtualAnchor = null!;

        // public TextMesh testNumbers = null!;
        float distanceFloat;
        public float DistanceLimitNegative = -0.01f;
        public float AnchorDistanceMin = -0.025f;
        public float AnchorDistanceMax;
        bool isSlidePullMin;
        bool isSlidePullMax;

        bool isSlidePullBack;
        bool isButtonPressed;

        public GrabInteractableHandler GIHandler;

        // 効果音を管理
        public AudioSource audio_GunSlide;

        void Update()
        {
            if (GIHandler != null)
            {
                Transform handTransform = GIHandler.GetHandTransform();
                if (handTransform != null)
                {
                    // 取得したhandTransformを使用する処理
                    Debug.Log("Using Hand Transform: " + handTransform.position);
                    // 例: 手の位置にオブジェクトを移動
                    handPos = handTransform.position;
                }
            }

            // プレイヤーにつかまれているときだけ動くUpdateを別途用意する
            if (isGrabbed)
            {
                GrabUpdate();
            }

            ReloadStateManager();

            // スライドが後ろに引かれていなければ
            if (GetSlidePullBack() && !isGrabbed)
            {
                // Debug.Log("Slideは後ろで引かれたまま固定されちる");
                SlideBody.transform.localPosition =
                    new Vector3(0, 0, DistanceLimitNegative) + VirtualAnchor.transform.localPosition;
            }
            // スライドが持たれておらず、後ろにも引かれていないときは
            else if (GetSlidePullBack() == false && !isGrabbed)
            {
                // Debug.Log("Slideはニュートラルに戻った");
                SlideBody.transform.localPosition = VirtualAnchor.transform.localPosition;
            }
        }

        public void Grabbed()
        {
            Debug.Log($"SimpleSlideが手につかまれた");
            isGrabbed = true;

            // GrabInteractableHanderを解説する
            if (GIHandler != null)
            {
                Transform handTransform = GIHandler.GetHandTransform();
                if (handTransform != null)
                {
                    // 取得したhandTransformを使用する処理
                    Debug.Log("Using Grab Transform: " + handTransform.position);
                    // _grabPos = handTransform.position;
                }
            }
        }

        public void Released()
        {
            Debug.Log($"SimpleSlideが手から離された");
            isGrabbed = false;
            // プレイヤーが手を離したら初期化する
            // 手を離したときに親を初期化する
            var slideSelf = gameObject;
            slideSelf.transform.parent = ParentGun.transform;
            slideSelf.transform.position = VirtualAnchor.transform.position;
            slideSelf.transform.rotation = VirtualAnchor.transform.rotation;
            // スライドの位置も初期化する
            // slideBody.transform.localPosition = Vector3.zero;
            if (isSlidePullBack || isSlidePullBack!)
            {
                SlideBody.transform.localPosition = VirtualAnchor.transform.localPosition;
                Debug.Log("SlidePullBackTrue");
            }
            else
            {
                SlideBody.transform.localPosition = VirtualAnchor.transform.localPosition;
                Debug.Log("SlidePullBackFalse");
            }
        }

        void GrabUpdate()
        {
            // 現在の手の位置と銃のスライドのアンカーの差分を出す
            var anchorTransform = VirtualAnchor.transform;
            var anchorPos = VirtualAnchor.transform.position;
            var distance = anchorPos - handPos;
            // 差分ベクトルをスライドの方向にのみ抽出する
            // 正射影ベクトルのProjectを用いて、ピストルのスライドに対する手の位置の正射影を求める
            var normalVector3 = Vector3.Project(distance, anchorTransform.forward);

            // distanceFloat（スライドの稼働範囲計算）はプレイヤーに捕まれているときのみ行う
            // distanceFloatの初期値はゼロ

            // 銃口の向きと「手の位置-銃口」ベクトルの角度が90度を越えていると、向きを反転する
            // この処理を入れると、距離を絶対値でなく正負で取得できるようになる
            if (Vector3.Angle(anchorTransform.forward, distance) < 90.0f)
            {
                distanceFloat = normalVector3.magnitude * -1.0f;
                Debug.Log("Over90  distanceFloat: " + distanceFloat);
            }
            else
            {
                distanceFloat = normalVector3.magnitude;
                Debug.Log("Under90  distanceFloat: " + distanceFloat);
            }

            // スライダーの可動範囲を制限する
            // スライダーが後ろの限界よりも後ろにあるとき
            if (distanceFloat < AnchorDistanceMin)
            {
                distanceFloat = AnchorDistanceMin;
                isSlidePullMax = true;
                Debug.Log("distanceFloat, anchorMin: " + distanceFloat);
            }
            // スライドを前方向の限界よりも前に引っ張っているとき & スライドが後ろに引かれていないとき
            else if (distanceFloat > AnchorDistanceMax && !GetSlidePullBack())
            {
                distanceFloat = AnchorDistanceMax;
                Debug.Log("distanceFloat, anchorMax: " + distanceFloat);
            }
            // スライドを前方向の限界よりも前に引っ張っているとき & スライドが後ろに引かれているとき
            else if (distanceFloat > DistanceLimitNegative && GetSlidePullBack())
            {
                distanceFloat = DistanceLimitNegative;
                Debug.Log("distanceFloat, disNegative: " + distanceFloat);
            }
            else
            {
                Debug.Log("distanceFLoatは可動範囲内のはず, distanceFloat: " + distanceFloat);
            }

            Debug.Log($"slideBody.normalVector3: {normalVector3}, magnitude: {normalVector3.magnitude}");

            SlideBody.transform.localPosition =
                VirtualAnchor.transform.localPosition + new Vector3(0, 0, distanceFloat);
            Debug.Log(
                $"slideBody.transform.localPosition: {SlideBody.transform.localPosition}, WorldPos: {SlideBody.transform.position}");
            Debug.Log("DistanceFloat: " + distanceFloat);
        }


        void ReloadStateManager()
        {
            var gunBody = ParentGun.GetComponent<SimpleGunSlideManagerStudy>();

            // スライドのdistanceFloatが閾値を越えたときにCheckChamberSlideを実行する
            // Chamberの状態によってスライドを引くの閾値が異なる

            // プレイヤーがスライドを後ろ最大にまで引っ張って、手を離した瞬間
            if (isSlidePullMax && isGrabbed == false || GetButtonPressed())
            {
                if (GetButtonPressed())
                {
                    Debug.Log("GetButtonPressedで発火");
                }

                Debug.Log($"{name}: スライド側でCheckChamberSlide発火");
                gunBody.ChamberCheck();
                // スライドがニュートラル化する
                isSlidePullMax = false;
                // スライドが後ろに下がっていた場合は、それがニュートラル化する
                isSlidePullBack = false;

                // ボタン処理用
                SetButtonPressed(false);

                // 効果音を再生
                if (audio_GunSlide != null)
                {
                    audio_GunSlide.transform.position = gameObject.transform.position;
                    audio_GunSlide.Play();
                }
                else
                {
                    Debug.Log("効果音GunSlideはセットされていない");
                }
            }

            if (isSlidePullMin && isGrabbed == false)
            {
                // none
                isSlidePullMin = false;
            }
        }

        public void SetSlidePullBack(bool pullback)
        {
            isSlidePullBack = pullback;
        }

        public bool GetSlidePullBack()
        {
            return isSlidePullBack;
        }

        // ボタンによるスライド用
        public void SetIsSlidePullMax(bool pull)
        {
            isSlidePullMax = pull;
        }

        public void SetButtonPressed(bool push)
        {
            isButtonPressed = push;
        }

        public bool GetButtonPressed()
        {
            return isButtonPressed;
        }

        public bool GetIsGrabbed()
        {
            return isGrabbed;
        }
    }
}