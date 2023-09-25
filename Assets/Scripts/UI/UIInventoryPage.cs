using Inventory.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory.UI
{
    public class UIInventoryPage : MonoBehaviour
    {
        [SerializeField] private UIInventoryItem _itemPrefab;
        [SerializeField] private RectTransform _contentPanel;
        [SerializeField] private UIInventoryDescription _itemDescription;
        [SerializeField] private MouseFollower _mouseFollower;


        [SerializeField] private Button _deleteItemButton;
        [SerializeField] private Button _useItemButton;

        

        private List<UIInventoryItem> _listOfItems = new List<UIInventoryItem>();

        private int _currentlyDraggedItemIndex = -1;

        public event Action<int> OnDescriptionRequest,
            OnItemActionRequested,
            OnStartDragging;
        public event Action<int, int> OnDropItemActionRequested;

        public event Action<int, int> OnSwapItems;

        private void Awake()
        {
            Hide();
            _mouseFollower.Toogle(false);
            _itemDescription.ResetDescription();
        }
        public void InitializeInventoryUI(int inventorySize)
        {
            for (int i = 0; i < inventorySize; i++)
            {
                UIInventoryItem uiItem = Instantiate(_itemPrefab, Vector3.zero, Quaternion.identity);
                uiItem.transform.SetParent(_contentPanel);
                _listOfItems.Add(uiItem);

                uiItem.OnItemClicked += HandleItemSelection;
                uiItem.OnItemBeginDrag += HandleBeginDrag;
                uiItem.OnItemDroppedOn += HandleSwap;
                uiItem.OnItemEndDrag += HandleEndDrag;
                uiItem.OnRightMouseBtnClick += HandleShowItemActions;

                uiItem.OnDropItemClicked += Handle2ShowItemActions;
            }
        }

        internal void UpdateDescription(int itemIndex, Sprite itemImage, string name, string description)
        {
            _itemDescription.SetDescription(itemImage, name, description);
            DeselectAllItems();
            _listOfItems[itemIndex].Select();
        }

        public void UpdateData(int itemIndex, Sprite itemImage, int itemQuantity)
        {
            if (_listOfItems.Count > itemIndex)
            {
                _listOfItems[itemIndex].SetData(itemImage, itemQuantity);
            }
        }

        private void HandleShowItemActions(UIInventoryItem inventoryItemUI)
        {
            int index = _listOfItems.IndexOf(inventoryItemUI);
            if (index == -1)
            {
                return;
            }
            OnItemActionRequested?.Invoke(index);
        }
        private void Handle2ShowItemActions(UIInventoryItem inventoryItemUI)
        {
            int index = _listOfItems.IndexOf(inventoryItemUI);
            if (index == -1)
            {
                return;
            }
            OnDropItemActionRequested?.Invoke(index,99);
        }

        private void HandleEndDrag(UIInventoryItem inventoryItemUI)
        {
            ResetDraggedItem();
        }

        private void HandleSwap(UIInventoryItem inventoryItemUI)
        {
            int index = _listOfItems.IndexOf(inventoryItemUI);
            if (index == -1)
            {
                return;
            }

            OnSwapItems?.Invoke(_currentlyDraggedItemIndex, index);
            HandleItemSelection(inventoryItemUI);
        }

        private void ResetDraggedItem()
        {
            _mouseFollower.Toogle(false);
            _currentlyDraggedItemIndex = -1;
        }

        private void HandleBeginDrag(UIInventoryItem inventoryItemUI)
        {
            int index = _listOfItems.IndexOf(inventoryItemUI);
            if (index == -1)
                return;
            _currentlyDraggedItemIndex = index;
            HandleItemSelection(inventoryItemUI);
            OnStartDragging?.Invoke(index);
        }

        public void CreateDraggedItem(Sprite sprite, int quantity)
        {
            _mouseFollower.Toogle(true);
            _mouseFollower.SetData(sprite, quantity);
        }

        private void HandleItemSelection(UIInventoryItem inventoryItemUI)
        {
            int index = _listOfItems.IndexOf(inventoryItemUI);
            if (index == -1)
                return;
            OnDescriptionRequest?.Invoke(index);
        }

        public void Show()
        {
            gameObject.SetActive(true);
            ResetSelection();
        }

        public void ResetSelection()
        {
            _itemDescription.ResetDescription();
            DeselectAllItems();
        }

        private void DeselectAllItems()
        {
            foreach (UIInventoryItem item in _listOfItems)
            {
                item.Deselect();
            }
        }

        public void Hide()
        {
            gameObject.SetActive(false);
            ResetDraggedItem();
        }

        internal void ResetAllItems()
        {
            foreach (var item in _listOfItems)
            {
                item.ResetData();
                item.Deselect();
            }
        }

    }
}