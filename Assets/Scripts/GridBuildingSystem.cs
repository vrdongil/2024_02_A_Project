using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;


public class GridCell
{
    public Vector3Int Position;
    public bool isOccupied;
    public GameObject Building;

    public GridCell(Vector3Int position)
    {
        Position = position;
        isOccupied = false;
        Building = null;
    }
}

public class GridBuildingSystem : MonoBehaviour
{

    [SerializeField] private int width = 10;
    [SerializeField] private int height = 10;
    [SerializeField] float cellSize = 1.0f;
    [SerializeField] private GameObject cellPrefebs;
    [SerializeField] private GameObject buildingPrefebs;

    [SerializeField] private PlayerController playerController;

    [SerializeField] private Grid grid;
    private GridCell[,] cells;
    private Camera firstPersonCamera;

    private void HighlightCell(Vector3Int gridPosition)
    {
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                GameObject cellObject = cells[x, z].Building != null ? cells[x, z].Building : transform.GetChild(x * height + z).gameObject;
                cellObject.GetComponent<Renderer>().material.color = Color.white;
            }
        }

        GridCell cell = cells[gridPosition.x, gridPosition.z];
        GameObject highlightObject = cell.Building != null ? cell.Building : transform.GetChild(gridPosition.x * height + gridPosition.z).gameObject;
        highlightObject.GetComponent<Renderer>().material.color = cell.isOccupied ? Color.red : Color.green;

    }


    private bool isVaildGridPosition(Vector3Int gridPosition)
    {
        return gridPosition.x >= 0 && gridPosition.x < width &&
            gridPosition.z >= 0 && gridPosition.z < height;
    }
    private Vector3 GetLookPosition()
    {
        if (playerController.isFirstPerson)
        {
            Ray ray = new Ray(firstPersonCamera.transform.position, firstPersonCamera.transform.forward);
            if (Physics.Raycast(ray, out RaycastHit hitInfo, 5.0f))
            {
                Debug.DrawRay(ray.origin, ray.direction * hitInfo.distance, Color.red);
                return hitInfo.point;
            }
            else
            {
                Debug.DrawRay(ray.origin, ray.direction * 5.0f, Color.white);
            }
        }
        else
        {
            Vector3 characterPosition = playerController.transform.position;
            Vector3 characterForward = playerController.transform.forward;
            Vector3 rayOrigin = characterPosition + Vector3.up * 1.5f + characterForward * 0.5f;
            Vector3 rayDirection = (characterForward - Vector3.up).normalized;

            Ray ray = new Ray(rayOrigin, rayDirection);

            if (Physics.Raycast(ray, out RaycastHit hitInfo, 5.0f))
            {
                Debug.DrawRay(ray.origin, ray.direction * hitInfo.distance, Color.red);
                return hitInfo.point;
            }
            else
            {
                Debug.DrawRay(ray.origin, ray.direction * 5.0f, Color.white);
            }
        }
        return Vector3.zero;
    }




    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        for (int x = 0; x < width; x++)
        {

            for (int z = 0; z < height; z++)
            {
                Vector3 cellCenter = grid.GetCellCenterWorld(new Vector3Int(x, 0, z));
                Gizmos.DrawWireCube(cellCenter, new Vector3(cellSize, 0.1f, cellSize));
            }
        }

    }


    // Start is called before the first frame update
    void Start()
    {
        firstPersonCamera = playerController.firstPersonCamera;
        CreateGrid();
    }

    private void CreateGrid()
    {
        grid.cellSize = new Vector3(cellSize, cellSize, cellSize);

        cells = new GridCell[width, height];
        Vector3 gridCenter = playerController.transform.position;
        gridCenter.y = 0;
        transform.position = gridCenter - new Vector3(width * cellSize / 2.0f, 0, height * cellSize / 2.0f);

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                Vector3Int cellPosition = new Vector3Int(x, 0, z);
                Vector3 worldPosition = grid.GetCellCenterWorld(cellPosition);
                GameObject cellObject = Instantiate(cellPrefebs, worldPosition, cellPrefebs.transform.rotation);
                cellObject.transform.SetParent(transform);

                cells[x, z] = new GridCell(cellPosition);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lockPosition = GetLookPosition();
        if (lockPosition != Vector3.zero)
        {
            Vector3Int gridPosition = grid.WorldToCell(lockPosition);
            if (isVaildGridPosition(gridPosition))
            {
                HighlightCell(gridPosition);

                if (Input.GetMouseButtonDown(0))
                {
                    PlaceBuilding(gridPosition);
                }

                if (Input.GetMouseButtonDown(1))
                {
                    RemoveBuilding(gridPosition);
                }
            }

        }
    }

    private void PlaceBuilding(Vector3Int gridPosition)
    {
        GridCell cell = cells[gridPosition.x, gridPosition.z];
        if (!cell.isOccupied)
        {
            Vector3 worldPosition = grid.GetCellCenterWorld(gridPosition);
            GameObject building = Instantiate(buildingPrefebs, worldPosition, Quaternion.identity);
            cell.isOccupied = true;
            cell.Building = building;
        }

    }
    private void RemoveBuilding(Vector3Int gridPosition)
    {
        GridCell cell = cells[gridPosition.x, gridPosition.z];
        if (cell.isOccupied)
        {
            Destroy(cell.Building);
            cell.isOccupied = false;
            cell.Building = null;

        }

    }
}
