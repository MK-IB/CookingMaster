using System.Collections;
using System.Collections.Generic;
using _CookingMaster._Scripts.ControllerRelated;
using _CookingMaster._Scripts.GamePlayRelated;
using UnityEngine;

namespace _CookingMaster._Scripts.GamePlayRelated
{
    public class ChoppingCounter : MonoBehaviour, ICounterBase
    {
        [SerializeField] private CounterVisual counterVisual;
        [SerializeField] private List<Transform> kitchenObjectHoldPointsList;
        [SerializeField] private List<Transform> kitchenObjectChoppedPointsList;
        private List<KitchenObject> _heldKitchenObjectsList = new List<KitchenObject>();

        [SerializeField] private Animator knifeAnimator;

        private int _koHeldCounter;
        public void Interact(PlayerController playerController)
        {
            if (_koHeldCounter >= kitchenObjectHoldPointsList.Count) return;
            //get the player's kitchen objects list
            List<KitchenObject> playerHeldKitchenObjects = playerController.PickedUpKitchenObjects;
            playerHeldKitchenObjects.Reverse();
            Transform kitchenObject = playerHeldKitchenObjects[_koHeldCounter].transform;
            kitchenObject.parent = kitchenObjectHoldPointsList[_koHeldCounter];
            kitchenObject.localPosition = Vector3.zero;
            _heldKitchenObjectsList.Add(playerHeldKitchenObjects[_koHeldCounter]);
            StartCoroutine(ChopKitchenObject());
        }

        IEnumerator ChopKitchenObject()
        {
            knifeAnimator.SetTrigger("Cut");
            yield return new WaitForSeconds(0.75f);

            KitchenObject heldKitchenObject = _heldKitchenObjectsList[_koHeldCounter]; 
            Transform choppedKitchenKO = heldKitchenObject.ChoppedKo.transform;
            Transform choppedKoHoldPoint = kitchenObjectChoppedPointsList[_koHeldCounter];
            Transform choppedKO = Instantiate(choppedKitchenKO.gameObject, choppedKoHoldPoint.position, Quaternion.identity).transform;
            choppedKO.parent = choppedKoHoldPoint;
            choppedKO.localPosition = Vector3.zero;
            heldKitchenObject.gameObject.SetActive(false);
            while (Vector3.Distance(choppedKitchenKO.position, choppedKoHoldPoint.position) <= 0.1f)
            {
                choppedKitchenKO.position = Vector3.Lerp(choppedKitchenKO.position, choppedKoHoldPoint.position, Time.deltaTime * 1.5f);
            }
            _koHeldCounter++;
        }

        public void ShowSelectedCounterVisual(bool b)
        {
            Debug.Log("Selecetd");
            counterVisual.ShowSelectedCounterVisual(b);
        }
    }
}