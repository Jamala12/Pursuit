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
        if (placer == null)
        {
            Debug.LogError("Placer is not initialized.");
            return;
        }
        placer.ClearObjects();
        CorridorFirstGenerator(); // Primary method that coordinates the dungeon generation.
    }

    private void CorridorFirstGenerator()
    {
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>(); // Tracks positions of all floor tiles.
        HashSet<Vector2Int> potentialRoomPositions = new HashSet<Vector2Int>(); // Tracks potential room positions.

        List<List<Vector2Int>> corridors = CreateCorridors(floorPositions, potentialRoomPositions); // Generate corridors.

        PropsAndEnemiesPreset selectedPreset = presets[UnityEngine.Random.Range(0, presets.Length)];

        HashSet<Vector2Int> roomPositions = CreateRooms(potentialRoomPositions, presets); // Generate rooms based on potential positions.

        List<Vector2Int> deadEnds = FindAllDeadEnds(floorPositions); // Identify all dead ends in the corridors.

        CreateRoomsAtDeadEnd(deadEnds, roomPositions, presets); // Optionally add rooms at dead ends.

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

    private void CreateRoomsAtDeadEnd(List<Vector2Int> deadEnds, HashSet<Vector2Int> roomFloors, PropsAndEnemiesPreset[] presets)
    {
        foreach (var position in deadEnds)
        {
            if (!roomFloors.Contains(position))
            {
                var room = RunRandomWalk(randomWalkParameters, position);
                roomFloors.UnionWith(room);

                // Select a preset randomly for each room at dead end
                PropsAndEnemiesPreset selectedPreset = presets[UnityEngine.Random.Range(0, presets.Length)];
                placer.PlacePropsAndEnemies(room, position, selectedPreset); // Use the instance here
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

    private HashSet<Vector2Int> CreateRooms(HashSet<Vector2Int> potentialRoomPositions, PropsAndEnemiesPreset[] presets)
    {
        HashSet<Vector2Int> roomPositions = new HashSet<Vector2Int>();
        int roomToCreateCount = Mathf.RoundToInt(potentialRoomPositions.Count * roomPercent);
        List<Vector2Int> roomsToCreate = potentialRoomPositions.OrderBy(x => Guid.NewGuid()).ToList();

        bool isFirstRoom = true;

        foreach (var roomPosition in roomsToCreate.Take(roomToCreateCount))
        {
            var roomFloor = RunRandomWalk(randomWalkParameters, roomPosition);
            roomPositions.UnionWith(roomFloor);

            // Apply the spawn preset to the first room
            PropsAndEnemiesPreset presetToUse = isFirstRoom ? presets[0] : presets[UnityEngine.Random.Range(1, presets.Length)];
            placer.PlacePropsAndEnemies(roomFloor, roomPosition, presetToUse);

            if (isFirstRoom)
            {
                Vector2Int roomCenter = CalculateRoomCenter(roomFloor);
                // Assuming you have a method or a way to access the player's game object
                MovePlayerToSpawn(new Vector3(roomCenter.x, roomCenter.y, 0));
                isFirstRoom = false;  // Mark the first room as initialized
            }
        }
        return roomPositions;
    }

    private Vector2Int CalculateRoomCenter(HashSet<Vector2Int> roomFloor)
    {
        int x = 0, y = 0;
        foreach (var pos in roomFloor)
        {
            x += pos.x;
            y += pos.y;
        }
        return new Vector2Int(x / roomFloor.Count, y / roomFloor.Count); // Average position, essentially the center
    }

    private void MovePlayerToSpawn(Vector3 spawnPosition)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player"); // Make sure your player GameObject has the tag "Player"
        if (player != null)
        {
            player.transform.position = spawnPosition;
        }
        else
        {
            Debug.LogError("Player object not found. Ensure it's tagged correctly in the scene.");
        }
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
