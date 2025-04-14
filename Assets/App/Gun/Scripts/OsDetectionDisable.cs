using UnityEngine;

namespace App.Samples.Gun
{
    public class OsDetectionDisable : MonoBehaviour
    {
        int gameObjLength;
        public GameObject[] GameObjList;

        void Awake()
        {
            gameObjLength = GameObjList.Length;

            // Androidを検知したときに自動的にgameObjectを無効化する
            Deactivate();
        }

        public void Activate()
        {
            for (int i = 0; i < gameObjLength; i++)
            {
                GameObjList[i].gameObject.SetActive(true);
            }
        }

        void Deactivate()
        {
            for (int i = 0; i < gameObjLength; i++)
            {
                GameObjList[i].gameObject.SetActive(false);
            }
        }
    }
}