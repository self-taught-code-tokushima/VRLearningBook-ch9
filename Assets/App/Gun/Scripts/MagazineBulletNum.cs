using UnityEngine;

namespace App.Samples.Gun
{
    public class MagazineBulletNum : MonoBehaviour
    {
        [SerializeField] TextMesh tmBulletNum = null!;
        [SerializeField] TextMesh tmBulletNumTop = null!;
        public int BulletNumMax = 13;
        int bulletNum;
        public string BulletNumString = null!;

        void Start()
        {
            bulletNum = BulletNumMax;
            BulletNumString = bulletNum.ToString();
            EnableHudBottom();
        }

        void Update()
        {
            // ボタンの情報管理
            if (InputManagerLR.PrimaryButtonR_OnPress())
            {
                ResetNum();
            }
        }

        public void ResetNum()
        {
            Debug.Log($"{gameObject.name}: 弾数をMAXにリセットした");
            bulletNum = BulletNumMax;
            NumToString();
        }

        public void AddBulletNum(int addNum)
        {
            if (addNum < 0)
            {
                Debug.Log($"{gameObject.name}: 弾数を増やす処理で負の数が指定されている");
                addNum = 0;
            }

            bulletNum += addNum;
            NumToString();
        }

        public void SubBulletNum(int subNum)
        {
            if (subNum < 0)
            {
                Debug.Log($"{gameObject.name}: 弾数を減らす処理で負の数が指定されている");
                subNum = 0;
            }

            bulletNum -= subNum;
            NumToString();
        }

        public void DisableMagTextMesh()
        {
            // TextMeshを非表示
            tmBulletNum.gameObject.SetActive(true);
        }

        public void EnableTextMesh()
        {
            // TextMeshを表示
            tmBulletNum.gameObject.SetActive(true);
        }

        void NumToString()
        {
            if (bulletNum < 0)
            {
                bulletNum = 0;
            }

            // 弾数をテキストに変換
            BulletNumString = bulletNum.ToString();
            // テキストをTextMeshに代入
            tmBulletNum.text = BulletNumString;
            tmBulletNumTop.text = BulletNumString;
        }

        public void EnableHudBottom()
        {
            tmBulletNum.gameObject.SetActive(true);
            tmBulletNumTop.gameObject.SetActive(false);
        }

        public void EnableHudTop()
        {
            tmBulletNum.gameObject.SetActive(false);
            tmBulletNumTop.gameObject.SetActive(true);
        }

        public int GetBulletNum()
        {
            // 弾の数を伝える
            return bulletNum;
        }
    }
}