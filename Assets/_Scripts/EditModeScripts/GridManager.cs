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
    public float HalfXSpacing => xSpacing;

    [SerializeField] private float ySpacing = 0.2f;
    public float HalfYSpacing => ySpacing;

    [Space(10)]
    [SerializeField] private Grid grid;
    [SerializeField] private GameObject whiteCube;
    [SerializeField] private GameObject darkCube;

    //[Space(10)]
    //[SerializeField] private List<Sprite> spritesToDistribute;  // Sprites to assign to placeholder objects
    [Space(20)]
    [SerializeField] private List<GameObject> _gridObjs;
    public List<GameObject> GridObjs { get => _gridObjs; set => _gridObjs = value; }

    public static GridManager Instance;

    private void Awake()
    {
        Instance = this;
    }

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
        // Just in case, delete all child objects from this parent
        for (int i = _gridObjs.Count - 1; i >= 0; i--)
        {
            //print("Deleted grid object number: " + i);
            GameObject obj = _gridObjs[i];
            _gridObjs.Remove(obj);
            DestroyImmediate(obj.gameObject);
        }

        Transform[] childrens = transform.GetComponentsInChildren<Transform>();
        if (childrens.Length > 1)    // The first one is parent obj
        {
            for (int i = 1; i < childrens.Length; i++)
            {
                DestroyImmediate(childrens[i].gameObject);
            }
        }
    }
}
