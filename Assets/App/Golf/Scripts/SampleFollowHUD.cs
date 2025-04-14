using UnityEngine;

namespace App.Samples.Golf
{
    public class SampleFollowHUD : MonoBehaviour
    {
        [SerializeField] Transform target;
        [SerializeField] float followMoveSpeed = 0.1f;
        [SerializeField] float height;
        [SerializeField] float distance;

        void LateUpdate()
        {
            UpdatePosition();
            UpdateRotation();
        }

        void UpdatePosition()
        {
            var targetPosition = GetTargetPosition();
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * followMoveSpeed);
        }

        Vector3 GetTargetPosition()
        {
            var pos = target.position + target.forward * distance;
            pos.y = height;
            return pos;
        }

        void UpdateRotation()
        {
            transform.rotation = Quaternion.LookRotation(transform.position - target.position);
        }
    }
}