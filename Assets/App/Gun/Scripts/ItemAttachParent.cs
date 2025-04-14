using UnityEngine;

namespace App.Samples.Gun
{
    public class ItemAttachParent : MonoBehaviour
    {
        // ゲームオブジェクトがぶつかったとき
        public void OnTriggerEnter(Collider other)
        {
            // ぶつかったゲームオブジェクトのコリジョンのレイヤーの種類を文字列にする
            string layerName = LayerMask.LayerToName(other.gameObject.layer);

            if (other.gameObject.tag == "Backpack")
            {
                // ぶつかったゲームオブジェクト（かばんを想定）を親ゲームオブジェクトにする
                transform.SetParent(other.gameObject.transform);
                // Rigidbodyを取得し、ゲームオブジェクトが物理設定で動かないようにする
                Rigidbody rb = transform.GetComponent<Rigidbody>();
                rb.isKinematic = true;
            }
        }

        // ゲームオブジェクトがプレイヤーに握られたとき
        public void ExitGrab()
        {
            // 親ゲームオブジェクトを空にして独立させる
            gameObject.transform.parent = null;
            // Rigidbodyを取得し、ゲームオブジェクトが物理設定で動くようにする
            Rigidbody rb = transform.GetComponent<Rigidbody>();
            rb.isKinematic = false;
        }
    }
}