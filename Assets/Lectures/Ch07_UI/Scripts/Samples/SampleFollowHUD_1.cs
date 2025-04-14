using UnityEngine;

namespace Lecture.Ch07.Sample
{
    public class SampleFollowHUD_1 : MonoBehaviour
    {
        [SerializeField] Transform target;
        [SerializeField] float distance;

        void LateUpdate()
        {
            UpdatePosition();
            UpdateRotation();
        }

        void UpdatePosition()
        {
            var targetPosition = GetTargetPosition();
            transform.position = targetPosition;
        }

        Vector3 GetTargetPosition()
        {
            return target.position + target.forward * distance;
        }

        void UpdateRotation()
        {
            transform.rotation = Quaternion.LookRotation(transform.position - target.position);
        }
    }
}