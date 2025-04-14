using UnityEngine;
using UnityEngine.SceneManagement;

namespace Lectures.Ch06.Sample
{
    /// <summary>
    /// ゴールの当たり判定を行うクラス
    /// </summary>
    public class SampleGoal : MonoBehaviour
    {
        [SerializeField] GameObject resultUI;

        void Start()
        {
            resultUI.SetActive(false);
        }

        void OnCollisionEnter(Collision other)
        {
            if (!other.gameObject.CompareTag("Ball"))
            {
                return;
            }

            Debug.Log("ステージクリア");
            resultUI.SetActive(true);
        }

        public void GoToTitle()
        {
            SceneManager.LoadScene("Ch07_Example_3_Title");
        }
    }
}