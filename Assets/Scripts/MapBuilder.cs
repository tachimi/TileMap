using UnityEngine;

public class MapBuilder : MonoBehaviour
{
    [SerializeField] private Map _map;
    [SerializeField] private MapIndexProvider _mapIndexProvider;
    [SerializeField] private LayerMask _layer;

    private Tile _currentTile;
    private bool _isPlayerMoveTile;

    public void StartPlacingTile(GameObject tilePrefab)
    {
        if (_isPlayerMoveTile)
        {
            Destroy(_currentTile.gameObject);
        }

        var tileObject = Instantiate(tilePrefab);
        _currentTile = tileObject.GetComponent<Tile>();
        _currentTile.transform.SetParent(_map.transform);
        _isPlayerMoveTile = true;
    }

    private void Update()
    {
        if (_isPlayerMoveTile)
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out var hitInfo, 50f, _layer) && _currentTile != null)
            {
                var tileIndex = _mapIndexProvider.GetIndex(hitInfo.point);
                var tilePosition = _mapIndexProvider.GetTilePosition(tileIndex);

                _currentTile.transform.localPosition = tilePosition;

                if (hitInfo.collider.CompareTag("GameZone") && IsPossibleToSetTile(tileIndex))
                {
                    _currentTile.SwitchColorToGreen();

                    if (Input.GetMouseButtonDown(0))
                    {
                        _map.SetTile(tileIndex, _currentTile);
                        _currentTile.ResetColor();
                        _currentTile = null;
                        _isPlayerMoveTile = false;
                    }
                }
                else
                {
                    _currentTile.SwitchColorToRed();
                }
            }
        }
    }

    private bool IsPossibleToSetTile(Vector2Int index)
    {
        return _map.GetTile(index) == null;
    }
}