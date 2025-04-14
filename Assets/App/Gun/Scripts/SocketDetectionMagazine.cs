using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

namespace App.Samples.Gun
{
    public class SocketDetectionMagazine : MonoBehaviour
    {
        // Socketの情報を格納する変数
        public XRSocketInteractor MagazineSocket;

        // Socketの情報をピストルに伝達するための変数
        // SimpleGunMagazineManagerの入っているゲームオブジェクトのみ受け付ける
        public SimpleGunMagazineManager GunManager;

        // スクリプト内でソケット内のゲームオブジェクトの情報を保つ
        IXRSelectInteractable socketObject;

        void Start()
        {
            MagazineSocket = GetComponent<XRSocketInteractor>();
        }

        public void SendSocketInfo()
        {
            // ソケットに挿入されたマガジンの情報を銃本体に送る
            if (GunManager != null && socketObject != null)
            {
                GunManager.MagazineCache = socketObject.transform.gameObject;
            }
            else if (GunManager == null)
            {
                Debug.Log(gameObject.name + "のSocketの親となる銃が登録されていません");
            }
            else
            {
                Debug.Log(gameObject.name + "のSocketの中のゲームオブジェクトはnullです");
            }
        }

        public void ResetSocketInfo()
        {
            GunManager.MagazineCache = null;
            socketObject = null;
            Debug.Log("Socket情報をリセット");
        }
    }
}
