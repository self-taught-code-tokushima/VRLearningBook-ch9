using UnityEngine;
using UnityEngine.InputSystem;

namespace Lectures.Ch03.Sample
{
    public class SampleGamePlayManager : MonoBehaviour
    {
        void Start()
        {
        }

        void Update()
        {
            // マウスの左ボタンが押された時
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.transform.position = new Vector3(0, 8, 0);
                cube.AddComponent<Rigidbody>();
            }
        }
    }
}