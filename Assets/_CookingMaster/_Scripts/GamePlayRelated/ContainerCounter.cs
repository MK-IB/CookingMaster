using System;
using _CookingMaster._Scripts.ControllerRelated;
using UnityEngine;

namespace _CookingMaster._Scripts.GamePlayRelated
{
    public class ContainerCounter : BaseCounter
    {
        public event EventHandler OnPlayerGrabbedObject;
        [SerializeField] private KitchenObjectSO kitchenObjectSo;

        public override void Interact(PlayerController player)
        {
            if (!player.HasKitchenObject())
            {
                KitchenObject.SpawnKitchenObject(kitchenObjectSo, player);
                OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}