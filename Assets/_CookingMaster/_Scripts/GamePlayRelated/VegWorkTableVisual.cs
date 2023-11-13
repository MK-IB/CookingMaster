using System;
using System.Collections;
using System.Collections.Generic;
using _CookingMaster._Scripts.GamePlayRelated;
using UnityEngine;

namespace _CookingMaster._Scripts.GamePlayRelated
{
    public class VegWorkTableVisual : MonoBehaviour
    {
        private const string OPEN_CLOSE = "OpenClose";
        [SerializeField] private VegWorktable vegWorktable;

        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        void Start()
        {
            vegWorktable.OnPlayerGrabbedObject += VegWorktableOnOnPlayerGrabbedObject;
        }

        private void VegWorktableOnOnPlayerGrabbedObject(object sender, EventArgs e)
        {
            _animator.SetTrigger(OPEN_CLOSE);
        }
    }
}