using _CookingMaster._Scripts.ControllerRelated;
using _CookingMaster._Scripts.GamePlayRelated;
using UnityEngine;

public class ClearWorkTable : WorkTableBase
{
    [SerializeField] private KitchenObjectSO kitchenObjectSo;

    public override void Interact(PlayerController player)
    {
        if (!HasKitchenObject())
        {
            if(player.HasKitchenObject())
            {
              player.GetKitchenObject().SetKitchenObjectParent(this);  
            }
        }
        else
        {
            if (player.HasKitchenObject())
            {
                
            }else
                GetKitchenObject().SetKitchenObjectParent(player);
        }
    }
}
