using _CookingMaster._Scripts.ControllerRelated;
using _CookingMaster._Scripts.GamePlayRelated;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    [SerializeField] protected Transform counterTopPoint;

    protected KitchenObject _kitchenObject;
    
    public virtual void Interact(PlayerController player)
    {
        Debug.LogError("BaseCounter.Interact();");
    }
    public virtual void InteractAlternate(PlayerController player)
    {
        Debug.LogError("BaseCounter.InteractAlternate();");
    }
    public Transform GetKitchenObjectFollowTransform()
    {
        return counterTopPoint;
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
    public bool HasKitchenObject()
    {
        return _kitchenObject != null;
    }
}
