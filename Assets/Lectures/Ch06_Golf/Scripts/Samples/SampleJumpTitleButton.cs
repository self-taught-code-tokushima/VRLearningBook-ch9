using UnityEngine;

namespace Lectures.Ch06.Sample
{
    public class SampleJumpTitleButton : MonoBehaviour
    {
        public void GoToSampleTitle()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Ch06_Example_3_Title");
        }

        public void GoToTaskitle()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Ch06_Task_Title");
        }
    }
}