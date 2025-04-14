using Source.ButtonHandlers;
using UnityEngine;

namespace Source.Managers
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private ButtonHandler okButton;
        [SerializeField] private ButtonHandler sidePanelButton;
        [SerializeField] private InventoryManager inventoryManager;
        [SerializeField] private BoosterSelectorManager boosterSelectorManager;
        
        public static UIManager Instance { get; private set; }
        
        public BoosterSelectorManager BoosterSelectorManager => boosterSelectorManager;
        public InventoryManager InventoryManager => inventoryManager;
        public ButtonHandler OkButton => okButton;
        public ButtonHandler SidePanelButton => sidePanelButton;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            
            Instance = this;
        }
    }
}
