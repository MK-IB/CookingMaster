using System;
using System.Collections;
using System.Collections.Generic;
using _CookingMaster._Scripts.GamePlayRelated;
using UnityEngine;

namespace _CookingMaster._Scripts.GamePlayRelated
{
    public class CounterVisual : MonoBehaviour
    {
        private const string OPEN_CLOSE = "OpenClose";
        [SerializeField] private GameObject selectedCounterVisual;

        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        void Start()
        {
            
        }

        public void VegCounterOnPlayerInteracted()
        {
            _animator.SetTrigger(OPEN_CLOSE);
        }
        public void ShowSelectedCounterVisual(bool state)
        {
            selectedCounterVisual.SetActive(state);
        }
    }
}