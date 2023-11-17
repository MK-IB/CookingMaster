using System;
using _CookingMaster._Scripts.ControllerRelated;
using UnityEngine;

namespace _CookingMaster._Scripts.GamePlayRelated
{
    public class VegCounter : MonoBehaviour, ICounterBase
    {
        [SerializeField] private CounterVisual counterVisual;
        [SerializeField] private KitchenObjectSO kitchenObjectSO;

        public void Interact(PlayerController playerController)
        {
            if (!playerController.HasKitchenObject())
            {
                //spawns the contained veg
                //KitchenObject.SpawnKitchenObject(kitchenObjectSO);
                playerController.SpawnKitchenObjects(kitchenObjectSO);
                //play the counter open and close animation on VegCounterVisual.cs
                counterVisual.VegCounterOnPlayerInteracted();
            }
        }

        public void ShowSelectedCounterVisual(bool state)
        {
            counterVisual.ShowSelectedCounterVisual(state);
        }
    }
}