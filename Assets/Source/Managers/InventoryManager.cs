using System.Collections.Generic;
using Source.Data;
using Source.Other;
using Unity.VisualScripting;
using UnityEngine;

namespace Source.Managers
{
    public class InventoryManager : MonoBehaviour
    {
        [SerializeField] private RectTransform sidePanelRoot;
        [SerializeField] private RectTransform inventoryGroupRoot;
        [SerializeField] private RectTransform inventoryVisibilityPoint;
        [SerializeField] private RectTransform inventoryHidePoint;

        public RectTransform SidePanelRoot => sidePanelRoot;
        public RectTransform InventoryVisibilityPoint => inventoryVisibilityPoint;
        public RectTransform InventoryHidePoint => inventoryHidePoint;
        public List<InventoryCell> InventoryCells { get; } = new();

        private void Start()
        {
            InitializeInventory();
        }

        private void InitializeInventory()
        {
            for (var i = 0; i <= 4; i++)
            {
                var inventoryInstance = Instantiate(Resources.Load(Constants.PrefabInventoryCategory + Constants.InventoryItemPrefab)
                    .GameObject(), inventoryGroupRoot);
                var inventoryCell = inventoryInstance.GetComponent<InventoryCell>();
                inventoryCell.Lock(i != 0);
                InventoryCells.Add(inventoryCell);
            }
        }
    }
}
