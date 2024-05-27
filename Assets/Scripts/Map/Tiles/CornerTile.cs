using System;
using System.Collections;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class CornerTile : MapTile
{
    protected bool isStartingTile;
    [SerializeField] private TextMeshPro startTextTMP;
    [SerializeField] private TextMeshPro tilePropertyTMP;

    public bool IsStartingTile => isStartingTile;
    public bool IsForward => goForward;
    
    private bool goForward = false;
    private int moveTileCount;

    private void Start()
    {
        goForward = false;//Random.Range(0, 2) == 0;
        moveTileCount = 5;//Random.Range(1, 7);

        string directionName = goForward ? "forward" : "backward";
        
        tilePropertyTMP.text = $"Move {moveTileCount} tiles {directionName}";
    }

    public override void StoppedOnTile()
    {
        if(isStartingTile) return;
        
        StartCoroutine(StoppedOnCornerTileCoroutine());
    }

    private IEnumerator StoppedOnCornerTileCoroutine()
    {
        yield return new WaitForSeconds(1f);
        ActionHandler.PlayerCanMove?.Invoke(moveTileCount, goForward);
    }

    public void SetAsStartingTile()
    {
        isStartingTile = true;
        startTextTMP.gameObject.SetActive(true);
        tilePropertyTMP.gameObject.SetActive(false);
    }
}
