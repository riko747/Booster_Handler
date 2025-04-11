 using System.Collections.Generic;
 using Source.ButtonHandlers;
 using Source.Data;
 using UnityEngine;

 namespace Source
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private Transform boostersGroup;
        [SerializeField] private OkButtonHandler okButton;
        [SerializeField] private List<InventoryCell> inventoryCells;
        [SerializeField] private RectTransform sidePanelHidePoint;
        [SerializeField] private RectTransform sidePanelVisibilityPoint;
        
        private readonly List<Booster> _boosters = new();
        public static UIManager Instance { get; private set; }

        public List<InventoryCell> InventoryCells => inventoryCells;
        public RectTransform SidePanelHidePoint => sidePanelHidePoint;
        public RectTransform SidePanelVisibilityPoint => sidePanelVisibilityPoint;

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

        public List<Booster> GetBoosters() => _boosters;
        
        public OkButtonHandler GetOkButton() => okButton;
    }
}
