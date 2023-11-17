using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _CookingMaster._Scripts.GamePlayRelated
{
    public class GameInput : MonoBehaviour
    {
        private PlayerInputActions playerInputActions;
        public event EventHandler OnInteractActionA;
        public event EventHandler OnInteractActionB;
        public event EventHandler OnInteractAlternateAction;

        private void Start()
        {
            playerInputActions = new PlayerInputActions();
            playerInputActions.Enable();
            playerInputActions.Player.InteractA.performed += InteractOnperformedA;
            playerInputActions.Player.InteractB.performed += InteractOnperformedB;
            //playerInputActions.Player.InteractAlternate.performed += InteractAlternate_Performed;
        }
        private void InteractAlternate_Performed(InputAction.CallbackContext obj)
        {
            OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
        }
        private void InteractOnperformedA(InputAction.CallbackContext obj)
        {
            OnInteractActionA?.Invoke(this, EventArgs.Empty);
        }
        private void InteractOnperformedB(InputAction.CallbackContext obj)
        {
            OnInteractActionB?.Invoke(this, EventArgs.Empty);
        }

        public Vector2 GetMoveDirectionPlayerA()
        {
            Vector2 inputVector = playerInputActions.Player.MoveA.ReadValue<Vector2>();
            inputVector = inputVector.normalized;
            return inputVector;
        }
        public Vector2 GetMoveDirectionPlayerB()
        {
            Vector2 inputVector = playerInputActions.Player.MoveB.ReadValue<Vector2>();
            inputVector = inputVector.normalized;
            return inputVector;
        }
    }
}