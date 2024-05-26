using UnityEngine.Events;

public static class ActionHandler
{
    public static UnityAction<ItemType, int> SendItemToInventory;
    public static UnityAction DiceRolled;
    public static UnityAction PlayerStopped;
}
