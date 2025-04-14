using UnityEngine;
using UnityEngine.InputSystem;

namespace App.Samples.Golf
{
    /// <summary>
    /// インゲームシーンのマネージャー
    /// </summary>
    public class SampleInGameManager : MonoBehaviour
    {
        [SerializeField] InputActionAsset actionAsset;
        [SerializeField] GameObject menu;

        InputAction primaryButtonR, primaryButtonL;

        void Start()
        {
            var actionMap = actionAsset.FindActionMap("Test");
            primaryButtonR = actionMap.FindAction("XR_PrimaryButtonR", throwIfNotFound: true);
            primaryButtonL = actionMap.FindAction("XR_PrimaryButtonL", throwIfNotFound: true);
        }

        void Update()
        {
            if (primaryButtonR.WasPressedThisFrame() || primaryButtonL.WasPressedThisFrame())
            {
                // メニューの表示・非表示を切り替える
                menu.SetActive(!menu.activeSelf);
            }
        }
    }
}