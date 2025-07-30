using UnityEngine;
using UnityEngine.Tilemaps;
using Management;

namespace Environment
{
    public class TilemapManager : MonoBehaviour
    {
        [Header("Tilemaps")]
        [SerializeField] private Tilemap _baseTilemap;
        [SerializeField] private Tilemap _groundTilemap;
        [SerializeField] private Tilemap _plantingTilemap;


        // TODO: Use Resources to get tile base automatically
        public TileBase defaultGroundTile { get; private set; }
        public TileBase defaultGroundTile_2 { get; private set; }
        public TileBase hoedGroundTile { get; private set; }
        public TileBase wateredGroundTile { get; private set; }

        // white tile used for marking the planted tiles
        public TileBase whiteTile { get; private set; }

        private string _farmName;

        public Tilemap baseTilemap => _baseTilemap;
        public Tilemap groundTilemap => _groundTilemap;
        public Tilemap plantingTilemap => _plantingTilemap;


        void Awake()
        {
            // load tile bases from resources
            defaultGroundTile = Resources.Load<TileBase>("Tiles/Ground/default_ground");
            defaultGroundTile_2 = Resources.Load<TileBase>("Tiles/Ground/default_ground_2");
            hoedGroundTile = Resources.Load<TileBase>("Tiles/Ground/hoed_ground");
            wateredGroundTile = Resources.Load<TileBase>("Tiles/Ground/watered_ground");

            whiteTile = Resources.Load<TileBase>("Tiles/WhiteTile");

            // check if tile bases are loaded successfully
            if (defaultGroundTile == null ||
                defaultGroundTile_2 == null ||
                hoedGroundTile == null ||
                wateredGroundTile == null ||
                whiteTile == null)
            {
                Debug.LogError("Can't load ground tile base.");
            }
        }

        // get the position (in tile map) of the tile in front of the character
        public Vector3Int GetTileInFrontCharacter()
        {
            var _characterPosition = _groundTilemap.WorldToCell(Characters.CharacterController.CharacterWorldPosition);

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
