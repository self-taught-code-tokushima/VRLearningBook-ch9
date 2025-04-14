using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

namespace App.Samples.Gun
{
    public class GrabInteractableHandler : MonoBehaviour
    {
        XRGrabInteractable grabInteractable;
        Transform handTransform;

        void Awake()
        {
            grabInteractable = GetComponent<XRGrabInteractable>();
            // AddListenerを使うことによって、プレイヤーがエディターで指定しなくても
            // 指定のオブジェクトのイベントにスクリプトで用意した関数を追加、実行できる
            grabInteractable.selectEntered.AddListener(OnSelectEntered);
            grabInteractable.selectExited.AddListener(OnSelectExited);
        }

        void OnDestroy()
        {
            // ゲーム停止時に命令を破棄する
            grabInteractable.selectEntered.RemoveListener(OnSelectEntered);
            grabInteractable.selectExited.RemoveListener(OnSelectExited);
        }

        void OnSelectEntered(SelectEnterEventArgs args)
        {
            // 手の座標の変数を確保する
            handTransform = args.interactorObject.transform;
            Debug.Log("HandがグラブしはじめたPosition: " + handTransform.position);
        }

        void OnSelectExited(SelectExitEventArgs args)
        {
            handTransform = null;
            Debug.Log("Handはグラブをやめた");
        }

        public Transform GetHandTransform()
        {
            // Handにグラブされたオブジェクトに座標を渡すための関数
            return handTransform;
        }
    }
}