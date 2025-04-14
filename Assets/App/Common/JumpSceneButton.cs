using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace App.Common
{
    [RequireComponent(typeof(Button))]
    public class JumpSceneButton : MonoBehaviour
    {
        [SerializeField] string sceneName;

        void Awake()
        {
            GetComponent<Button>().onClick
                .AddListener(() => SceneManager.LoadScene(sceneName));
        }
    }
}