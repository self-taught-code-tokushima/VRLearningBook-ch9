using TMPro;
using UnityEngine;

namespace App.Samples.Gun
{
    public class SimpleMagazineManagerStudy : MonoBehaviour
    {
        // Magazineの最大弾数
        public int MagazineBulletNumMax = 13;

        // Magazineの現時点の弾数
        int magazineBulletNumCurrent;

        void Start()
        {
            // 現時点の弾数を最大弾数に初期化
            magazineBulletNumCurrent = MagazineBulletNumMax;
        }

        // Pistolに現時点の弾数を渡すGet関数
        public int GetBulletNum()
        {
            return magazineBulletNumCurrent;
        }

        // Pistolから弾数の加算減算を受け取って現時点の弾数にするSet関数
        public void SetBulletNum(int pistolBulletNum)
        {
            magazineBulletNumCurrent = pistolBulletNum;
        }

        public void RemoveFromParent()
        {
            gameObject.transform.parent = null;
        }
    }
}