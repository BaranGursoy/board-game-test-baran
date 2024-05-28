    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class PlayerMovementController : MonoBehaviour
    {
        [SerializeField] private float oneJumpTime = 0.2f;
        
        private List<MapTile> mapTiles;
        private int _playerTileIndex = 0;

        private void Awake()
        {
            ActionHandler.MapGenerationFinished += GetTileList;
            ActionHandler.PlayerCanMove += MovePlayer;
        }

        private void GetTileList()
        {
            mapTiles = TileManager.Instance.mapGenerator.GetTileList();
        }

        private void MovePlayer(int totalMoveCount, bool forward = true)
        {
            StartCoroutine(MoveCoroutine(totalMoveCount, forward));
        }

        private bool FindIsCornerAndIsPassable()
        {
            return mapTiles[_playerTileIndex] is CornerTile foundCornerTile &&
                   (foundCornerTile.IsForward || foundCornerTile.IsStartingTile);
        }
        
        private bool FindIsCornerIsNotDestination(int destinationTileIndex)
        {
           return mapTiles[_playerTileIndex] is CornerTile && mapTiles[destinationTileIndex] != mapTiles[_playerTileIndex];
        }

        private bool IsCornerIsDestinationAndIsStart(int destinationTileIndex)
        {
            return mapTiles[_playerTileIndex] is CornerTile cornerTile && mapTiles[destinationTileIndex] == mapTiles[_playerTileIndex] && cornerTile.IsStartingTile;
        }

        private IEnumerator MoveCoroutine(int totalMoveCount, bool forward)
        {
            int destinationTileIndex = LogDestinationTileNumber(totalMoveCount, forward);

            for (int i = 0; i < totalMoveCount; i++)
            {
                if (forward)
                {
                    _playerTileIndex++;
                }

                else
                {
                    _playerTileIndex--;

                    if (_playerTileIndex == -1)
                    {
                        _playerTileIndex = mapTiles.Count - 1;
                    }
                }
                
                _playerTileIndex %= mapTiles.Count;
                
                Quaternion endRotation = transform.rotation;
                
                if (((FindIsCornerAndIsPassable() || FindIsCornerIsNotDestination(destinationTileIndex)) && !IsCornerIsDestinationAndIsStart(destinationTileIndex)) || IsCornerIsDestinationAndIsStart(destinationTileIndex))
                {
                    float multiplier = forward ? 1f : -1f;
                    
                    transform.Rotate(0f, 90f * multiplier, 0f);
                    endRotation = transform.rotation;
                    transform.Rotate(0f, -90f * multiplier, 0f);
                }
                
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
                
                ActionHandler.PlayerTouchedTheBoard?.Invoke();
            }
            
            mapTiles[_playerTileIndex].StoppedOnTile();

            if (mapTiles[_playerTileIndex] is not CornerTile cornerTile || cornerTile.IsStartingTile)
            {
                ActionHandler.PlayerStopped?.Invoke();
            }
        }

        private int LogDestinationTileNumber(int totalMoveCount, bool forward)
        {
            if (!forward)
            {
                totalMoveCount *= -1;
            }

            int destinationIndex = _playerTileIndex + totalMoveCount;

            if (destinationIndex <= -1)
            {
                destinationIndex = mapTiles.Count + destinationIndex;
                Debug.Log($"Player will reach to tile number {destinationIndex + 1}");
                return destinationIndex;
            }

            int destinationTileNumber = (destinationIndex + 1) % mapTiles.Count;

            Debug.Log($"Player will reach to tile number {destinationTileNumber}");

            return destinationTileNumber - 1;
        }

    }
