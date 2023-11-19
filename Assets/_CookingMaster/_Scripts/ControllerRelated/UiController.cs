using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace _CookingMaster._Scripts.ControllerRelated
{
    public class UiController : MonoBehaviour
    {
        public static UiController instance;
        [SerializeField] private RectTransform vegHolderPanelA, vegHolderPanelB;
        [SerializeField] private List<Image> holderPanelImageListA, holderPanelImageListB;
        [SerializeField] private Transform playerA, playerB;
        [SerializeField] private Vector3 panelFollowOffset;
        [SerializeField] private Sprite defaultSprite;
        private Camera _camera;

        private int _holderPanelImageCounterA, _holderPanelImageCounterB;
        private Queue<Sprite> _vegHeldQueueA = new Queue<Sprite>();
        private Queue<Sprite> _vegHeldQueueB = new Queue<Sprite>();

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            Vector3 screenPointA = _camera.WorldToScreenPoint(playerA.position);
            Vector3 screenPointB = _camera.WorldToScreenPoint(playerB.position);
            vegHolderPanelA.position = screenPointA + panelFollowOffset;
            vegHolderPanelB.position = screenPointB + panelFollowOffset;
        }

        public void AddVegHolderPanelA(Sprite sprite)
        {
            _vegHeldQueueA.Enqueue(sprite);
            UpdateHoldPanelA();
        }

        public void AddVegHolderPanelB(Sprite sprite)
        {
            _vegHeldQueueB.Enqueue(sprite);
            UpdateHoldPanelB();
        }
        
        //remove the vegetable sprite from HUD for player A
        public void RemoveVegHolderPanelA()
        {
            _vegHeldQueueA.Dequeue();
            UpdateHoldPanelA();
        }

        //remove the vegetable sprite from HUD for player B
        public void RemoveVegHolderPanelB()
        {
            _vegHeldQueueB.Dequeue();
            UpdateHoldPanelB();
        }

        public void UpdateHoldPanelA()
        {
            //first remove the sprite from the sprite list
            for (int j = 0; j < holderPanelImageListA.Count; j++)
            {
                holderPanelImageListA[j].sprite = defaultSprite;
            }
            
            //then assign the updates sprites
            int i = 0;
            foreach (var vegSprite in _vegHeldQueueA)
            {
                if (i < holderPanelImageListA.Count)
                {
                    holderPanelImageListA[i].sprite = vegSprite;
                    i++;
                }
            }
        }
        
        public void UpdateHoldPanelB()
        {
            for (int j = 0; j < holderPanelImageListA.Count; j++)
            {
                holderPanelImageListA[j].sprite = null;
            }
            
            int i = 0;
            foreach (var vegSprite in _vegHeldQueueB)
            {
                if (i < holderPanelImageListB.Count)
                {
                    holderPanelImageListB[i].sprite = vegSprite;
                    i++;
                }
            }
        }
    }
}
