using System;
using System.Collections;
using _CookingMaster._Scripts.ControllerRelated;
using UnityEngine;
using UnityEngine.UI;

namespace _CookingMaster._Scripts.ElementRelated
{
    public class CustomerWaitingSliderElement : MonoBehaviour
    {
        private Slider _waitingSlider;
        public float duration;
        [HideInInspector] public bool waitingStarted;

        [SerializeField] private CustomerElement _customerElement;
        [SerializeField] private float decreasingSpeed;

        public float DecreasingSpeed
        {
            get => decreasingSpeed;
            set => decreasingSpeed = value;
        }

        void Start()
        {
            _waitingSlider = GetComponent<Slider>();
        }

        private void Update()
        {
            if (waitingStarted)
            {
                StartWaiting();
            }
        }

        public void StartWaiting()
        {
            if(!waitingStarted) return;
            StartCoroutine(DecreaseSliderValue());
            waitingStarted = false;
        }
        IEnumerator DecreaseSliderValue()
        {
            float currentTime = 0f;
            float startValue = _waitingSlider.value;
            float endValue = 0f;

            while (currentTime < duration)
            {
                currentTime += Time.deltaTime * decreasingSpeed;
                _waitingSlider.value = Mathf.Lerp(startValue, endValue, currentTime / duration);
                yield return null;
            }
            _waitingSlider.value = endValue;
            
            //if slider value goes to 0/wait time ends- customer exits angry
            if (_waitingSlider.value <= 0)
            {
                _customerElement.ExitDisappointed();
                
                //decrease score of both the players
                PlayerController[] players = FindObjectsOfType<PlayerController>();
                for (int i = 0; i < players.Length; i++)
                {
                    players[i].UpdatePlayerScores(-10);
                }
            }
        }
    }
}