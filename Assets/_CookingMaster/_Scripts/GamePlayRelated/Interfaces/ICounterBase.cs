using _CookingMaster._Scripts.ControllerRelated;
using UnityEngine;

namespace _CookingMaster._Scripts.GamePlayRelated
{
    public interface ICounterBase
    {
        void ShowSelectedCounterVisual(bool b);
        void Interact(PlayerController playerController);
    }
}