using UnityEngine;
using UnityEngine.InputSystem;

namespace App.Samples.Gun
{
    public class InputManagerLR : MonoBehaviour
    {
        static InputManagerLR instance;

        // あとでInspectorのActionAsset欄にInput Actionを代入する
        [SerializeField] InputActionAsset actionAsset;

        InputActionMap actionMap;
        InputAction primaryButtonR;
        InputAction primaryButtonL;
        InputAction secondaryButtonR;
        InputAction secondaryButtonL;
        InputAction menuButton;
        InputAction thumbstickR;
        InputAction thumbstickL;

        void Awake()
        {
            instance = this;
            // InputActionマネージャーをシーンから破棄しないようにする
            DontDestroyOnLoad(gameObject);
            // Action Mapsの名前を入れる
            actionMap = actionAsset.FindActionMap("Test");
            // Actionsの名前をすべて入れる
            primaryButtonR = actionMap.FindAction("XR_PrimaryButtonR", throwIfNotFound: true);
            primaryButtonL = actionMap.FindAction("XR_PrimaryButtonL", throwIfNotFound: true);
            secondaryButtonR = actionMap.FindAction("XR_SecondaryButtonR", throwIfNotFound: true);
            secondaryButtonL = actionMap.FindAction("XR_SecondaryButtonL", throwIfNotFound: true);
            menuButton = actionMap.FindAction("XR_MenuButton", throwIfNotFound: true);
            thumbstickR = actionMap.FindAction("XR_ThumbstickR", throwIfNotFound: true);
            thumbstickL = actionMap.FindAction("XR_ThumbstickL", throwIfNotFound: true);
        }

        void OnEnable()
        {
            actionMap?.Enable();
        }

        void OnDisable()
        {
            actionMap?.Disable();
        }

        // PrimaryButtonR

        public static bool PrimaryButtonR()
        {
            return instance.primaryButtonR.IsPressed();
        }

        public static bool PrimaryButtonR_OnPress()
        {
            return instance.primaryButtonR.WasPressedThisFrame();
        }

        public static bool PrimaryButtonR_OnRelease()
        {
            return instance.primaryButtonR.WasReleasedThisFrame();
        }

        // SecondaryButtonR

        public static bool SecondaryButtonR()
        {
            return instance.secondaryButtonR.IsPressed();
        }

        public static bool SecondaryButtonR_OnPress()
        {
            return instance.secondaryButtonR.WasPressedThisFrame();
        }

        public static bool SecondaryButtonR_OnRelease()
        {
            return instance.secondaryButtonL.WasReleasedThisFrame();
        }

        // PrimaryButtonL

        public static bool PrimaryButtonL()
        {
            return instance.primaryButtonL.IsPressed();
        }

        public static bool PrimaryButtonL_OnPress()
        {
            return instance.primaryButtonL.WasPressedThisFrame();
        }

        public static bool PrimaryButtonL_OnRelease()
        {
            return instance.primaryButtonL.WasReleasedThisFrame();
        }

        // SecondaryButtonL

        public static bool SecondaryButtonL()
        {
            return instance.secondaryButtonL.IsPressed();
        }

        public static bool SecondaryButtonL_OnPress()
        {
            return instance.secondaryButtonL.WasPressedThisFrame();
        }

        public static bool SecondaryButtonL_OnRelease()
        {
            return instance.secondaryButtonL.WasReleasedThisFrame();
        }

        // MenuButton

        public static bool MenuButton()
        {
            return instance.menuButton.IsPressed();
        }

        public static bool MenuButton_OnPress()
        {
            return instance.menuButton.WasPressedThisFrame();
        }

        public static bool MenuButton_OnRelease()
        {
            return instance.menuButton.WasReleasedThisFrame();
        }

        // ThumbStickR

        public static bool ThumbstickR()
        {
            return instance.thumbstickR.IsPressed();
        }

        public static bool ThumbstickR_OnPress()
        {
            return instance.thumbstickR.WasPressedThisFrame();
        }

        public static bool ThumbstickR_OnRelease()
        {
            return instance.thumbstickR.WasReleasedThisFrame();
        }

        // ThumbStickL

        public static bool ThumbstickL()
        {
            return instance.thumbstickL.IsPressed();
        }

        public static bool ThumbstickL_OnPress()
        {
            return instance.thumbstickL.WasPressedThisFrame();
        }

        public static bool ThumbstickL_OnRelease()
        {
            return instance.thumbstickL.WasReleasedThisFrame();
        }
    }
}