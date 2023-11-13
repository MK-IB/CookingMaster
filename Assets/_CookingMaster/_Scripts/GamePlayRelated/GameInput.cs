using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _CookingMaster._Scripts.GamePlayRelated
{
    public class GameInput : MonoBehaviour
    {
        private PlayerInputActions playerInputActions;
        public event EventHandler OnInteractAction;
        public event EventHandler OnInteractAlternateAction;

        private void Start()
        {
            playerInputActions = new PlayerInputActions();
            playerInputActions.Enable();
            playerInputActions.Player.Interact.performed += InteractOnperformed;
            //playerInputActions.Player.InteractAlternate.performed += InteractAlternate_Performed;
        }
        private void InteractAlternate_Performed(InputAction.CallbackContext obj)
        {
            OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
        }
        private void InteractOnperformed(InputAction.CallbackContext obj)
        {
            OnInteractAction?.Invoke(this, EventArgs.Empty);
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