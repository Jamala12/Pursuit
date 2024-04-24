using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Defines an abstract base class for dungeon generators, providing a framework for procedural dungeon generation.
public abstract class AbstractDungeonGenerator : MonoBehaviour
{
    [SerializeField]
    protected TilemapVisualizer tilemapVisualizer = null; // Reference to the TilemapVisualizer to handle the visual representation of the dungeon.

    [SerializeField]
    protected Vector2Int startPosition = Vector2Int.zero; // Starting position for dungeon generation.

    // Public method to start the dungeon generation process.
    public void GenerateDungeon()
    {
        tilemapVisualizer.Clear(); // Clears any existing dungeon visualizations.
        RunProceduralGeneration(); // Calls the overridden method to generate the dungeon procedurally.
    }

    // Abstract method that must be implemented by subclasses to define specific dungeon generation algorithms.
    protected abstract void RunProceduralGeneration();
}