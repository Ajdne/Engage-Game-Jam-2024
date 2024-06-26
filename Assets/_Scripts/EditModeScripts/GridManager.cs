using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Random = System.Random;

[ExecuteAlways]
public class GridManager : MonoBehaviour
{
    /// <summary>
    /// This script handles Grid layout, size and randomizes disttribution of Sprites
    /// </summary>
    [Header("Grid Settings: ")]
    [SerializeField, Range(5, 20)] private int numberOfRows = 9;
    [SerializeField, Range(5, 20)] private int numberOfColumns = 9;

    [Space(5)]
    [SerializeField] private float xSpacing = 0.1f;
    [SerializeField] private float ySpacing = 0.2f;

    [Space(10)]
    [SerializeField] private Grid grid;
    [SerializeField] private GameObject whiteCube;
    [SerializeField] private GameObject darkCube;

    //[Space(10)]
    //[SerializeField] private List<Sprite> spritesToDistribute;  // Sprites to assign to placeholder objects
    [Space(20)]
    [SerializeField] private List<GameObject> _gridObjs;
    public List<GameObject> GridObjs { get => _gridObjs; set => _gridObjs = value; }

#if UNITY_EDITOR

    private void LateUpdate()
    {
        print("Update: " + this);
        if (Application.isPlaying) this.enabled = false;    // Disable the script in runtime

        CheckForMissingElements();  // If one of the scene objects get deleted, this we reset the grid
        PositionGridElements();
    }

#endif

    [ButtonGroup("Set_Grid")]
    private void SetGrid()
    {
        DeleteGrid();

        // Check if there are too many or too little sprites assigned
        //if (spritesToDistribute.Count < numberOfRows * numberOfColumns)
        //{
        //    Debug.LogError("Not enough spritess assigned! Will not generate grid! The needed number of sprites is " + numberOfRows * numberOfColumns);
        //    return;
        //}
        //if (spritesToDistribute.Count > numberOfRows * numberOfColumns) Debug.LogWarning("Too many sprites assigned! Some of them will not be distributed!");

        for (int i = 0; i < numberOfRows; i++)
        {
            for (int j = 0; j < numberOfColumns; j++)
            {
                GameObject obj;
                var worldPos = grid.GetCellCenterWorld(new Vector3Int(i, j));
                if (_gridObjs.Count % 2 == 0)
                {
                    obj = Instantiate(darkCube, worldPos, Quaternion.identity, grid.transform);
                }
                else obj = Instantiate(whiteCube, worldPos, Quaternion.identity, grid.transform);

                _gridObjs.Add(obj);
                obj.name = $"PlaceholderObj_{i}_{j}";
                //obj.GetComponent<SpriteRenderer>().sprite = spritesToDistribute[j + i * numberOfColumns];
            }
        }
    }
    private void PositionGridElements()
    {
        if (_gridObjs.Count != numberOfRows * numberOfColumns) return; // Prevent errors

        grid.cellGap = new Vector3(xSpacing, ySpacing);

        for (int i = 0; i < numberOfRows; i++)
        {
            for (int j = 0; j < numberOfColumns; j++)
            {
                // Iterate through every element in the list
                GameObject obj = _gridObjs[j + i * numberOfColumns]; // This way we are reading the matrix as a linear sequence
                obj.transform.position = grid.GetCellCenterWorld(new Vector3Int(i, j));
            }
        }
    }

    private void CheckForMissingElements()
    {
        //Check if some of the items are null(missing)
        foreach (GameObject item in _gridObjs)
        {
            if (!item)
            {
                SetGrid();
                break;
            }
        }
    }
    [ButtonGroup("Set_Grid")]
    private void DeleteGrid()
    {
        for (int i = _gridObjs.Count - 1; i >= 0; i--)
        {
            //print("Deleted grid object number: " + i);
            GameObject obj = _gridObjs[i];
            _gridObjs.Remove(obj);
            DestroyImmediate(obj.gameObject);
        }
    }

    /// <summary>
    /// The grid layout stays the same, but the sprites are assigned randomly.
    /// </summary>
    //[Button]
    //private void RandomizeGrid()
    //{
    //    if(_gridObjs.Count == 0)
    //    {
    //        Debug.LogWarning("Can not randomize an empty grid!");
    //        return;
    //    }
    //    var rand = new Random();
    //    int index = 0;
    //    foreach (var sprite in spritesToDistribute.OrderBy(t => rand.Next()).Take(_gridObjs.Count))
    //    {
    //        _gridObjs[index].GetComponent<SpriteRenderer>().sprite = sprite;
    //        index++;
    //    }
    //}
}
