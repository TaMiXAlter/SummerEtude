using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public GameObject[] _tilePrefab;
    public float _Zspawn = 0;
    public float _tilelength = 23;
    public int numberOfTiles = 5;
    private List<GameObject> activeTiles = new List<GameObject>();
    
    public Transform _playerTransform;
    void Start()
    {
        _playerTransform = GameObject.Find("Player").transform;
        for(int i = 0; i < numberOfTiles; i++)
        {
            if (i == 0)
                SpawnTile(0);
            else
                SpawnTile(Random.Range(0, _tilePrefab.Length)); 
        }
    } 

    void Update()
    {
        if (_playerTransform.position.z -_tilelength > _Zspawn - (numberOfTiles * _tilelength))
        {
            SpawnTile(Random.Range(0, _tilePrefab.Length));
            DeleteTile();
        }
    }

    public void SpawnTile(int tileIndex)
    {
        GameObject go = Instantiate(_tilePrefab[tileIndex],new Vector3(_playerTransform.position.x+2f,_playerTransform.position.y,_Zspawn-20f) , transform.rotation);
        activeTiles.Add(go);
        _Zspawn += _tilelength;
    }

    private void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }
}
