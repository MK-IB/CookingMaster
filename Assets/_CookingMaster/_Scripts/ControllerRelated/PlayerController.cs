using System;
using System.Collections;
using System.Collections.Generic;
using _CookingMaster._Scripts.GamePlayRelated;
using UnityEngine;

namespace _CookingMaster._Scripts.ControllerRelated
{
    public class PlayerController : MonoBehaviour, IKitchenObjectParent
    {
        enum PlayerType
        {
            PlayerA,
            PlayerB
        }

        [SerializeField] private GameInput _gameInput;
        [SerializeField] private PlayerType _playerType;
        
        private float rotationSpeed = 10f;
        [SerializeField] private float moveSpeed = 7f;
        [SerializeField] private LayerMask countersLayerMask;
        [SerializeField] private Transform kitchenObjectHoldPoint;

        private bool _isWalking;
        private Vector3 _lastInteractDir;
        private WorkTableBase _selectedWorkTable;
        private KitchenObject _kitchenObject;
        /*private BaseCounter _selectedCounter;
        private KitchenObject _kitchenObject;*/
        private void Start()
        {
            _gameInput.OnInteractAction += GameInputOnInteractAction;
        }
        private void GameInputOnInteractAction(object sender, EventArgs e)
        {
            if(_selectedWorkTable != null)
                _selectedWorkTable.Interact(this);
        }
        private void Update()
        {
            if(_playerType == PlayerType.PlayerA)
            {
                Move(_gameInput.GetMoveDirectionPlayerA());
                Interaction(_gameInput.GetMoveDirectionPlayerA());
            }
            else
            {
                Move(_gameInput.GetMoveDirectionPlayerB());
                Interaction(_gameInput.GetMoveDirectionPlayerB());
            }
        }

        void Move(Vector2 inputVector)
        {
            Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);
            float moveDistance = moveSpeed * Time.deltaTime;
            float playerRadius = 1.7f;
            float playerHeight = 2f;
            transform.position += moveDir * moveDistance;
            bool canMove = !Physics.CapsuleCast(transform.position, transform.position, playerRadius, moveDir, moveDistance);
            if (!canMove)
            {
                //attempt only x movement
                Vector3 moveDirX = new Vector3(moveDir.x, 0,0).normalized;
                canMove = moveDir.x != 0 && !Physics.CapsuleCast(transform.position, transform.position, playerRadius, moveDirX, moveDistance);
                if (canMove) {
                    moveDir = moveDirX;
                }
                else {
                    //attempt only z movement
                    Vector3 moveDirZ = new Vector3(0,0,moveDir.z).normalized;
                    canMove = moveDir.z != 0 && !Physics.CapsuleCast(transform.position, transform.position, playerRadius, moveDirZ, moveDistance);
                    if (canMove) {
                        moveDir = moveDirZ;
                    }
                    else {
                        //can't move any direction
                    }
                }
            }
            if(canMove)
            {
                transform.position += moveDir * moveDistance;
            }
            transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotationSpeed);
            _isWalking = moveDir != Vector3.zero;
        }

        void Interaction(Vector2 interactionDir)
        {
            
        }

        public Transform GetKitchenObjectFollowTransform()
        {
            return kitchenObjectHoldPoint;
        }

        public void SetKitchenObject(KitchenObject kitchenObject)
        {
            _kitchenObject = kitchenObject;
        }

        public KitchenObject GetKitchenObject()
        {
            return _kitchenObject;
        }

        public void ClearKitchenObject()
        {
            _kitchenObject = null;
        }

        public bool HasKitchenObject()
        {
            return _kitchenObject != null;
        }
    }
}