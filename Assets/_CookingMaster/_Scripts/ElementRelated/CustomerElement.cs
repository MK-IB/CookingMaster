using System;
using System.Collections;
using System.Collections.Generic;
using _CookingMaster._Scripts.ControllerRelated;
using _CookingMaster._Scripts.GamePlayRelated;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace _CookingMaster._Scripts.ElementRelated
{
    public class CustomerElement : MonoBehaviour
    {
        [SerializeField] private CustomerWaitingSliderElement _sliderElement;
        [SerializeField] private List<Transform> kitchenObjectHoldPointsList;
        [SerializeField] private List<KitchenObject> _kitchenObjects;
        [SerializeField] private List<string> _demandedSaladComboNames = new List<string>();
        private List<KitchenObject> demandedKitchebObjects = new List<KitchenObject>();
        private List<Transform> servedSaladCombo = new List<Transform>();

        [SerializeField] private Transform exitPoint, waitPoint, currentAssignedPoint;
        [SerializeField] private Image reactionImage;
        [SerializeField] private Sprite angryEmoji, happyEmoji;
        [SerializeField] private float approachDelay;

        private NavMeshAgent _navMeshAgent;

        public List<string> DemandedSaladComboNames => _demandedSaladComboNames;
        private bool _canAcceptOrder;

        public bool CanAcceptOrder
        {
            get => _canAcceptOrder;
            set => _canAcceptOrder = value;
        }

        private void OnEnable()
        {
            MainController.GameStateChanged += On_GameStateChanged;
        }

        private void OnDisable()
        {
            MainController.GameStateChanged -= On_GameStateChanged;
        }

        void On_GameStateChanged(GameState newState, GameState oldState)
        {
            if (newState == GameState.LevelStart)
            {
                //customer walks to the waiting counter when game starts
                DOVirtual.DelayedCall(approachDelay, () =>
                {
                    SetCustomerDestination(waitPoint);
                });
            }
        }

        private void Start()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            //Assign 3 vegetables to the customer
            while (_demandedSaladComboNames.Count < 3)
            {
                KitchenObject kitchenObjectSo = _kitchenObjects[Random.Range(0, _kitchenObjects.Count)];
                string vegName = kitchenObjectSo.GetKitchenObjectSO().objectName;
                if (!_demandedSaladComboNames.Contains(vegName))
                {
                    _demandedSaladComboNames.Add(vegName);
                    demandedKitchebObjects.Add(kitchenObjectSo);
                }
            }
        }

        private void SetCustomerDestination(Transform dest)
        {
            _navMeshAgent.SetDestination(dest.position);
            currentAssignedPoint = dest;
        }

        private bool _serveCounterReached;

        private void Update()
        {
            if (currentAssignedPoint == null) return;
            if (!_navMeshAgent.pathPending && _navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
            {
                transform.eulerAngles = Vector3.Slerp(transform.eulerAngles, currentAssignedPoint.localEulerAngles,
                    Time.deltaTime * 5);
            }

            //approach to serve counter and show the demanded salad
            if (_serveCounterReached) return;
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, 1))
            {
                if (hit.transform.TryGetComponent(out ServeCounter serveCounter))
                {
                    Debug.Log("Serve counter encountered..");
                    serveCounter.CustomerElement = this;
                    serveCounter.SetDemandedSaladCombo(demandedKitchebObjects);
                    _serveCounterReached = true;
                    _sliderElement.waitingStarted = true;
                }
            }
        }

        public void ExitDisappointed()
        {
            StartCoroutine(SetEmojiAndExit(angryEmoji));
            //deduct points from both the players
        }

        public void ExitSatisfied(PlayerController playerController)
        {
            StartCoroutine(SetEmojiAndExit(happyEmoji));
            //assign salad to the customer
            List<Transform> playerHeldSaladCombo = playerController.HeldSaladComboList;
            ServeCustomer(playerHeldSaladCombo);
            playerHeldSaladCombo.Clear();
            //add points to the player wo served
        }

        IEnumerator SetEmojiAndExit(Sprite sprite)
        {
            _canAcceptOrder = false;
            reactionImage.gameObject.SetActive(true);
            reactionImage.sprite = sprite;
            Transform reactionImgTransform = reactionImage.transform;
            Vector3 reactionImgScale = reactionImgTransform.localScale;
            //reactionImgTransform.DOScale(reactionImgScale / 2, 0.35f).From().SetEase(Ease.OutBack);

            yield return new WaitForSeconds(2);
            _navMeshAgent.SetDestination(exitPoint.position);
        }

        public void ServeCustomer(List<Transform> playerHeldServedSaladCombo)
        {
            servedSaladCombo = playerHeldServedSaladCombo;
            for (int i = 0; i < servedSaladCombo.Count; i++)
            {
                Transform saladItem = servedSaladCombo[i];
                saladItem.parent = kitchenObjectHoldPointsList[i];
                saladItem.localPosition = Vector3.zero;
            }
        }
    }
}