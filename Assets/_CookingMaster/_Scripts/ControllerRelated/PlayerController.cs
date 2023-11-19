using System;
using System.Collections;
using System.Collections.Generic;
using _CookingMaster._Scripts.GamePlayRelated;
using UnityEngine;

namespace _CookingMaster._Scripts.ControllerRelated
{
    public class PlayerController : MonoBehaviour
    {
        enum PlayerType
        {
            PlayerA,
            PlayerB
        }

        [SerializeField] private GameInput _gameInput;
        [SerializeField] private PlayerType _playerType;
        
        private float rotationSpeed = 30f;
        [SerializeField] private float moveSpeed = 7f;
        [SerializeField] private LayerMask countersLayerMask;
        [SerializeField] private List<Transform> kitchenObjectHoldPointsList;

        //list of kitchen objects the player has picked up (limit = 2) 
        private Queue<KitchenObject> _pickedUpKitchenObjectQueue = new Queue<KitchenObject>();
        private List<string> _saladCombination = new List<string>();

        private bool _isWalking;
        private Vector3 _lastInteractDir;
        private ICounterBase _selectedCounter;
        private KitchenObject _kitchenObject;

        private float _rayDist;
        private int _holdCapacity= 2;
        public Queue<KitchenObject> PickedUpKitchenObjects
        {
            get => _pickedUpKitchenObjectQueue;
            set => _pickedUpKitchenObjectQueue = value;
        }

        public List<string> SaladCombination
        {
            get => _saladCombination;
            set => _saladCombination = value;
        }

        public List<Transform> KitchenObjectHoldPointsList
        {
            get => kitchenObjectHoldPointsList;
            set => kitchenObjectHoldPointsList = value;
        }

        private void Start()
        {
            if(_playerType == PlayerType.PlayerA)
                _gameInput.OnInteractActionA += GameInputOnInteractActionA;
            if(_playerType == PlayerType.PlayerB)
                _gameInput.OnInteractActionB += GameInputOnInteractActionB;
            _rayDist = transform.localScale.x / 2 + 0.1f;
        }
        private void GameInputOnInteractActionA(object sender, EventArgs e)
        {
            if(_selectedCounter != null)
                _selectedCounter.Interact(this);
        }
        private void GameInputOnInteractActionB(object sender, EventArgs e)
        {
            if(_selectedCounter != null)
                _selectedCounter.Interact(this);
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
            /*float playerRadius = 1.7f;
            float playerHeight = 2f;*/
            Debug.DrawRay(transform.position, moveDir * _rayDist, Color.magenta);
            bool canMove = !Physics.Raycast(transform.position, moveDir, _rayDist);
            /*if (!canMove)
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
            }*/
            if(canMove)
            {
                transform.position += moveDir * moveDistance;
                transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotationSpeed);
            }
            _isWalking = moveDir != Vector3.zero;
        }

        void Interaction(Vector2 interactionDir)
        {
            Vector3 moveDir = new Vector3(interactionDir.x, 0, interactionDir.y);
            if (moveDir != Vector3.zero)
            {
                _lastInteractDir = moveDir;
            }
            if (Physics.Raycast(transform.position, _lastInteractDir, out RaycastHit raycastHit, _rayDist))
            {
                if (raycastHit.transform.TryGetComponent(out ICounterBase baseWorkTable))
                {
                    if (baseWorkTable != _selectedCounter)
                    {
                        SetSelectedCounter(baseWorkTable);
                        baseWorkTable.ShowSelectedCounterVisual(true);
                    }
                }else
                {
                    SetSelectedCounter(null);
                }

                if (raycastHit.collider.CompareTag("Finish"))
                {
                    raycastHit.collider.GetComponent<Collider>().enabled = false;
                }
            }else {
                if(_selectedCounter != null) 
                    _selectedCounter.ShowSelectedCounterVisual(false);
                SetSelectedCounter(null);
            }
        }
        void SetSelectedCounter(ICounterBase selectedCounter)
        {
            _selectedCounter = selectedCounter;
            /*OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
            {
                selectedCounter = _selectedCounter
            });*/
        }
        public void SpawnKitchenObjects(KitchenObjectSO kitchenObjectSo)
        {
            //spawning the kitchen objects in players hold position
            if (_pickedUpKitchenObjectQueue.Count >= _holdCapacity) return;
            KitchenObject kitchenObject = Instantiate(kitchenObjectSo.prefab).GetComponent<KitchenObject>();
            _pickedUpKitchenObjectQueue.Enqueue(kitchenObject);
            kitchenObject.transform.parent = kitchenObjectHoldPointsList[_kitchenObjHoldCounter++];
            kitchenObject.transform.localPosition = Vector3.zero;
            
            if(_playerType == PlayerType.PlayerA)
                UiController.instance.AddVegHolderPanelA(kitchenObject.GetKitchenObjectSO().sprite);
            else UiController.instance.AddVegHolderPanelB(kitchenObject.GetKitchenObjectSO().sprite);
        }

        public void UpdatePlayerHoldPositions()
        {
            _kitchenObjHoldCounter--;
            if(_playerType == PlayerType.PlayerA)
                UiController.instance.RemoveVegHolderPanelA();
            else UiController.instance.RemoveVegHolderPanelB();
        }

        private int _kitchenObjHoldCounter;
        public Transform GetKitchenObjectFollowTransform()
        {
            if(_kitchenObjHoldCounter < kitchenObjectHoldPointsList.Count) 
                return kitchenObjectHoldPointsList[_kitchenObjHoldCounter++];
            return null;
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

        public int GetHoldPoints()
        {
            return kitchenObjectHoldPointsList.Count;
        }

        //put the kitchen object into trash
        public void PutKitchenObjectIntoTrash()
        {
            KitchenObject kitchenObject = _pickedUpKitchenObjectQueue.Peek();
            kitchenObject.transform.parent = null;
            kitchenObject.gameObject.SetActive(false);
            //Destroy(kitchenObject.gameObject);
            UpdatePlayerHoldPositions();
            _pickedUpKitchenObjectQueue.Dequeue();
        }

        public bool HasKitchenObject()
        {
            return _kitchenObject != null;
        }
    }
}