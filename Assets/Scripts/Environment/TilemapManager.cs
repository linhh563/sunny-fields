using UnityEngine;
using UnityEngine.Tilemaps;
using Characters;

namespace Environment
{
    public class TilemapManager : MonoBehaviour
    {
        [Header("Tilemaps")]
        [SerializeField] private Tilemap _groundTilemap;
        // TODO: delete ground rule tile, and delete this tilemap
        [SerializeField] private Tilemap _hoedTilemap; 

        [Header("Tile Bases")]
        [SerializeField] private TileBase _defaultGroundTile;
        [SerializeField] private TileBase _hoedGroundTile;
        [SerializeField] private TileBase _wateredGroundTile;

        public Tilemap groundTilemap => _groundTilemap;
        public Tilemap hoedTilemap => _hoedTilemap;
        public TileBase defaultGroundTile => _defaultGroundTile;
        public TileBase hoedGroundTile => _hoedGroundTile;
        public TileBase wateredGroundTile => _wateredGroundTile;


        private string _farmName;


        public Vector3Int GetTileInFrontCharacter()
        {
            var _characterPosition = _groundTilemap.WorldToCell(Characters.CharacterController.characterPosition);

            switch (Characters.CharacterController.currentDirection)
            {
                case CharacterDirection.Up:
                    return _characterPosition + Vector3Int.up;

                case CharacterDirection.Down:   
                    return _characterPosition + Vector3Int.down;

                case CharacterDirection.Right:
                    return _characterPosition + Vector3Int.right;

                default:
                    return _characterPosition + Vector3Int.left;
            }
        }
    }    
}
