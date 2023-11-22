using System;
using _CookingMaster._Scripts.ControllerRelated;
using DG.Tweening;
using UnityEngine;

namespace _CookingMaster._Scripts.ElementRelated
{
    public class GameOver : MonoBehaviour
    {
        public static GameOver instance;
        [SerializeField] private RectTransform playerA, playerB, rank1, rank2;

        private void Awake()
        {
            instance = this;
        }

        public void ShowRank(PlayerController playerController)
        {
            if (playerController.PlayersType == PlayerController.PlayerType.PlayerA)
            {
                playerA.DOMove(rank1.position, 1.5f);
                playerB.DOMove(rank2.position, 1.5f);
            }
            else
            {
                playerB.DOMove(rank1.position, 1.5f);
                playerA.DOMove(rank2.position, 1.5f);
            }
        }
    }
}
