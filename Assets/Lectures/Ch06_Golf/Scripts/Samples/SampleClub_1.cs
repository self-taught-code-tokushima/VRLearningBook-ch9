using UnityEngine;

namespace Lectures.Ch06.Sample
{
    public class SampleClub_1 : MonoBehaviour
    {
        [SerializeField] float power = 10;

        void OnTriggerEnter(Collider other)
        {
            // ボール以外は無視
            if (!other.CompareTag("Ball"))
            {
                return;
            }

            var ballRigidBody = other.gameObject.GetComponent<Rigidbody>();
            // ボールが移動中は叩けない
            if (ballRigidBody.linearVelocity.sqrMagnitude > Mathf.Epsilon)
            {
                return;
            }

            // クラブの正面方向を力の方向とする
            var velocity = -transform.forward;
            ballRigidBody.AddForce(velocity * power, ForceMode.Impulse);
        }
    }
}