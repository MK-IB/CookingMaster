using System.Collections;
using System.Collections.Generic;
using _CookingMaster._Scripts.ControllerRelated;
using _CookingMaster._Scripts.GamePlayRelated;
using UnityEngine;

namespace _CookingMaster._Scripts.GamePlayRelated
{
    public class ChoppingCounter : MonoBehaviour, ICounterBase
    {
        [SerializeField] private CounterVisual counterVisual;
        [SerializeField] private Transform kitchenObjectHoldPoint;
        [SerializeField] private List<Transform> kitchenObjectChoppedPointsList;
        public List<KitchenObject> _heldKitchenObjectsList = new List<KitchenObject>();
        
        /// comparing with the List type KitchenObject was not working, so tried taking string combination
        public List<string> _saladCombination = new List<string>();
        private List<Transform> _choppedItemsList = new List<Transform>();

        [SerializeField] private Animator knifeAnimator;

        private int _koHeldCounter;
        public void Interact(PlayerController playerController)
        {
            //get the player's first kitchen object
            Queue<KitchenObject> playerPickedKOs = playerController.PickedUpKitchenObjects;
            //if player has vegetables, chop it
            if(playerPickedKOs.Count > 0)
            {
                if (_koHeldCounter >= kitchenObjectChoppedPointsList.Count) return;
                KitchenObject playerHeldKitchenObject = playerPickedKOs.Dequeue();
                Transform kitchenObjectTransform = playerHeldKitchenObject.transform;
                kitchenObjectTransform.parent = kitchenObjectHoldPoint;
                kitchenObjectTransform.localPosition = Vector3.zero;
                _heldKitchenObjectsList.Add(playerHeldKitchenObject);
                _saladCombination.Add(playerHeldKitchenObject.GetKitchenObjectSO().objectName);
                playerController.UpdatePlayerHoldPositions();
                StartCoroutine(ChopKitchenObject(playerController));
                playerController._actionState = PlayerController.PlayerActionState.Chopping;
                return;
            }
            //if player doesn't have veg. and chopping board has chopped items, give it to the player
            if (_heldKitchenObjectsList.Count >= 1 && playerPickedKOs.Count == 0)
            {
                //playerController.saladCombinationKOs = _heldKitchenObjectsList;
                List<Transform> playerPickupPoints = playerController.KitchenObjectHoldPointsList;
                List<Transform> playerHeldSaladCombo = playerController.heldSaladComboList;
                Debug.Log("Chopped items = " + _choppedItemsList.Count);
                for (int i = 0; i < _heldKitchenObjectsList.Count; i++)
                {
                    Transform kitchenObject = _choppedItemsList[i];
                    kitchenObject.parent = playerPickupPoints[i];
                    kitchenObject.localPosition = Vector3.zero;
                    playerController.SaladCombinationNames.Add(_heldKitchenObjectsList[i].GetKitchenObjectSO().objectName);
                    playerHeldSaladCombo.Add(_choppedItemsList[i]);
                }
                _heldKitchenObjectsList.Clear();
                _choppedItemsList.Clear();
                _koHeldCounter = 0;
                _saladCombination.Clear();
            }
        }

        IEnumerator ChopKitchenObject(PlayerController playerController)
        {
            knifeAnimator.SetTrigger("Cut");
            yield return new WaitForSeconds(0.75f);

            KitchenObject heldKitchenObject = _heldKitchenObjectsList[_koHeldCounter]; 
            Transform choppedKitchenKO = heldKitchenObject.ChoppedKo.transform;
            Transform choppedKoHoldPoint = kitchenObjectChoppedPointsList[_koHeldCounter];
            Transform choppedKO = Instantiate(choppedKitchenKO.gameObject, choppedKoHoldPoint.position, Quaternion.identity).transform;
            choppedKO.parent = choppedKoHoldPoint;
            choppedKO.localPosition = Vector3.zero;
            _choppedItemsList.Add(choppedKO);
            heldKitchenObject.gameObject.SetActive(false);
            while (Vector3.Distance(choppedKitchenKO.position, choppedKoHoldPoint.position) <= 0.1f)
            {
                choppedKitchenKO.position = Vector3.Lerp(choppedKitchenKO.position, choppedKoHoldPoint.position, Time.deltaTime * 1.5f);
            }

            yield return new WaitForSeconds(1);
            playerController._actionState = PlayerController.PlayerActionState.Moving;
            _koHeldCounter++;
        }

        public void ShowSelectedCounterVisual(bool b)
        {
            counterVisual.ShowSelectedCounterVisual(b);
        }
    }
}