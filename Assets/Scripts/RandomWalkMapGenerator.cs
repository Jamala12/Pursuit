using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomWalkMapGenerator : AbstractDungeonGenerator
{
    [SerializeField]
    protected RandomWalkData randomWalkParameters;
    [SerializeField]
    protected PropAndEnemyPlacer placer;
    [SerializeField]
    protected PropsAndEnemiesPreset[] presets;  // Add this line

    protected override void RunProceduralGeneration()
    {
        placer.ClearObjects();
        HashSet<Vector2Int> floorPositions = RunRandomWalk(randomWalkParameters, startPosition);
        tilemapVisualizer.Clear();
        tilemapVisualizer.PaintFloorTiles(floorPositions);
        WallGenerator.CreateWalls(floorPositions, tilemapVisualizer);
        OnGenerationComplete(floorPositions);
    }

    protected HashSet<Vector2Int> RunRandomWalk(RandomWalkData parameters, Vector2Int position)
    {
        var currentPosition = position;
        HashSet<Vector2Int> floorPosition = new HashSet<Vector2Int>();
        for (int i = 0; i < parameters.iterations; i++)
        {
            var path = ProcedralGenerationAlgorithm.SimpleRandomWalk(currentPosition, parameters.walkLength);
            floorPosition.UnionWith(path);
            if (parameters.startRandomlyEachIteration)
            {
                currentPosition = floorPosition.ElementAt(Random.Range(0, floorPosition.Count));
            }
        }
        return floorPosition;
    }

    private void OnGenerationComplete(HashSet<Vector2Int> floorPositions)
    {
        placer.ClearObjects();
        // Selecting a random preset or defaulting to the first one if no specific logic is needed
        if (presets != null && presets.Length > 0)
        {
            PropsAndEnemiesPreset selectedPreset = presets[UnityEngine.Random.Range(0, presets.Length)];
            placer.PlacePropsAndEnemies(floorPositions, startPosition, selectedPreset);
        }
        else
        {
            Debug.LogWarning("No presets available to place props and enemies.");
            // Handle the case where no presets are available
            // Could potentially create a default preset manually or skip placing props and enemies
        }
    }
}

