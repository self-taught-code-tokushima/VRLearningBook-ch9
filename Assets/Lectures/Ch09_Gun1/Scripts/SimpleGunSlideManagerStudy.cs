using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

namespace App.Samples.Gun
{
    public class SimpleGunSlideManagerStudy : MonoBehaviour
    {
        // 弾を発射する座標を入れる変数
        public Transform BulletSpawnTransform = null!;

        // 弾のプレハブを入れる変数
        public GameObject BulletPrefab = null!;

        // 弾の射出速度の変数
        public float BulletSpeed = 10.0f;

        // 弾の寿命時間の変数
        public float BulletLife = 5.0f;

        // 弾の重力の有無
        public bool BulletGravity;

        // 薬莢を発射する座標
        public Transform CaseSpawnTransform = null!;

        // 薬莢のプレハブ
        public GameObject CasePrefab = null!;

        // 薬莢の射出速度
        public float CaseSpeed = 2.5f;

        // 薬莢の寿命時間
        public float CaseLife = 2.0f;
        // 薬莢は重力で落下するものなので、重力切り替えは用意しない

        // Magazine関連
        // Magazineの情報をキャッシュ（一時保存）
        public GameObject MagazineCache;

        // 薬室を管理する
        bool isChamberFill;

        // スライドを管理する
        public SimpleSlideManagerStudy Slide = null!;

        public XRGrabInteractable XrgInt;

        // 効果音を管理
        public AudioSource AudioGunShot = null!;
        public AudioSource AudioGunEmpty = null!;
        public AudioSource AudioAlert = null!;

        void Start()
        {
            // GameObject自身にあるXR Grab Interactableを呼び出す

            XrgInt = GetComponent<XRGrabInteractable>();

            if (XrgInt != null)
            {
                // First Select EnteredとLast Select Exitedにイベントを追加する
                XrgInt.selectEntered.AddListener(OnFirstSelectEntered);
                XrgInt.selectExited.AddListener(OnLastSelectExited);
                Debug.Log($"{gameObject.name}のAddListenerを実行");
            }
            else
            {
                Debug.Log($"{gameObject.name}にXR Grab Interactableが入ってない");
            }
        }

        public void Update()
        {
            // プレイヤーの持ち手を判定するにはIsSelectedByRight/Leftを用いる
            // プレイヤーが銃を持っていない手のボタン操作に反応させないようにするため
            if (XrgInt.IsSelectedByRight() && InputManagerLR.SecondaryButtonR_OnPress())
            {
                ButtonSlide();
            }

            if (XrgInt.IsSelectedByLeft() && InputManagerLR.SecondaryButtonL_OnPress())
            {
                ButtonSlide();
            }
        }

        public void PullTrigger()
        {
            // 薬室が空だと、撃つことができない
            if (isChamberFill == false)
            {
                Debug.Log(gameObject.name + "の薬室は空なので撃てない");

                // 効果音を追加
                if (AudioGunEmpty != null)
                {
                    AudioGunEmpty.transform.position = gameObject.transform.position;
                    AudioGunEmpty.Play();
                }

                return;
            }

            // 弾を発射する
            LaunchBullet();

            // マガジンの残弾を確認し、処置する
            ChamberCheck();
        }

        public void ButtonSlide()
        {
            // スライドを握られているときにボタンを押されるとややこしいので
            // スライドを握っているときはボタン入力のスライドを無効化する
            if (Slide.GetIsGrabbed())
            {
                // none
                Debug.Log("スライドが握られているのでボタンに反応できない");
            }
            else
            {
                // スライド側でリロード処理を実行する
                Slide.SetButtonPressed(true);
            }
        }

