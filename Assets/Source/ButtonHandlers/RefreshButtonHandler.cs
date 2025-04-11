using System.Collections.Generic;
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
        
        private void Start()
        {
            uiButton = GetComponent<Button>();

            if (uiButton != null)
                uiButton.onClick.AddListener(OnButtonClicked);

            foreach (Transform boosterInGroup in boostersGroupParent)
            {
                _boostersTransforms.Add(boosterInGroup);
            }
        }
        
        protected override void OnButtonClicked()
        {
            HashSet<int> boosterIds = new();
            
            UIManager.Instance.DisableOKButton();
            
            while (boosterIds.Count < 3)
            {
                boosterIds.Add(_random.Next(0, _boostersTransforms.Count));
            }

            foreach (Transform boosterInGroup in boostersGroupParent)
            {
                var booster = boosterInGroup.GetComponent<Booster>();
                booster.DisableHighlightAnimation();
                booster.DisableBoosterHighlight();
                boosterInGroup.gameObject.SetActive(false);
            }

            foreach (var boosterId in boosterIds)
            {
                boostersGroupParent.GetChild(boosterId).gameObject.SetActive(true);
            }
        }

        private void OnDestroy()
        {
            if (uiButton != null)
                uiButton.onClick.RemoveListener(OnButtonClicked);
        }
    }
}
