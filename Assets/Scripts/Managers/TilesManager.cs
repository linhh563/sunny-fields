using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilesManager : MonoBehaviour
{
    public static TilesManager Instance { get; private set; }

    [SerializeField] public Tilemap ground;
    [SerializeField] public Tile normalGround;
    [SerializeField] public Tile tilledGround;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        DontDestroyOnLoad(this.gameObject);
    }

    public void TillingGroundNextToPlayer(Vector3 _playerPosition, PlayerDirection _direction)
    {
        var playerPosition = ground.WorldToCell(_playerPosition);
        Vector3Int nextPosition;

        switch (_direction)
        {
            case PlayerDirection.DOWN:
                nextPosition = playerPosition + new Vector3Int(0, -1, 0);
                break;
            case PlayerDirection.UP:
                nextPosition = playerPosition + new Vector3Int(0, 1, 0);
                break;
            case PlayerDirection.RIGHT:
                nextPosition = playerPosition + new Vector3Int(1, 0, 0);
                break;
            case PlayerDirection.LEFT:
                nextPosition = playerPosition + new Vector3Int(-1, 0, 0);
                break;
            default: 
                return;
        }

        ground.SetTile(nextPosition, tilledGround);
    }
}
