using System.Collections.Generic;
using _CookingMaster._Scripts.ControllerRelated;
using UnityEngine;

namespace _CookingMaster._Scripts.GamePlayRelated
{
    public class PlateCounter : MonoBehaviour, ICounterBase
    {
        [SerializeField] private Transform kitchenObjectHoldPoint;
        [SerializeField] private CounterVisual counterVisual;
        
        private int _koHeldCounter;
        public void Interact(PlayerController playerController)
        {
            Queue<KitchenObject> playerPickedKOs = playerController.PickedUpKitchenObjects;
            if(playerPickedKOs.Count > 0 && kitchenObjectHoldPoint.childCount == 0)
            {
                if (kitchenObjectHoldPoint.childCount > 0) return;
                KitchenObject playerHeldKitchenObject = playerPickedKOs.Dequeue();
                Transform kitchenObjectTransform = playerHeldKitchenObject.transform;
                kitchenObjectTransform.parent = kitchenObjectHoldPoint;
                kitchenObjectTransform.localPosition = Vector3.zero;
                playerController.UpdatePlayerHoldPositions();
                return;
            }
            if (kitchenObjectHoldPoint.childCount > 0 && playerPickedKOs.Count >= 0)
            {
                if (playerPickedKOs.Count >= playerController.HoldCapacity) return;
                KitchenObject kitchenObject = kitchenObjectHoldPoint.GetChild(0).GetComponent<KitchenObject>();
                playerController.SpawnKitchenObjects(kitchenObject.GetKitchenObjectSO());
                kitchenObject.transform.parent = null;
                kitchenObject.gameObject.SetActive(false);
            }
        }
        public void ShowSelectedCounterVisual(bool b)
        {
            counterVisual.ShowSelectedCounterVisual(b);
        }
    }
}
