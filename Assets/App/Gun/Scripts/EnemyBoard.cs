using TMPro;
using UnityEngine;

namespace App.Samples.Gun
{
    public class EnemyBoard : MonoBehaviour
    {
        public GameObject TargetMark = null!;
        public GameObject HitTransform = null!;
        public ScoreManager ScoreManager;
        public TextMeshPro TextMeshPro;
        public TextMeshPro TextMeshPro2;
        public float Score = 100.0f;
        bool isAlreadyHitted;
        float rate = 10.0f;
        public Material DefaultMaterial = null!;
        public Material DamagedMaterial = null!;

        void Start()
        {
            TextMeshPro.text = "";
            if (DefaultMaterial == null)
            {
                Debug.Log($"{gameObject.name}のdefaultMaterialがnullだった");
                DefaultMaterial = gameObject.GetComponent<MeshRenderer>().materials[0];
            }

            if (DamagedMaterial == null)
            {
                Debug.Log($"{gameObject.name}のdamagedMaterialがnullだった");
                DamagedMaterial = DefaultMaterial;
            }
        }

        void Update()
        {
            TextMeshPro2.text = TextMeshPro.text;
        }

        void OnCollisionEnter(Collision collision)
        {
            Debug.Log($"Self: {gameObject.name}, hit: {collision.gameObject.name}");
            // TagがBulletだった場合
            if (collision.gameObject.CompareTag("Bullet") && isAlreadyHitted == false)
            {
                var subtractVector3 = collision.transform.position - TargetMark.transform.position;
                float length = subtractVector3.magnitude;
                CalculateScore(length);
                TextMeshPro.text = "SCORE: " + Score;

                HitTransform.transform.position = collision.transform.position;
                HitTransform.SetActive(true);

                ScoreManager.AddScore(Score);
                ScoreManager.AddDefeatEnemyNum(1);

                isAlreadyHitted = true;
                Destroy(collision.gameObject);

                GetComponent<MeshRenderer>().material = DamagedMaterial;
            }
        }

        void CalculateScore(float length)
        {
            Score -= length * rate;
            if (Score < 0)
            {
                Score = 0;
            }
        }

        public void ResetEnemyBoard()
        {
            TextMeshPro.text = "";
            HitTransform.SetActive(false);
            isAlreadyHitted = false;
        }
    }
}