using System;
using System.Collections.Generic;
using System.Linq;
using _CookingMaster._Scripts.ControllerRelated;
using _CookingMaster._Scripts.ElementRelated;
using UnityEngine;

namespace _CookingMaster._Scripts.GamePlayRelated
{
    public class ServeCounter : MonoBehaviour, ICounterBase
    {
        //keep reference of both player and customer to serve
        private CustomerElement _customerElement;
        private PlayerController _playerController;
        private List<string> demandedKONames = new List<string>();

        [SerializeField] private List<SpriteRenderer> saladComboSpritesHolder;
        [SerializeField] private GameObject selectedCounterVisual;

        public CustomerElement CustomerElement
        {
            get => _customerElement;
            set => _customerElement = value;
        }

        public PlayerController PlayerController
        {
            get => _playerController;
            set => _playerController = value;
        }

        public void SetDemandedSaladCombo(List<KitchenObject> kitchenObjectSos)
        {
            for (int i = 0; i < kitchenObjectSos.Count; i++)
            {
                KitchenObjectSO kitchenObjectSo = kitchenObjectSos[i].GetKitchenObjectSO();
                saladComboSpritesHolder[i].sprite = kitchenObjectSo.sprite;
                demandedKONames.Add(kitchenObjectSo.objectName);
            }
        }

        public void ResetSaladVisual()
        {
            for (int i = 0; i < saladComboSpritesHolder.Count; i++)
            {
                saladComboSpritesHolder[i].sprite = null;
            }
        }

        public void Interact(PlayerController playerController)
        {
            //check if the salad combo contained by the player is same as demandedeKONames list
            List<string> playerHeldSaladKOs = playerController.SaladCombinationNames;
            HashSet<string> setA = new HashSet<string>(playerHeldSaladKOs);
            HashSet<string> setB = new HashSet<string>(demandedKONames);

            if (playerHeldSaladKOs.Count <= 0) return;
            if (playerHeldSaladKOs.Count != demandedKONames.Count)
            {
                _customerElement.ExitDisappointed();
                //subtract 10 points on disappointing the customer
                playerController.UpdatePlayerScores(-10);
                return;
            }

            if (setA.SetEquals(setB))
            {
               //customer takes the salad and exits
               _customerElement.ExitSatisfied(playerController);
               playerController.UpdatePlayerScores(25);
               //reset the server counter
               ResetSaladVisual();
            }
            else
            {
                _customerElement.ExitDisappointed();
                playerController.UpdatePlayerScores(-10);
            }
        }

        public void ShowSelectedCounterVisual(bool b)
        {
            selectedCounterVisual.SetActive(b);
        }
    }
}