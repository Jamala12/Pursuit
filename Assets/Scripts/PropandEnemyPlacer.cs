using System;
using System.Collections.Generic;
using UnityEngine;
public class PropAndEnemyPlacer : MonoBehaviour
{
    [SerializeField]
    private GameObject[] propPrefabs;
    [SerializeField]
    private List<EnemyGroup> enemyGroups;
    [SerializeField]
    private int maxProps;
    [SerializeField]
    private float minDistanceFromStart;
    private List<GameObject> spawnedObjects = new List<GameObject>();

    public void ClearObjects()
    {
        foreach (GameObject obj in spawnedObjects)
        {
            if (Application.isPlaying)
            {
                Destroy(obj);
            }
            else
            {
                DestroyImmediate(obj);
            }
        }
        spawnedObjects.Clear();
    }

    public void PlacePropsAndEnemies(HashSet<Vector2Int> roomFloor, Vector2Int position, PropsAndEnemiesPreset preset)
    {
        List<Vector2Int> validPositions = new List<Vector2Int>(roomFloor);
        validPositions.RemoveAll(pos => Vector2.Distance(new Vector2(pos.x, pos.y), new Vector2(position.x, position.y)) < preset.minDistanceFromStart);

        RandomizeList(validPositions);

        PlaceProps(validPositions, roomFloor, preset);

        if (preset.enemyGroups != null)
        {
            foreach (EnemyGroup group in preset.enemyGroups)
            {
                // It's important to either pass a clone of validPositions or ensure PlaceGroupInFormation does not modify it
                PlaceGroupInFormation(new List<Vector2Int>(validPositions), group);
            }
        }
    }

    public void PlaceProps(List<Vector2Int> validPositions, HashSet<Vector2Int> roomFloor, PropsAndEnemiesPreset preset)
    {
        System.Random rng = new System.Random();
        int propsPlaced = 0;

        foreach (Vector2Int pos in validPositions)
        {
            if (propsPlaced >= preset.maxProps) break;
            GameObject propPrefab = preset.propPrefabs[rng.Next(preset.propPrefabs.Length)];
            Vector3 position = new Vector3(pos.x + 0.5f, pos.y + 0.5f, 0);
            GameObject prop = Instantiate(propPrefab, position, Quaternion.identity);

            if (IsSpaceAvailable(pos, prop, roomFloor))
            {
                spawnedObjects.Add(prop);
                propsPlaced++;
            }
            else
            {
                DestroyImmediate(prop);
                Debug.Log($"Failed to place prop at {pos} due to space constraints");
            }
        }
    }


    private bool IsSpaceAvailable(Vector2Int position, GameObject prop, HashSet<Vector2Int> floorPositions)
{
        Renderer renderer = prop.GetComponent<Renderer>();
        if (renderer != null)
        {
            Vector3 size = renderer.bounds.size;
            int requiredHeight = Mathf.CeilToInt(size.y);
            int requiredWidth = Mathf.CeilToInt(size.x);

            // Check all tiles that the prop would occupy
            for (int h = 0; h < requiredHeight; h++) // height check
            {
                for (int w = 0; w < requiredWidth; w++) // width check
                {
                    Vector2Int checkPos = new Vector2Int(position.x + w, position.y - h);
                    if (!floorPositions.Contains(checkPos))
                    {
                        return false; // Space required for the prop is not available
                    }
                }
            }
        }
        return true;
    }


    private void PlaceGroupInFormation(List<Vector2Int> positions, EnemyGroup group)
    {
        System.Random rng = new System.Random();
        Vector2Int roomCenter = CalculateRoomCenter(new HashSet<Vector2Int>(positions));
        Vector3 centerPosition = new Vector3(roomCenter.x + 0.5f, roomCenter.y + 0.5f, 0);

        switch (group.formation)
        {
            case "cluster":
                PlaceClusterFormation(positions, group, centerPosition, rng);
                break;
            case "centre":
                PlaceCentreFormation(group, centerPosition);
                break;
            case "circle":
                PlaceCircleFormation(positions, group, centerPosition, rng);
                break;
            default:
                PlaceDefaultFormation(positions, group, rng);  // Default formation as originally handled
                break;
        }
    }

    private void PlaceDefaultFormation(List<Vector2Int> positions, EnemyGroup group, System.Random rng)
    {
        // This replicates the original placement logic for any unspecified formations
        int startIndex = rng.Next(positions.Count);
        for (int i = 0; i < group.groupSize && positions.Count > startIndex; i++)
        {
            Vector3 position = new Vector3(positions[startIndex].x + 0.5f, positions[startIndex].y + 0.5f, 0);
            GameObject enemy = Instantiate(group.enemyTypes[i % group.enemyTypes.Length], position, Quaternion.identity);
            spawnedObjects.Add(enemy);
            positions.RemoveAt(startIndex);
        }
    }


    private void PlaceClusterFormation(List<Vector2Int> positions, EnemyGroup group, Vector3 centerPosition, System.Random rng)
    {
        // Adjust cluster radius as needed
        float clusterRadius = 1.5f;
        for (int i = 0; i < group.groupSize; i++)
        {
            Vector3 offset = UnityEngine.Random.insideUnitCircle * clusterRadius;
            Vector3 spawnPosition = centerPosition + offset;
            GameObject enemy = Instantiate(group.enemyTypes[i % group.enemyTypes.Length], spawnPosition, Quaternion.identity);
            spawnedObjects.Add(enemy);
        }
    }

    private void PlaceCentreFormation(EnemyGroup group, Vector3 centerPosition)
    {
        for (int i = 0; i < group.groupSize; i++)
        {
            GameObject enemy = Instantiate(group.enemyTypes[i % group.enemyTypes.Length], centerPosition, Quaternion.identity);
            spawnedObjects.Add(enemy);
        }
    }

    private void PlaceCircleFormation(List<Vector2Int> positions, EnemyGroup group, Vector3 centerPosition, System.Random rng)
    {
        float radius = 2.0f; // Adjust the radius as needed
        float angleStep = 360f / group.groupSize;

        for (int i = 0; i < group.groupSize; i++)
        {
            float angle = i * angleStep;
            Vector3 offset = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0) * radius;
            Vector3 spawnPosition = centerPosition + offset;
            GameObject enemy = Instantiate(group.enemyTypes[i % group.enemyTypes.Length], spawnPosition, Quaternion.identity);
            spawnedObjects.Add(enemy);
        }
    }

    private Vector2Int CalculateRoomCenter(HashSet<Vector2Int> positions)
    {
        if (positions.Count == 0)
            return Vector2Int.zero;

        long sumX = 0, sumY = 0;
        foreach (Vector2Int pos in positions)
        {
            sumX += pos.x;
            sumY += pos.y;
        }
        return new Vector2Int((int)(sumX / positions.Count), (int)(sumY / positions.Count));
    }



    private void RandomizeList(List<Vector2Int> list)
    {
        System.Random rng = new System.Random();
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            Vector2Int value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}