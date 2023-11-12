using System;
using System.Collections;
using System.Collections.Generic;
using _CookingMaster._Scripts.GamePlayRelated;
using UnityEngine;

public class ContainerCounterVisual : MonoBehaviour
{
    private const string OPEN_CLOSE = "OpenClose";
    [SerializeField] private ContainerCounter containerCounter;
    
    private Animator _animator;
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    void Start()
    {
        containerCounter.OnPlayerGrabbedObject += ContainerCounter_OnOnPlayerGrabbedObject;
    }

    private void ContainerCounter_OnOnPlayerGrabbedObject(object sender, EventArgs e)
    {
        _animator.SetTrigger(OPEN_CLOSE);
    }
}
