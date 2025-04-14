using UnityEngine;

namespace App.Samples.Golf
{
    public class SamplePauseHUD : MonoBehaviour
    {
        [SerializeField] Transform target;
        [SerializeField] float distance;
        [SerializeField] float height = 1.5f;
        [SerializeField] float followMoveSpeed = 2f;
        [SerializeField] SampleScoreItem[] scoreItems;

        int[] scores = { 0, 0, 0 };
        bool[] isCleared = { false, false, false };

        void Start()
        {
            UpdateScore(0);
        }

        public void AddScore(int stageIndex)
        {
            scores[stageIndex] += 1;
            UpdateScore(stageIndex);
        }

        public void Clear(int stageIndex)
        {
            isCleared[stageIndex] = true;
            UpdateScore(stageIndex);
        }

        void UpdateScore(int stageIndex)
        {
            for (int i = 0; i < scoreItems.Length; i++)
            {
                scoreItems[i].UpdateScore(scores[i], isCleared[i], i == stageIndex);
            }
        }

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