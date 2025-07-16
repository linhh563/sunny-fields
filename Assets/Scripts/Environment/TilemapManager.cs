using UnityEngine;
using UnityEngine.Tilemaps;
using Characters;

namespace Environment
{
    public class TilemapManager : MonoBehaviour
    {
        [Header("Tilemaps")]
        [SerializeField] private Tilemap _groundTilemap;
        private Tilemap _plantingTilemap;
        

        // TODO: Use Resources to get tile base automatically
        public TileBase defaultGroundTile {get; private set;}
        public TileBase defaultGroundTile_2 { get; private set; }
        public TileBase hoedGroundTile { get; private set; }
        public TileBase wateredGroundTile {get; private set;}

        private string _farmName;

        public Tilemap groundTilemap => _groundTilemap;

        void Awake()
        {
            defaultGroundTile = Resources.Load<TileBase>("Tiles/Ground/Default_Ground");
            defaultGroundTile_2 = Resources.Load<TileBase>("Tiles/Ground/Default_Ground_2");
            hoedGroundTile = Resources.Load<TileBase>("Tiles/Ground/Hoed_Ground");
            wateredGroundTile = Resources.Load<TileBase>("Tiles/Ground/Watered_Ground");

            if (defaultGroundTile == null || defaultGroundTile_2 == null || hoedGroundTile == null || wateredGroundTile == null)
            {
                Debug.LogError("Can't load ground tile base");
            }
        }

        void Start()
        {
        }


        public Vector3Int GetTileInFrontCharacter()
        {
            var _characterPosition = _groundTilemap.WorldToCell(Characters.CharacterController._characterPosition);

            switch (Characters.CharacterController._currentDirection)
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
