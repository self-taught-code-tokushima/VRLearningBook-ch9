using UnityEngine;
using UnityEngine.SceneManagement;

namespace Lectures.Ch06.Sample
{
    public class SampleTitleManager : MonoBehaviour
    {
        public void GoToGame()
        {
            SceneManager.LoadScene("Ch06_Example_4_Game_Complete");
        }
    }
}