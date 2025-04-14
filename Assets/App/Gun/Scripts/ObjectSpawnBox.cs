using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

namespace App.Samples.Gun
{
    public class ObjectSpawnBox : MonoBehaviour
    {
        public GameObject SpawnGameObject = null!;
        public XRSocketInteractor SocketInteractor = null!;

        void Start()
        {
            Spawn();
        }

        public void Spawn()
        {
            // Socketの中がnullだったら
            if (SocketInteractor.GetOldestInteractableSelected() == null)
            {
                // 指定のゲームオブジェクトを複製し、ソケットの位置に配置する
                var newSpawnGameObject = Instantiate(SpawnGameObject, gameObject.transform.position, gameObject.transform.rotation);
                Debug.Log($"{gameObject.name}が{newSpawnGameObject.gameObject.name}を{gameObject.transform.position}に生成した");
            }
            else
            {
                Debug.Log("Socketの中はまだなにか入っているのでスポーンできない");
            }
        }
    }
}