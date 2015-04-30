using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Maze : MonoBehaviour {

    public MazePassage passage_prefab;
    public MazeWall[] wall_prefabs;
    public MazeDoor doorPrefab;
    public MazeRoomSettings[] room_settings;

    public IntVector2 size;
    public MazeCell cellPrefab;
    public float generationStepDelay;
    [Range(0f, 1f)] public float door_probability;

    private MazeCell[,] cells;
    private List<MazeRoom> rooms = new List<MazeRoom>();

    private MazeRoom CreateRoom(int indexToExclude)
    {
        MazeRoom newRoom = ScriptableObject.CreateInstance<MazeRoom>();
        newRoom.settingsIndex = Random.Range(0, room_settings.Length);
        if (newRoom.settingsIndex == indexToExclude)
        {
            newRoom.settingsIndex = (newRoom.settingsIndex + 1) % room_settings.Length;
        }
        newRoom.settings = room_settings[newRoom.settingsIndex];
        rooms.Add(newRoom);
        return newRoom;
    }

    public MazeCell GetCell(IntVector2 coordinates)
    {
        return cells[coordinates.x, coordinates.y];
    }

    void Start()
    {
        StartCoroutine(Generate());
    }

    public IEnumerator Generate()
    {
        cells = new MazeCell[size.x, size.y];
        List<MazeCell> activeCells = new List<MazeCell>();
        DoFirstGenerationStep(activeCells);
        while (activeCells.Count > 0)
        {
            yield return new WaitForSeconds(generationStepDelay);
            DoNextGenerationStep(activeCells);
        }
    }

    private void DoFirstGenerationStep(List<MazeCell> activeCells)
    {
        MazeCell newCell = CreateCell(RandomCoordinates);
        newCell.Initialize(CreateRoom(-1));
        activeCells.Add(newCell);
    }

    private void CreatePassageInSameRoom(MazeCell cell, MazeCell otherCell, MazeDirection direction)
    {
        MazePassage passage = (MazePassage) Instantiate(passage_prefab);
        passage.Initialize(cell, otherCell, direction);
        passage = (MazePassage) Instantiate(passage_prefab);
        passage.Initialize(otherCell, cell, direction.GetOpposite());
        if (cell.room != otherCell.room)
        {
            MazeRoom roomToAssimilate = otherCell.room;
            cell.room.Assimilate(roomToAssimilate);
            rooms.Remove(roomToAssimilate);
            Destroy(roomToAssimilate);
        }
    }

    private void DoNextGenerationStep(List<MazeCell> activeCells)
    {
        int currentIndex = activeCells.Count - 1;
        //int currentIndex = Mathf.RoundToInt(activeCells.Count / 2);
        //int currentIndex = Random.Range(0, activeCells.Count);
        MazeCell currentCell = activeCells[currentIndex];
        if (currentCell.IsFullyInitialized)
        {
            activeCells.RemoveAt(currentIndex);
            return;
        }
        MazeDirection direction = currentCell.RandomUninitializedDirection;
        IntVector2 coordinates = currentCell.coordinates + direction.ToIntVector2();
        if (ContainsCoordinates(coordinates))
        {
            MazeCell neighbor = GetCell(coordinates);
            if (neighbor == null)
            {
                neighbor = CreateCell(coordinates);
                CreatePassage(currentCell, neighbor, direction);
                activeCells.Add(neighbor);
            }
            else if (currentCell.room.settingsIndex == neighbor.room.settingsIndex)
                CreatePassageInSameRoom(currentCell, neighbor, direction);
            else
                CreateWall(currentCell, neighbor, direction);
        }
        else
            CreateWall(currentCell, null, direction);
    }

    private void CreatePassage(MazeCell cell, MazeCell otherCell, MazeDirection direction)
    {
        MazePassage prefab = Random.value < door_probability ? doorPrefab : passage_prefab; 
        MazePassage passage = (MazePassage) Instantiate(prefab);
        passage.Initialize(cell, otherCell, direction);
        passage = (MazePassage)Instantiate(prefab);
        if (passage is MazeDoor)
            otherCell.Initialize(CreateRoom(cell.room.settingsIndex));
        else
            otherCell.Initialize(cell.room);
        passage.Initialize(otherCell, cell, direction.GetOpposite());
    }

    private void CreateWall(MazeCell cell, MazeCell otherCell, MazeDirection direction)
    {
        MazeWall wall = (MazeWall)Instantiate(wall_prefabs[Random.Range(0, wall_prefabs.Length)]);
        wall.Initialize(cell, otherCell, direction);
        if (otherCell != null)
        {
            wall = (MazeWall)Instantiate(wall_prefabs[Random.Range(0, wall_prefabs.Length)]);
            wall.Initialize(otherCell, cell, direction.GetOpposite());
        }
    }

    private MazeCell CreateCell(IntVector2 coordinates)
    {
        MazeCell newCell = (MazeCell)Instantiate(cellPrefab);
        cells[coordinates.x, coordinates.y] = newCell;
        newCell.coordinates = coordinates;
        newCell.name = "MazeCell " + coordinates.x + ", " + coordinates.y;
        newCell.transform.parent = transform;
        newCell.transform.localPosition = new Vector3(coordinates.x - size.x * .5f + .5f, coordinates.y - size.y * .5f + .5f, 0f);
        return newCell;
    }

    public IntVector2 RandomCoordinates
    {
        get {
            return new IntVector2(Random.Range(0, size.x), Random.Range(0, size.y));
        }
    }

    public bool ContainsCoordinates(IntVector2 coordinate)
    {
        return coordinate.x >= 0 && coordinate.x < size.x && coordinate.y > 0 && coordinate.y < size.y;
    }
}
