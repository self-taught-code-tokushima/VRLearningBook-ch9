using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

namespace App.Samples.Gun
{
    public class SimpleJump : MonoBehaviour
    {
        public float JumpHeight = 1.0f;
        public float GroundedDistance = 0.2f;
        public float GroundedSpereRadius = 0.01f;
        public CharacterController Controller;
        public NearFarInteractor InteractorR;
        public NearFarInteractor InteractorL;
        public LayerMask GroundLayer;
        Vector3 movement;
        bool jumpBuffer;
        public float JumpBufferSecLimit = 0.1f;
        float jumpBufferSec;

        bool grabRadder;

        // Project SettingsのPhysicsから重力/Gravityを調整可能
        float gravity = Physics.gravity.y * -1.0f;

        // 接地しているかどうかを判定する
        bool IsGrounded()
        {
            Vector3 groundedSpherePos = new Vector3(transform.position.x, transform.position.y - GroundedDistance, transform.position.z);
            return Physics.CheckSphere(groundedSpherePos, GroundedSpereRadius, GroundLayer);
        }

        void Jump()
        {
            movement.y = Mathf.Sqrt(JumpHeight * gravity);
            Debug.Log("Jump Success");
        }

        void Update()
        {
            // はしごの処理と干渉するので、
            // はしごを掴んでいたら以下の処理を無効化する

            if (grabRadder)
            {
                return;
            }

            // hasSelectionはinteractorが何かを持っているかどうかを判定する
            // InputManagerLRでボタン押下を検知する

            //右手の処理
            if (InteractorR.hasSelection == false && InputManagerLR.PrimaryButtonR_OnPress() && IsGrounded())
            {
                Jump();
                jumpBuffer = true;
            }

            //左手の処理
            if (InteractorL.hasSelection == false && InputManagerLR.PrimaryButtonL_OnPress() && IsGrounded())
            {
                Jump();
                jumpBuffer = true;
            }

            if (jumpBuffer)
            {
                jumpBufferSec += Time.deltaTime;
                if (jumpBufferSec > JumpBufferSecLimit)
                {
                    jumpBuffer = false;
                    jumpBufferSec = 0.0f;
                }
            }

            // 地面判定のないオブジェクトでも下に重なっていたら、重力判定を無視する分岐を入れる
            // 判定同士が食いあってしまう
            if (IsGrounded() && jumpBuffer == false)
            {
                movement.y = 0;
                return;
            }
            else
            {
                movement.y -= gravity * Time.deltaTime;
                Controller.Move(movement * Time.deltaTime);
            }
        }

        public void SetGrabRadder(bool grab)
        {
            grabRadder = grab;
        }

        public bool GetGrabRadder()
        {
            return grabRadder;
        }
    }
}