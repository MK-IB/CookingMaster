using System.Collections;
using System.Collections.Generic;
using _CookingMaster._Scripts.ControllerRelated;
using _CookingMaster._Scripts.GamePlayRelated;
using UnityEngine;

namespace _CookingMaster._Scripts.GamePlayRelated
{
    public class CuttingWorkTable : WorkTableBase
    {
        [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;

        public override void Interact(PlayerController player)
        {
            if (!HasKitchenObject())
            {
                if (player.HasKitchenObject())
                {
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                }
                else
                {

                }
            }
            else
            {
                if (player.HasKitchenObject())
                {

                }
                else
                    GetKitchenObject().SetKitchenObjectParent(player);
            }
        }

        public override void InteractAlternate(PlayerController player)
        {
            if (HasKitchenObject())
            {
                KitchenObjectSO outputKitchenObjectSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());
                GetKitchenObject().DestroySelf();
                KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this);
            }
        }

        private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitcheObjectSO)
        {
            foreach (CuttingRecipeSO cuttingRecipeSo in cuttingRecipeSOArray)
            {
                if (cuttingRecipeSo.input == inputKitcheObjectSO)
                    return cuttingRecipeSo.output;
            }

            return null;
        }
    }
}