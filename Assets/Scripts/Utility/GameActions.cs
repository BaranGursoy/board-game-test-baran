using UnityEngine;
using UnityEngine.Events;

public static class GameActions
{
    public static UnityAction<ItemType, int> SendItemToInventory;
    public static UnityAction HideDiceButton;
    public static UnityAction FirstDiceTouchedTheFloor;
    public static UnityAction DiceTouchedTheFloor;
    public static UnityAction AllDicesStopped;
    public static UnityAction PlayerStopped;
    public static UnityAction<int> DiceStopped;
    public static UnityAction<int, bool> PlayerCanMove;
    public static UnityAction PlayerTouchedTheBoard;
    public static UnityAction MapGenerationFinished;
    public static UnityAction<ItemType, Vector3, int> SpawnCurrency;
    public static UnityAction PlayerStoppedOnItemTile;
    public static UnityAction<ItemType, int> CurrencyReachedDestination;
    public static UnityAction AllCurrenciesReachedInventory;
}
