using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICurrency : MonoBehaviour
{
   [SerializeField] private Image currencyImage;
   [SerializeField] private RectTransform currencyRectTransform;
   
   [SerializeField] private Sprite appleSprite;
   [SerializeField] private Sprite pearSprite;
   [SerializeField] private Sprite strawberrySprite;
   
   [SerializeField] private float flyDuration = 1f;
   
   public RectTransform RectTransform => currencyRectTransform;
   
   public void SetCurrencySprite(ItemType itemType)
   {
      currencyImage.sprite = GetSpriteForCurrency(itemType);
   }
   
   private Sprite GetSpriteForCurrency(ItemType itemType)
   {
      switch (itemType)
      {
         case ItemType.Apple:
            return appleSprite;
         case ItemType.Pear:
            return pearSprite;
         case ItemType.Strawberry:
            return strawberrySprite;
      }

      return null;
   }

   public void SendCurrencyToPosition(ItemType itemType, Vector3 currencyEndPosition, int quantity)
   {
      StartCoroutine(CurrencyMoveCoroutine(itemType, currencyEndPosition, quantity));
   }

   private IEnumerator CurrencyMoveCoroutine(ItemType itemType, Vector3 endPosition, int quantity)
   {
      float passedTime = 0f;
      Vector3 startingPosition = currencyRectTransform.transform.position;

      while (passedTime <= flyDuration)
      {
         currencyRectTransform.transform.position = Vector3.Slerp(startingPosition, endPosition, passedTime / flyDuration);
         
         passedTime += Time.deltaTime;

         yield return null;
      }

      currencyRectTransform.transform.position = endPosition;
      
      ActionHandler.CurrencyReachedDestination?.Invoke(itemType, quantity);
      ObjectPooler.Instance.ReturnToPool(gameObject, PrefabType.CurrencySprite);
   }
}
