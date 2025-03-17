using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilesManager : MonoBehaviour
{
    public static TilesManager Instance { get; private set; }

    [SerializeField] public Tilemap ground;
    // [SerializeField] public Tile normalGround;
    [SerializeField] public Tile tilledGround;
    [SerializeField] public Tile wateredGround;
    [SerializeField] public List<TileBase> tillableGround;

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
        Vector3Int nextPosition = GetPositionNextToPlayer(_playerPosition, _direction);

        var tile = ground.GetTile(nextPosition);
        if (!tillableGround.Contains(tile))
        {
            return;
        }

        ground.SetTile(nextPosition, tilledGround);
    }

    public void SetToPlantedTile(Vector3Int _position)
    {
        var tile = ground.GetTile(_position);
        if (!tillableGround.Contains(tile))
        {
            return;
        }

        // TODO: planting seeds in tile next to player
    }

    public void SetToWateredTile(Vector3Int _position)
    {
        var tile = ground.GetTile(_position);
        if (tile != tilledGround)
        {
            return;
        }

        // TODO: watering the ground (and seeds/ plants) next to player
    }

    public GroundState GetGroundStateNextToPlayer(Vector3 _playerPosition, PlayerDirection _direction)
    {
        var tilePosition = GetPositionNextToPlayer(_playerPosition, _direction);
        var tile = ground.GetTile(tilePosition);

        if (tillableGround.Contains(tile))
        {
            return GroundState.TILLABLE;
        }

        if (tile == tilledGround)
        {
            return GroundState.TILLED;
        }

        if (tile == wateredGround)
        {
            return GroundState.WATERED;
        }

        return GroundState.NORMAL;
    }

    public Vector3Int GetPositionNextToPlayer(Vector3 _playerPosition, PlayerDirection _direction)
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
                return Vector3Int.zero;
        }

        return nextPosition;
    }
}
