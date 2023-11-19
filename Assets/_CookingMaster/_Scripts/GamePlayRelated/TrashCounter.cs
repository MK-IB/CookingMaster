using _CookingMaster._Scripts.ControllerRelated;
using UnityEngine;

namespace _CookingMaster._Scripts.GamePlayRelated
{
    public class TrashCounter : MonoBehaviour, ICounterBase
    {
        [SerializeField] private CounterVisual counterVisual;
        public void ShowSelectedCounterVisual(bool b)
        {
            counterVisual.ShowSelectedCounterVisual(b);
        }

        public void Interact(PlayerController playerController)
        {
            playerController.PutKitchenObjectIntoTrash();
            counterVisual.AnimateOnPlayerInteracted();
        }
    }
}
