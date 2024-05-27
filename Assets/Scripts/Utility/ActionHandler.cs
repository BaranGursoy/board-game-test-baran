using UnityEngine;
using UnityEngine.Events;

public static class ActionHandler
{
    public static UnityAction<ItemType, int> SendItemToInventory;
    public static UnityAction DiceRolled;
    public static UnityAction FirstDiceTouchedTheFloor;
    public static UnityAction DiceTouchedTheFloor;
    public static UnityAction AllDicesStopped;
    public static UnityAction PlayerStopped;
    public static UnityAction<int> DiceStopped;
    public static UnityAction<int> PlayerCanMove;
    public static UnityAction PlayerTouchedTheBoard;
    public static UnityAction MapGenerationFinished;
    public static UnityAction<ItemType, Vector3, int> SpawnCurrency;
    public static UnityAction PlayerStoppedOnItemTile;
    public static UnityAction<ItemType, int> CurrencyReachedDestination;
    public static UnityAction AllCurrenciesReachedInventory;
}
