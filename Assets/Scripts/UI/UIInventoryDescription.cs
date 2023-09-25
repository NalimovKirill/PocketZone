using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory.UI
{
    public class UIInventoryDescription : MonoBehaviour
    {
        [SerializeField] private Image _itemImage;
        [SerializeField] private TMP_Text _tittle;
        [SerializeField] private TMP_Text _description;
        [SerializeField] private Button _deleteItemBtn;
        [SerializeField] private Button _useItemBtn;

        private void Awake()
        {
            ResetDescription();
        }

        public void ResetDescription()
        {
            _itemImage.gameObject.SetActive(false);
            _tittle.text = "";
            _description.text = "";

            _deleteItemBtn.gameObject.SetActive(false);
            _useItemBtn.gameObject.SetActive(false);
        }

        public void SetDescription(Sprite sprite, string itemName, string itemDescription)
        {
            _itemImage.gameObject.SetActive(true);
            _itemImage.sprite = sprite;
            _tittle.text = itemName;
            _description.text = itemDescription;

            _deleteItemBtn.gameObject.SetActive(true);
            _useItemBtn.gameObject.SetActive(true);
        }
    }
}