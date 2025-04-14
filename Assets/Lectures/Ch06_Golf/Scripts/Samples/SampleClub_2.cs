using UnityEngine;

namespace Lectures.Ch06.Sample
{
    public class SampleClub_2 : MonoBehaviour
    {
        [SerializeField] float basePower = 2f;
        [SerializeField] float swingSpeedMultiplier = 2f;
        [SerializeField] float minimumSwingSpeed = 0.02f;

        Vector3 previousPosition;
        Vector3 currentVelocity;

        void Start()
        {
            previousPosition = transform.position;
        }

        void Update()
        {
            // クラブの速度を計算
            currentVelocity = (transform.position - previousPosition) / Time.deltaTime;
            previousPosition = transform.position;
        }

        void OnTriggerEnter(Collider other)
        {
            // ボール以外は無視
            if (!other.CompareTag("Ball"))
            {
                return;
            }

            var ballRigidbody = other.gameObject.GetComponent<Rigidbody>();
            // ボールが移動中は叩けない
            if (ballRigidbody.linearVelocity.sqrMagnitude > Mathf.Epsilon)
            {
                return;
            }

            // クラブヘッドの速さを計算
            float swingSpeed = currentVelocity.magnitude;

            // 最低速度未満の場合は打撃をキャンセル
            if (swingSpeed < minimumSwingSpeed)
            {
                return;
            }

            // クラブの正面方向を力の方向とする
            var direction = -transform.forward;

            // クラブの振りの速度を力に反映
            float totalPower = basePower * (1f + swingSpeed * swingSpeedMultiplier);

            // ボールに力を加える
            ballRigidbody.AddForce(direction * totalPower, ForceMode.Impulse);

            // デバッグ用（必要に応じて）
            Debug.Log($"Swing Speed: {swingSpeed}, Total Power: {totalPower}");
        }
    }
}