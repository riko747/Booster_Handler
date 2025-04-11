using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Source.Animation;
using Source.Data;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

namespace Source.ButtonHandlers
{
    public class RefreshButtonHandler : ButtonHandler
    {
        [SerializeField] private Transform boostersGroupParent;
        
        private readonly List<Transform> _boostersTransforms = new();
        private readonly Random _random = new();
        
        protected override void Start()
        {
           base.Start();

            foreach (Transform boosterInGroup in boostersGroupParent)
            {
                _boostersTransforms.Add(boosterInGroup);
            }
        }
        
        protected override async UniTask OnButtonClicked()
        {
            await HideOldBoosters();
            await ShowNewBoosters();
        }

        private async Task HideOldBoosters()
        {
            uiButton.interactable = false;
            var graphicsToAnimateFadeOut = new List<Graphic>();

            foreach (Transform boosterInGroup in boostersGroupParent)
            {
                if (!boosterInGroup.gameObject.activeInHierarchy) continue;
                
                var booster = boosterInGroup.GetComponent<Booster>();
                booster.DisableInteraction();
                booster.DisableHighLight();
                graphicsToAnimateFadeOut.AddRange(booster.CollectImagesToAnimate());
            }
            
            graphicsToAnimateFadeOut.AddRange(UIManager.Instance.GetOkButton().CollectImagesToAnimate());
            await DoTweenManager.Instance.PlayFadeOutAnimation(graphicsToAnimateFadeOut);
            
            foreach (Transform boosterInGroup in boostersGroupParent)
            {
                var booster = boosterInGroup.GetComponent<Booster>();
                    booster.DisableBooster();
            }
        }

        private async Task ShowNewBoosters()
        {
            HashSet<int> boosterIds = new();
            var graphicsToAnimateFadeIn = new List<Graphic>();
            
            while (boosterIds.Count < 3)
            {
                boosterIds.Add(_random.Next(0, _boostersTransforms.Count));
            }
            
            foreach (var boosterId in boosterIds)
            {
                var currentBooster = boostersGroupParent.GetChild(boosterId).GetComponent<Booster>();
                currentBooster.gameObject.SetActive(true);
                currentBooster.EnableInteraction();
                currentBooster.EnableBooster();
                graphicsToAnimateFadeIn.AddRange(currentBooster.CollectImagesToAnimate());
            }
            await DoTweenManager.Instance.PlayFadeInAnimation(graphicsToAnimateFadeIn);
            uiButton.interactable = true;
        }

        public override List<Graphic> CollectImagesToAnimate()
        {
            //No OP
            return null;
        }
    }
}
