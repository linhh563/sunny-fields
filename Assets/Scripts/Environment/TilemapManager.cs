using UnityEngine;
using UnityEngine.Tilemaps;

namespace Environment
{
    public class TilemapManager : MonoBehaviour
    {
        [SerializeField] private Tilemap _tilemap;
        private string _farmName;
        private Vector3Int _characterPosition;

        private void Update() {
            UpdateCharacterPosition();
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log(GetTileName(GetTileInFrontCharacter()));
            }
        }

        public Vector3Int GetTileInFrontCharacter()
        {
            switch (Characters.CharacterController.currentDirection)
            {
                case Characters.CharacterDirection.Up:
                    return _characterPosition + new Vector3Int(0, 1, 0);

                case Characters.CharacterDirection.Down:
                    return _characterPosition + new Vector3Int(0, -1, 0);

                case Characters.CharacterDirection.Right:
                    return _characterPosition + new Vector3Int(1, 0, 0);

                default:
                    return _characterPosition + new Vector3Int(-1, 0, 0);
            }
        }

        public string GetTileName(Vector3Int position)
        {
            var tile = _tilemap.GetSprite(position);
            if (tile != null)
            {
                return tile.name;
            }
            return "";
        }

        private void UpdateCharacterPosition()
        {
            _characterPosition = _tilemap.WorldToCell(Characters.CharacterController.characterPosition);
        }
    }    
}
