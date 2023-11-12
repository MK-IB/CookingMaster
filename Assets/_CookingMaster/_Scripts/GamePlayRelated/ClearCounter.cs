using _CookingMaster._Scripts.ControllerRelated;
using UnityEngine;

public class ClearCounter : BaseCounter
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
