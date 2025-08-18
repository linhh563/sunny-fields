using UnityEngine;
using UnityEngine.Tilemaps;
using Management;
using System.Collections.Generic;

namespace Environment
{
    public class TilemapManager : MonoBehaviour
    {
        [Header("Tilemaps")]
        [SerializeField] private Tilemap _baseTilemap;
        [SerializeField] private Tilemap _groundTilemap;
        [SerializeField] private Tilemap _plantingTilemap;


        public TileBase defaultGroundTile { get; private set; }
        public TileBase defaultGroundTile_2 { get; private set; }
        public TileBase hoedGroundTile { get; private set; }
        public TileBase wateredGroundTile { get; private set; }

        // white tile used for marking the planted tiles
        public TileBase whiteTile { get; private set; }

        public static List<Vector3Int> hoedGrounds = new List<Vector3Int>();
        public static List<Vector3Int> wateredGrounds = new List<Vector3Int>();

        // public fields
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


        void Start()
        {
            TimeManager.OnDayChanged += ResetWateredGround;
        }


        void OnDisable()
        {
            TimeManager.OnDayChanged -= ResetWateredGround;

            hoedGrounds.Clear();
            wateredGrounds.Clear();
        }


        // get the position (in tile map) of the tile in front of the character
        public Vector3Int GetTileInFrontCharacter()
        {
            var _characterPosition = _groundTilemap.WorldToCell(Characters.CharacterController.characterWorldPosition);

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


        public void SetGroundTile(Vector3Int position, GroundState state)
        {
            switch (state)
            {
                case GroundState.Hoed:
                    _groundTilemap.SetTile(position, hoedGroundTile);
                    AddHoedGround(position);
                    break;

                case GroundState.Watered:
                    _groundTilemap.SetTile(position, wateredGroundTile);
                    AddHoedGround(position);
                    AddWateredGround(position);
                    break;
            }
        }


        public void AddHoedGround(Vector3Int newHoedGround)
        {
            hoedGrounds.Add(newHoedGround);
        }


        public void AddWateredGround(Vector3Int newWateredGround)
        {
            wateredGrounds.Add(newWateredGround);
        }


        private void ResetWateredGround()
        {
            foreach (var hoedGround in hoedGrounds)
            {
                _groundTilemap.SetTile(hoedGround, hoedGroundTile);
            }

            wateredGrounds.Clear();
        }
    }
}
