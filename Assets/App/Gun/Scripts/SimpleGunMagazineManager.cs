using UnityEngine;

namespace App.Samples.Gun
{
    public class SimpleGunMagazineManager : MonoBehaviour
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

        // Magazine関連
        // Magazineの情報をキャッシュ（一時保存）
        public GameObject MagazineCache;

        public void PullTrigger()
        {
            if (MagazineCache == null)
            {
                Debug.Log(gameObject.name + "のマガジンがnullなので撃てない");
                Blank();
                return;
            }

            Debug.Log("SocketMagがnullではない");
            if (MagazineCache.TryGetComponent<SimpleMagazineManager>(out var socketMag))
            {
                // 弾が1発未満のときは、撃てない
                if (socketMag.GetBulletNum() < 1)
                {
                    Debug.Log(socketMag.gameObject.name + "は撃てない、弾の数が" + socketMag.GetBulletNum());
                    Blank();
                }
                else
                {
                    socketMag.SetBulletNum(socketMag.GetBulletNum() - 1);
                    Debug.Log(socketMag.gameObject.name + "は撃った、弾の数は" + socketMag.GetBulletNum());
                    Fire();
                }
            }
        }
        private void Blank()
        {
            Debug.Log("空撃ち");
        }

        private void Fire()
        {
            // IInstantiateで弾のPrefabを複製し、弾の射出座標に配置する
            var newBullet = Instantiate(BulletPrefab, BulletSpawnTransform);

            // 弾は銃から独立したオブジェクトであることを保証する
            newBullet.gameObject.transform.parent = null;

            if (newBullet.gameObject.transform.parent == null)
            {
                Debug.Log(newBullet.gameObject.name + "の親はnull");
            }
            else
            {
                Debug.Log(newBullet.gameObject.name + "の親は" + newBullet.gameObject.transform.parent);
            }

            // 弾の物理演算に干渉するため、弾のRigidbodyを呼び出す
            var rbBullet = newBullet.GetComponent<Rigidbody>();

            // 弾の重力設定に作用する
            rbBullet.useGravity = BulletGravity;

            // 弾を弾の正面方向（ローカル座標のZ軸の正、青い矢印）に向かってAddForceのImpulseで射出する
            rbBullet.AddForce(newBullet.transform.forward * BulletSpeed, ForceMode.Impulse);

            // 弾が寿命を迎えたら消滅させる
            Destroy(newBullet, BulletLife);
        }
    }
}