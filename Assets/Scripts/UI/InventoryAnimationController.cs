using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryAnimationController : MonoBehaviour
{
    [SerializeField] private Animator inventoryAnimator;
    
    private readonly int AppleBounce = Animator.StringToHash("AppleBounce");
    private readonly int StrawberryBounce = Animator.StringToHash("StrawberryBounce");
    private readonly int PearBounce = Animator.StringToHash("PearBounce");
    private readonly int InventoryBounce = Animator.StringToHash("InventoryBounce");

    private void Awake()
    {
        GameActions.CurrencyReachedDestination += PlayBounceAnimation;
    }

    private void OnDestroy()
    {
        GameActions.CurrencyReachedDestination -= PlayBounceAnimation;
    }

    private void PlayBounceAnimation(ItemType itemType, int dummyQuantity)
    {
        int animatorState;
        switch (itemType)
        {
            case ItemType.Apple:
                animatorState = AppleBounce;
                break;
            case ItemType.Pear:
                animatorState = PearBounce;
                break;
            case ItemType.Strawberry:
                animatorState = StrawberryBounce;
                break;
            default:
                animatorState = InventoryBounce;
                break;
        }
        
        inventoryAnimator.Play(animatorState);
    }
}
