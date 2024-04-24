using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CorridorFirstDungeonGenerator : RandomWalkMapGenerator
{
    [SerializeField]
    private int corridorLength = 30; // Defines the length of each corridor.
    [SerializeField]
    private int corridorCount = 10; // Defines the number of corridors to generate.
    [SerializeField]
    [Range(0.1f, 1)]
    private float roomPercent = 0.8f; // Percentage of potential room positions to actually turn into rooms.

    protected override void RunProceduralGeneration()
    {
        CorridorFirstGenerator(); // Primary method that coordinates the dungeon generation.
    }

    private void CorridorFirstGenerator()
    {
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>(); // Tracks positions of all floor tiles.
        HashSet<Vector2Int> potentialRoomPositions = new HashSet<Vector2Int>(); // Tracks potential room positions.

        List<List<Vector2Int>> corridors = CreateCorridors(floorPositions, potentialRoomPositions); // Generate corridors.

        HashSet<Vector2Int> roomPositions = CreateRooms(potentialRoomPositions); // Generate rooms based on potential positions.

        List<Vector2Int> deadEnds = FindAllDeadEnds(floorPositions); // Identify all dead ends in the corridors.

        CreateRoomsAtDeadEnd(deadEnds, roomPositions); // Optionally add rooms at dead ends.

        // Increase corridor size for visual and gameplay purposes.
        for (int i = 0; i < corridors.Count; i++)
        {
            corridors[i] = IncreaseCorridorSize(corridors[i]);
            floorPositions.UnionWith(corridors[i]); // Add the updated corridors to floor positions.
        }

        floorPositions.UnionWith(roomPositions); // Combine room positions with floor positions.

        tilemapVisualizer.PaintFloorTiles(floorPositions); // Visualize floor tiles.
        WallGenerator.CreateWalls(floorPositions, tilemapVisualizer); // Generate walls around the floor tiles.
         // ------------------------------------------------ FINISH COMMENTING! -------------------------------------------

    }

    private List<Vector2Int> IncreaseCorridorSize(List<Vector2Int> corridor)
    {
        List<Vector2Int> newCorridor = new List<Vector2Int>();
        for (int i = 1; i < corridor.Count; i++)
        {
            for (int x = -1; x < 2; x++)
            {
                for (int y = -1; y < 2; y++)
                {
                    newCorridor.Add(corridor[i - 1] + new Vector2Int(x, y));
                }
            }
        }
        return newCorridor;
    }

    private void CreateRoomsAtDeadEnd(List<Vector2Int> deadEnds, HashSet<Vector2Int> roomFloors)
    {
        foreach (var position in deadEnds)
        {
            if(roomFloors.Contains(position) == false)
            {
                var room = RunRandomWalk(randomWalkParameters, position);
                roomFloors.UnionWith(room);
            }
        }
    }

    private List<Vector2Int> FindAllDeadEnds(HashSet<Vector2Int> floorPositions)
    {
        List<Vector2Int> deadEnds = new List<Vector2Int>();
        foreach (var position in floorPositions)
        {
            int neighboursCount = 0;
            foreach (var direction in Direction2D.cardinalDirectionsList)
            {
                if (floorPositions.Contains(position + direction))
                {
                    neighboursCount++;
                }
                
            }
            if (neighboursCount == 1)
            {
                deadEnds.Add(position);
            }
        }
        return deadEnds;
    }

    private HashSet<Vector2Int> CreateRooms(HashSet<Vector2Int> potentialRoomPositions)
    {
        HashSet<Vector2Int> roomPositions = new HashSet<Vector2Int>();
        int roomToCreateCount = Mathf.RoundToInt(potentialRoomPositions.Count* roomPercent);

        List<Vector2Int> roomsToCreate = potentialRoomPositions.OrderBy(x => Guid.NewGuid()).Take(roomToCreateCount).ToList();

        foreach (var roomPosition in roomsToCreate)
        {
            var roomFloor = RunRandomWalk(randomWalkParameters, roomPosition);
            roomPositions.UnionWith(roomFloor);
        }
        return roomPositions;
        
    }

    private List<List<Vector2Int>> CreateCorridors(HashSet<Vector2Int> floorPositions, HashSet<Vector2Int> potentialRoomPositions)
    {
        var currentPosition = startPosition;
        potentialRoomPositions.Add(currentPosition);
        List<List<Vector2Int>> corridors = new List<List<Vector2Int>>();

        for (int i = 0; i < corridorCount; i++)
        {
            var corridor = ProcedralGenerationAlgorithm.RandomWalkCorridor(currentPosition, corridorLength);
            corridors.Add(corridor);
            currentPosition = corridor[corridor.Count - 1];
            potentialRoomPositions.Add(currentPosition);
            floorPositions.UnionWith(corridor);
        }
        return corridors;
    }
}
