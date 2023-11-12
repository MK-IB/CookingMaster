using UnityEngine;

namespace _CookingMaster._Scripts.GamePlayRelated
{
    public interface IKitchenObjectParent
    {
        Transform GetKitchenObjectFollowTransform();
        void SetKitchenObject(KitchenObject kitchenObject);
        KitchenObject GetKitchenObject();
        void ClearKitchenObject();
        bool HasKitchenObject();
    }
}