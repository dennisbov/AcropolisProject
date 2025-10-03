using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Rendering;

[ExecuteInEditMode]
public class DistansesFileCreator : MonoBehaviour
{
    [SerializeField] private CreateTileGraph CreateTileGraph;
    [SerializeField] private bool createFile;
    [SerializeField] private int BFSLimit;
    [SerializeField] private bool checkFile;

    private int desiredVertex = 83;
    List<int> markedVertecies;
    private void OnValidate()
    {
        if (createFile)
        {
            List<byte> numbers = new List<byte>();
            foreach(TileGraphVertex tileFrom in CreateTileGraph.tileGraphVertices)
            {
                foreach (TileGraphVertex tileTo in CreateTileGraph.tileGraphVertices)
                {
                    numbers.Add((byte) CalculateDistance(tileFrom.id, tileTo.id));
                }
            }
            SaveNumbersBinary(numbers.ToArray(), CreateTileGraph.tileGraphVertices.Count);
        }
        if (checkFile)
        {

        }
    }
    public void SaveNumbersBinary(byte[] numbers, int vertexCount)
    {
        string streamingAssetsPath = Application.streamingAssetsPath;

        if (!Directory.Exists(streamingAssetsPath))
        {
            Directory.CreateDirectory(streamingAssetsPath);
        }

        string path = Path.Combine(Application.streamingAssetsPath, "distances.bin");
        using (BinaryWriter writer = new BinaryWriter(File.Open(path, FileMode.Create)))
        {
            writer.Write(numbers.Length);
            writer.Write(vertexCount);
            foreach (byte number in numbers)
            {
                writer.Write(number);
            }
        }
        Debug.Log($"Статичный файл создан: {path}");
    }

    private int CalculateDistance(int from, int to)
    {
        if(from == to)
        {
            return 0;
        }
        desiredVertex = to;
        markedVertecies = new List<int>();
        List<int> startList = new List<int>() { from };
        return BFS(startList, 1);
    }

    
    
    private int BFS(List<int> verticies, int depth)
    {
        List<int> future = new List<int>();
        if(depth > BFSLimit)
        {
            Debug.LogError("can't find path to " + desiredVertex.ToString());
            return 0;
        }
        foreach(int vertex in verticies)
        {
            foreach(TileGraphVertex tile in CreateTileGraph.tileGraphVertices[vertex].neighbourVertices)
            {
                if(tile.id == desiredVertex)
                {
                    return depth;
                }
                if(markedVertecies.Contains(tile.id) == false)
                {
                    future.Add(tile.id);
                    markedVertecies.Add(tile.id);
                }
            }
        }
        return BFS(future, depth + 1);
    }
}