        public void LaunchBullet()
        {
            // IInstantiateで弾のPrefabを複製し、弾の射出座標に配置する
            var newBullet = Instantiate(BulletPrefab, BulletSpawnTransform);

            // 薬莢は銃から独立したオブジェクトであることを保証する
            newBullet.gameObject.transform.parent = null;

            // 弾の物理演算に干渉するため、弾のRigidbodyを呼び出す
            var rbBullet = newBullet.GetComponent<Rigidbody>();

            // 弾の重力設定に作用する
            rbBullet.useGravity = BulletGravity;

            // 弾を弾の正面方向（ローカル座標のZ軸の正、青い矢印）に向かってAddForceのImpulseで射出する
            rbBullet.AddForce(newBullet.transform.forward * BulletSpeed, ForceMode.Impulse);

            // 効果音を追加
            if (AudioGunShot != null) 
            {
                AudioGunShot.transform.position = gameObject.transform.position;
                AudioGunShot.Play();
            }

            // 弾が寿命を迎えたら消滅させる
            Destroy(newBullet, BulletLife);
        }

        public void EjectCase()
        {
            // あとで排出処理をまとめる

            // 排莢を実行する
            // 例外をはじく
            if (isChamberFill == false)
            {
                Debug.Log("薬室に弾がないので排出する弾がない");
                return;
            }

            // Instantiateで薬莢のPrefabを複製し、薬莢の射出座標に配置する
            var newCase = Instantiate(CasePrefab, CaseSpawnTransform);

            // 薬莢は銃から独立したオブジェクトであることを保証する
            newCase.gameObject.transform.parent = null;

            // 薬莢の物理演算に干渉するため、薬莢のRigidbodyを呼び出す
            var rbBullet = newCase.GetComponent<Rigidbody>();

            // 薬莢を薬莢の正面方向（ローカル座標のZ軸の正、青い矢印）に向かってAddForceのImpulseで射出する
            rbBullet.AddForce(newCase.transform.forward * CaseSpeed, ForceMode.Impulse);

            // 薬莢が寿命を迎えたら消滅させる
            Destroy(newCase, CaseLife);
        }

        public void ChamberCheck()
        {
            // マガジンが挿さっていない場合
            if (MagazineCache == null)
            {
                // チェンバーに弾が入っていたとき
                if (GetChamberFill())
                {
                    EjectCase();
                    Debug.Log("薬室は空になった");
                    // スライドを後ろに引く
                    Slide.SetSlidePullBack(true);
                    Debug.Log("マガジンがないので、スライドを後ろに引いた");
                }
                else
                    // チェンバーに弾が入っていなかったとき
                {
                    Slide.SetSlidePullBack(false);
                    Debug.Log("薬室は元から空だった");
                }

                // 薬室の中を空にする
                isChamberFill = false;
                Debug.Log("薬室は空になった");

                return;
            }

            // マガジンが挿さっている場合
            if (MagazineCache.TryGetComponent<SimpleMagazineManagerStudy>(out var magazine))
            {
                // マガジンの残弾数が0以上のとき
                if (magazine.GetBulletNum() > 0)
                {
                    // マガジンの残弾数を1減らす
                    magazine.SetBulletNum(magazine.GetBulletNum() - 1);

                    // この時点で薬室が空の場合もあるため、考慮する
                    if (isChamberFill)
                    {
                        EjectCase();
                    }

                    // 薬室を埋める
                    isChamberFill = true;
                    Debug.Log("弾を補充、マガジンの残弾は残り " + magazine.GetBulletNum());
                }
                // マガジンの残弾数が0のとき
                else
                {
                    // スライドを後ろに引く
                    Slide.SetSlidePullBack(true);
                    Debug.Log("マガジンの残弾数が0なので、スライドを引いた");

                    // 効果音を再生
                    if (AudioAlert != null)
                    {
                        AudioAlert.transform.position = gameObject.transform.position;
                        AudioAlert.Play();
                    }

                    // この時点で薬室が空の場合もあるため、考慮する
                    if (isChamberFill)
                    {
                        EjectCase();
                    }

                    // 薬室の中が空になった
                    isChamberFill = false;
                    Debug.Log("薬室は空になった");
                }
            }
        }

        public bool GetChamberFill()
        {
            // 薬室に弾があるかどうかを外部に伝える
            return isChamberFill;
        }

        void OnFirstSelectEntered(SelectEnterEventArgs args)
        {
            Debug.Log($"{gameObject.name}がFirstSelectEnteredのイベント追加に成功");
        }

        void OnLastSelectExited(SelectExitEventArgs args)
        {
            Debug.Log($"{gameObject.name}がLastSelectExitedのイベント追加に成功");
        }
    }
}