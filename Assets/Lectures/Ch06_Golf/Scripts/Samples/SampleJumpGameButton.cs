using UnityEngine;

namespace Lectures.Ch06.Sample
{
    public class SampleJumpGameButton : MonoBehaviour
    {
        public void GoToGame()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Ch06_Example_2_Game_DynamicImpulse");
        }
    }
}