using _CookingMaster._Scripts.GamePlayRelated;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSo;
    [SerializeField] private GameObject choppedKoPrefab;

    public GameObject ChoppedKo
    {
        get => choppedKoPrefab;
        set => choppedKoPrefab = value;
    }

    public KitchenObjectSO GetKitchenObjectSO()
    {
        return kitchenObjectSo;
    }

    public void SetKitchenObjectParent()
    {
        
        transform.localPosition = Vector3.zero;
    }

    /*public IKitchenObjectParent GetKitchenObjectParent()
    {
        return _kitchenObjectParent;
    }

    public void DestroySelf()
    {
        _kitchenObjectParent.ClearKitchenObject();
        Destroy(gameObject);
    }*/

    public static KitchenObject SpawnKitchenObject(KitchenObjectSO kitchenObjectSo)
    {
        Transform kitchenObjectTransform = Instantiate(kitchenObjectSo.prefab);
        KitchenObject kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
        //kitchenObject.SetKitchenObjectParent(kitchenObjectParent);
        return kitchenObject;
    }
}