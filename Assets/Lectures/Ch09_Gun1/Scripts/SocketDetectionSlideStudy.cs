using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

namespace App.Samples.Gun
{
    public class SocketDetectionSlideStudy : MonoBehaviour
    {
        // Socketの情報を格納する変数
        public XRSocketInteractor MagazineSocket;

        // Socketの情報をピストルに伝達するための変数
        // SimpleGunSlideManagerの入っているゲームオブジェクトのみ受け付ける
        public SimpleGunSlideManagerStudy GunManager;

        // スクリプト内でソケット内のゲームオブジェクトの情報を保つ
        IXRSelectInteractable socketObject;

        bool isForceEject;
        float timer;
        public float SocketTimer = 0.8f;

        // 効果音を追加
        public AudioSource Audio_EjectMagazine;
        public AudioSource Audio_SetMagazine;

        void Start()
        {
            MagazineSocket = GetComponent<XRSocketInteractor>();
        }

        void Update()
        {
            // ソケットは利き手を判定する情報を持たないので
            // 銃の利き手の情報をソケットで再利用する
            // 銃が右手で持たれているときは、ソケットはAボタンに反応する
            if (GunManager.XrgInt.IsSelectedByRight() && InputManagerLR.PrimaryButtonR_OnPress())
            {
                ForceEjectSocket();
            }

            // 銃が左手で持たれているときは、ソケットはXボタンに反応する
            if (GunManager.XrgInt.IsSelectedByLeft() && InputManagerLR.PrimaryButtonL_OnPress())
            {
                ForceEjectSocket();
            }

            // ForceEjectSocketを実行したら、ソケットの当たり判定を無効化するタイマーを用意する
            if (isForceEject)
            {
                timer += Time.deltaTime;
            }

            // タイマーが指定時間を超えたら、ソケットの当たり判定を復活させる
            if (timer > SocketTimer)
            {
                Debug.Log("Socket復活");
                timer = 0.0f;
                isForceEject = false;
                transform.GetComponent<BoxCollider>().enabled = true;
            }
        }

        public void SocketCheck()
        {
            // Socketの中身を特定する関数はGetOldestInteractableSelected()
            // Debug.Log($"firstInteractableSelected: {MagazineSocket.firstInteractableSelected}");
            // Debug.Log($"interactablesSelected: {MagazineSocket.interactablesSelected}");
            // Debug.Log($"hasSelection: {MagazineSocket.hasSelection}");
            // Debug.Log($"GetOldestInteractableSelected: {MagazineSocket.GetOldestInteractableSelected()}");

            socketObject = MagazineSocket.GetOldestInteractableSelected();
            // SelectTargetは旧式のため非推奨となった
            // IXRSelectInteractable objNameSelect = magSocket.selectTarget;
            if (socketObject != null)
            {
                Debug.Log($"magSocket: " + socketObject.transform.gameObject.name);
            }
            else
            {
                Debug.Log("magSocket is null");
            }
        }

        public void SendSocketInfo()
        {
            if (!GunManager)
            {
                Debug.Log(gameObject.name + "のSocketの親となる銃が登録されていません");
            }

            // ソケットに挿入されたマガジンの情報を銃本体に送る
            if (socketObject != null)
            {
                GunManager.MagazineCache = socketObject.transform.gameObject;
            }
            else
            {
                Debug.Log(gameObject.name + "のSocketの中のゲームオブジェクトはnullです");
            }
        }

        public void ResetSocketInfo()
        {
            // マガジンを強制イジェクトしたときに
            GunManager.MagazineCache = null;
            socketObject = null;
            Debug.Log("Socket情報をリセット");
        }

        public void ForceEjectSocket()
        {
            if (socketObject == null)
            {
                Debug.Log("socketの中身がnullなので強制排出できない");
                return;
            }

            // 特定の条件でソケットを強制無効化、中身を排出
            Debug.Log("ForceEjectSocket実行");
            MagazineSocket.interactionManager.SelectExit(MagazineSocket, socketObject);
            transform.GetComponent<BoxCollider>().enabled = false;
            isForceEject = true;

            // Socketからマガジンの情報を強制的に削除
            ResetSocketInfo();
        }
    }
}