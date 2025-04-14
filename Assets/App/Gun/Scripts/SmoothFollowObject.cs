using UnityEngine;

namespace App.Samples.Gun
{
    public class SmoothFollowObject : MonoBehaviour
    {
        public Transform FollowTarget;

        [Range(0, 1)] public float PosDamp;

        [Range(0, 1)] public float RotDamp;

        void OnEnable()
        {
            // ゲーム開始時に、追従するオブジェクトと同じ座標に配置する
            var selfTransform = transform;
            selfTransform.position = FollowTarget.position;
            selfTransform.rotation = FollowTarget.rotation;
        }

        void Update()
        {
            // 追従するオブジェクトに近づくと、補間が弱くなる
            var selfTransform = transform;
            selfTransform.position = Vector3.Lerp(selfTransform.position, FollowTarget.position, PosDamp);
            selfTransform.rotation = Quaternion.Lerp(selfTransform.rotation, FollowTarget.rotation, RotDamp);
        }
    }
}