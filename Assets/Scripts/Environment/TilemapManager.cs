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
        public TileBase hoedGroundTile {get; private set;}
        public TileBase wateredGroundTile {get; private set;}

        private string _farmName;

        public Tilemap groundTilemap => _groundTilemap;

        void Awake()
        {
            defaultGroundTile = Resources.Load<TileBase>("Tiles/Ground/Default_Ground");
            hoedGroundTile = Resources.Load<TileBase>("Tiles/Ground/Hoed_Ground");
            wateredGroundTile = Resources.Load<TileBase>("Tiles/Ground/Watered_Ground");

            if (defaultGroundTile == null || hoedGroundTile == null || wateredGroundTile == null)
            {
                Debug.LogError("Can't load ground tile base");
            }
        }

        void Start()
        {
            // TODO: Use Transform.Find to get tile map game object automatically            
        }


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
