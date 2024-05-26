using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFlyCurrencyEffect : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    private Camera _mainCamera;
    [SerializeField] private RectTransform currencyIcon;
    
    [SerializeField] private int currencyIconCount = 10;
    
    [SerializeField] private RectTransform appleEndPos;
    [SerializeField] private RectTransform pearEndPos;
    [SerializeField] private RectTransform strawberryEndPos;

    private int _currencySpawnCount;
    private int _reachedCurrencies;

    private void Awake()
    {
        ActionHandler.SpawnCurrency += SpawnCurrencyAtWorldPosition;
        ActionHandler.CurrencyReachedDestination += CheckForAllCurrenciesReachedDestination;
    }

    private void Start()
    {
        _mainCamera = Camera.main;
    }

    private void SpawnCurrencyAtWorldPosition(ItemType itemType, Vector3 worldPosition, int quantity)
    {
        Vector3 screenPosition = _mainCamera.WorldToScreenPoint(worldPosition);
        
        RectTransform canvasRect = canvas.GetComponent<RectTransform>();
        Vector2 localPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, screenPosition, null, out localPosition);

        StartCoroutine(SpawnCurrencyCoroutine(itemType, localPosition, quantity));
    }

    private IEnumerator SpawnCurrencyCoroutine(ItemType itemType, Vector3 localPosition, int quantity)
    {
        _currencySpawnCount = quantity < currencyIconCount ? quantity : currencyIconCount;
        
        for (int i = 0; i < _currencySpawnCount; i++)
        {
            GameObject currency =
                ObjectPooler.Instance.SpawnFromPool(currencyIcon.gameObject.name, localPosition, Quaternion.identity);
            
            currency.transform.SetParent(transform);

            UICurrency uiCurrency = currency.GetComponent<UICurrency>();

            uiCurrency.SetCurrencySprite(itemType);
            
            uiCurrency.RectTransform.localPosition = localPosition;
            
            uiCurrency.SendCurrencyToPosition(itemType, GetTargetPositionForCurrency(itemType), quantity);

            yield return new WaitForSeconds(0.05f);
        }
    }
    
    private Vector3 GetTargetPositionForCurrency(ItemType itemType)
    {
        switch (itemType)
        {
            case ItemType.Apple:
                return appleEndPos.transform.position;
            case ItemType.Pear:
                return pearEndPos.transform.position;
            case ItemType.Strawberry:
                return strawberryEndPos.transform.position;
            default:
                return Vector3.zero;
        }
    }

    private void CheckForAllCurrenciesReachedDestination(ItemType itemType, int quantity)
    {
        _reachedCurrencies++;

        if (_reachedCurrencies >= _currencySpawnCount)
        {
            ActionHandler.SendItemToInventory?.Invoke(itemType, quantity);
            _reachedCurrencies = 0;
        }
    }

    
    
    
}
