using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Inventory.UI
{
    public class UIInventoryItem : MonoBehaviour, IPointerClickHandler,
        IBeginDragHandler, IEndDragHandler, IDropHandler, IDragHandler
    {
        [SerializeField] private Image _itemImage;
        [SerializeField] private TMP_Text _quantityTxt;
        [SerializeField] private Image _borderImage;
        
        private UseItemBtn _useItemBtn;
        private DeleteItemBtn _dropItemBtn;

        public event Action<UIInventoryItem> OnItemClicked, OnItemDroppedOn,
            OnItemBeginDrag, OnItemEndDrag, OnRightMouseBtnClick, OnDropItemClicked;

        private bool _empty = true;
        private bool _isSubed1 = false;
        private bool _isSubed2 = false;

        private void Awake()
        {
            ResetData();
            Deselect();
        }
        public void ResetData()
        {
            _itemImage.gameObject.SetActive(false);
            _empty = true;
            if (_useItemBtn == null)
            {
                return;
            }
            if (_dropItemBtn == null)
            {
                return;
            }

        }
        public void Deselect()
        {
            _borderImage.enabled = false;

            if (_isSubed1)
            {
                _useItemBtn.ClickOnUseBtn -= OnUseItemBtnClicked;
                _isSubed1 = false;
            }
            if (_isSubed2)
            {
                _dropItemBtn.ClickOnDropBtn -= OnDropItemBtnClicked;
                _isSubed2 = false;
            }
        }

        public void SetData(Sprite sprite, int quantity)
        {
            _itemImage.gameObject.SetActive(true);
            _itemImage.sprite = sprite;
            _quantityTxt.text = quantity + "";
            _empty = false;
        }

        public void Select()
        {
            _borderImage.enabled = true;

            _useItemBtn = GameObject.Find("UseItemBtn").GetComponent<UseItemBtn>();
            _dropItemBtn = GameObject.Find("DeleteItemBtn").GetComponent<DeleteItemBtn>();
            if (!_isSubed1)
            {
                _useItemBtn.ClickOnUseBtn += OnUseItemBtnClicked;
                _isSubed1 = true;
            }
            if (!_isSubed2)
            {
                _dropItemBtn.ClickOnDropBtn += OnDropItemBtnClicked;
                _isSubed2 = true;
            }
        }

        public void OnPointerClick(PointerEventData pointerData)
        {
            if (pointerData.button == PointerEventData.InputButton.Right)
            {
                //OnRightMouseBtnClick?.Invoke(this);
            }
            else
            {
                OnItemClicked?.Invoke(this);
            }
        }

        public void OnUseItemBtnClicked()
        {
            OnRightMouseBtnClick?.Invoke(this);
        }
        public void OnDropItemBtnClicked()
        {
            OnDropItemClicked?.Invoke(this);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (_empty)
            {
                return;
            }
            OnItemBeginDrag?.Invoke(this);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            OnItemEndDrag?.Invoke(this);
        }

        public void OnDrop(PointerEventData eventData)
        {
            OnItemDroppedOn?.Invoke(this);
        }

        public void OnDrag(PointerEventData eventData)
        {

        }
    }
}