using UnityEngine.Events;

public static class ActionHandler
{
    public static UnityAction<ItemType, int> SendItemToInventory;
    public static UnityAction DiceRolled;
    public static UnityAction PlayerStopped;
    public static UnityAction<int> DiceStopped;
    public static UnityAction<int> PlayerCanMove;
    public static UnityAction MapGenerationFinished;
}
