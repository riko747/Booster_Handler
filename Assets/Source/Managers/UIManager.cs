using System.Collections.Generic;
using Source.ButtonHandlers;
using Source.Data;
using UnityEngine;

namespace Source.Managers
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private Transform boostersGroup;
        [SerializeField] private ButtonHandler okButton;
        [SerializeField] private ButtonHandler refreshButton;
        [SerializeField] private InventoryManager inventoryManager;
        
        private readonly List<Booster> _boosters = new();
        public static UIManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            
            Instance = this;
        }

        private void Start()
        {
            GetAllChildrenInBoosterGroup();
        }

        private void GetAllChildrenInBoosterGroup()
        {
            foreach (Transform booster in boostersGroup)
            {
                var currentBooster = booster.GetComponent<Booster>();
                _boosters.Add(currentBooster);
            }
        }

        public List<Booster> GetBoostersFromShuffle() => _boosters;
        public List<InventoryCell> GetInventoryCells() => inventoryManager.InventoryCells;
        
        public ButtonHandler GetOkButton() => okButton;
        public ButtonHandler GetRefreshButton() => refreshButton;
    }
}
