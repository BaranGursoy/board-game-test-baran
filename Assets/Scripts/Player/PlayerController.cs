using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float oneJumpTime = 0.2f;
    
    private List<MapTile> mapTiles;
    private int _playerTileIndex = 0;

    private void Awake()
    {
        TileManager.MapGenerationFinished += GetTileList;
        DiceManager.PlayerCanMove += MovePlayer;
    }

    private void GetTileList()
    {
        mapTiles = TileManager.Instance.mapGenerator.GetTileList();
    }

    private void MovePlayer(int totalMoveCount)
    {
        Debug.Log($"Player moves {totalMoveCount} tiles");
        StartCoroutine(MoveCoroutine(totalMoveCount));
    }

    private IEnumerator MoveCoroutine(int totalMoveCount)
    {
        for (int i = 0; i < totalMoveCount; i++)
        {
            Debug.Log("1");
            _playerTileIndex++;
            _playerTileIndex %= mapTiles.Count;

            Debug.Log("2");

            Quaternion endRotation = transform.rotation;

            
            if (mapTiles[_playerTileIndex] is CornerTile)
            {
                transform.Rotate(0f, 90f, 0f);
                endRotation = transform.rotation;
                transform.Rotate(0f, -90f, 0f);
                Debug.Log("3");

            }
            
            Debug.Log("4");

            
            Vector3 startPosition = transform.position;
            Quaternion startRotation = transform.rotation;

            float halfPointX = (mapTiles[_playerTileIndex].transform.position.x + transform.position.x) / 2f;
            float halfPointZ = (mapTiles[_playerTileIndex].transform.position.z + transform.position.z) / 2f;
            
            Vector3 halfEndPosition = new Vector3(halfPointX, transform.position.y + 0.5f, halfPointZ);
            
            Vector3 endPosition = new Vector3(mapTiles[_playerTileIndex].transform.position.x,
                transform.position.y, mapTiles[_playerTileIndex].transform.position.z);
            
            float passedTime = 0f;
            float halfTime = oneJumpTime / 2f;

            while (passedTime <= halfTime)
            {
                transform.position = Vector3.Lerp(startPosition, halfEndPosition,passedTime/halfTime);
                passedTime += Time.deltaTime;
                yield return null;
            }

            passedTime = 0f;
            transform.position = halfEndPosition;
            startPosition = transform.position;
            
            while (passedTime <= halfTime)
            {
                transform.position = Vector3.Lerp(startPosition, endPosition,passedTime/halfTime);
                transform.rotation = Quaternion.Lerp(startRotation, endRotation, passedTime/halfTime);
                passedTime += Time.deltaTime;
                yield return null;
            }
            
            transform.position = endPosition;
            transform.rotation = endRotation;
            Debug.Log("5");

        }
        
    }

}
