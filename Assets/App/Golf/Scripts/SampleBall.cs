using UnityEngine;

namespace App.Samples.Golf
{
    /// <summary>
    /// ボール
    /// </summary>
    public class SampleBall : MonoBehaviour
    {
        [SerializeField] Rigidbody rigidbody;
        [SerializeField] Renderer renderer;
        public int StageIndex;

        void Update()
        {
            // 移動中は色を変える
            renderer.material.color = rigidbody.linearVelocity.sqrMagnitude > Mathf.Epsilon ? Color.blue : Color.white;
        }
    }
}