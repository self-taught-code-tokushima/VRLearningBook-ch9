using UnityEngine;

namespace Lecture.Ch07.Sample
{
    public class SampleFollowHUD_2 : MonoBehaviour
    {
        [SerializeField] Transform target;
        [SerializeField] float distance;
        // 追加
        [SerializeField] float height = 1.4f;
        [SerializeField] float followMoveSpeed = 3f;

        void LateUpdate()
        {
            UpdatePosition();
            UpdateRotation();
        }

        void UpdatePosition()
        {
            var targetPosition = GetTargetPosition();
            // 座標の更新を滑らかにする
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * followMoveSpeed);
        }

        Vector3 GetTargetPosition()
        {
            var pos = target.position + target.forward * distance;
            // 高さを一定にする
            pos.y = height;
            return pos;
        }

        void UpdateRotation()
        {
            transform.rotation = Quaternion.LookRotation(transform.position - target.position);
        }
    }
}