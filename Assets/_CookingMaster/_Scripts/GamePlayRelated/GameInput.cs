using UnityEngine;

namespace _CookingMaster._Scripts.GamePlayRelated
{
    public class GameInput : MonoBehaviour
    {
        private PlayerInputActions playerInputActions;

        private void Start()
        {
            playerInputActions = new PlayerInputActions();
            playerInputActions.Enable();
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