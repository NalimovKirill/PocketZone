using Inventory.Model;
using Inventory.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

namespace Inventory
{
    public class InventoryController : MonoBehaviour
    {
        [SerializeField] private UIInventoryPage _inventoryUI;
        [SerializeField] private InventorySO _inventoryData;


        public List<InventoryItem> initialItems = new List<InventoryItem>();

        private void Start()
        {
            PrepareUI();
            PrepareInventoryData();
        }

        private void PrepareInventoryData()
        {
            _inventoryData.Initialize();
            _inventoryData.OnInventoryUpdated += UpdateInventoryUI;
            foreach (InventoryItem item in initialItems)
            {
                if (item.IsEmpty) 
                    continue;
                _inventoryData.AddItem(item);
            }
        }

        private void UpdateInventoryUI(Dictionary<int, InventoryItem> inventoryState)
        {
            _inventoryUI.ResetAllItems();
            foreach (var item in inventoryState)
            {
                _inventoryUI.UpdateData(item.Key, item.Value.item.ItemImage,
                    item.Value.quantity);
            }
        }

        private void PrepareUI()
        {
            _inventoryUI.InitializeInventoryUI(_inventoryData.Size);
            _inventoryUI.OnDescriptionRequest += HandleDescriptionRequest;
            _inventoryUI.OnSwapItems += HandleSwapItems;
            _inventoryUI.OnStartDragging += HandleDragging;
            _inventoryUI.OnItemActionRequested += HandleItemActionRequest;
            _inventoryUI.OnDropItemActionRequested += DropItem;
        }

        private void HandleItemActionRequest(int itemIndex)
        {
            InventoryItem inventoryItem = _inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
                return;
            IItemAction itemAction = inventoryItem.item as IItemAction;
            if (itemAction != null)
            {
                itemAction.PerfomAction(gameObject);
            }
            IdestroyableItem destroyableItem = inventoryItem.item as IdestroyableItem;
            if (destroyableItem != null)
            {
                _inventoryData.RemoveItem(itemIndex, 1);
            }
        }

        private void DropItem(int itemIndex,int quantity)
        {
            _inventoryData.RemoveItem(itemIndex, quantity);
            _inventoryUI.ResetSelection();
        }

        private void HandleDragging(int itemIndex)
        {
            InventoryItem inventoryItem = _inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
                return;
            _inventoryUI.CreateDraggedItem(inventoryItem.item.ItemImage, inventoryItem.quantity);
        }

        private void HandleSwapItems(int itemIndex_1, int itemIndex_2)
        {
            _inventoryData.SwapItems(itemIndex_1, itemIndex_2);
        }

        private void HandleDescriptionRequest(int itemIndex)
        {
            InventoryItem inventoryItem = _inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
            {
                _inventoryUI.ResetSelection();
                return;
            }

            ItemSO item = inventoryItem.item;
            _inventoryUI.UpdateDescription(itemIndex, item.ItemImage,
                item.name, item.Description);

        }


        public void OpenInventory()
        {
            if (_inventoryUI.isActiveAndEnabled == false)
            {
                _inventoryUI.Show();
                foreach (var item in _inventoryData.GetCurrentInventoryState())
                {
                    _inventoryUI.UpdateData(item.Key,
                        item.Value.item.ItemImage,
                        item.Value.quantity);
                }
            }
            else
            {
                _inventoryUI.Hide();
            }
        }
    }
}