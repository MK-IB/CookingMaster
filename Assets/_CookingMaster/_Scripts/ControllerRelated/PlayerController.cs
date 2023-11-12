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
        private BaseCounter _selectedCounter;
        private KitchenObject _kitchenObject;
        /*private BaseCounter _selectedCounter;
        private KitchenObject _kitchenObject;*/
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
            transform.position += moveDir * moveDistance;
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