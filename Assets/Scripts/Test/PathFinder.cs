using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PathFinder
{
    private List<TileGraphVertex> tileGraphVertices;
    private List<int> obstacleId;
    private List<WayPoint> avaliableWaypoints;
    public PathFinder(List<TileGraphVertex> tileGraphVertices, List<int> obstacleId, int startTileId, int range) 
    {
        this.tileGraphVertices = tileGraphVertices;
        this.obstacleId = obstacleId;
        GenerateAlailableTiles(startTileId, range);
    }

    public bool GetPathTo(int target, out List<int> path)
    {
        path = new List<int>();

        WayPoint currentWayPoint = new WayPoint(target);
        if (avaliableWaypoints.Contains(currentWayPoint) == false)
        {
            return false;
        }
        currentWayPoint = avaliableWaypoints[avaliableWaypoints.IndexOf(currentWayPoint)];
        
        while (currentWayPoint.parent != null)
        {
            path.Add(currentWayPoint.parent.id);
            currentWayPoint = currentWayPoint.parent;
        }
        path.RemoveAt(path.Count - 1); 
        path.Reverse();
        return true;
    }

    public List<int> GetAvailableTiles()
    {
        List<int> result = new List<int>();
        foreach(WayPoint wayPoint in avaliableWaypoints)
        {
            result.Add(wayPoint.id);
        }
        return result;
    }

    public void GenerateAlailableTiles(int startTileId, int range)
    {
        avaliableWaypoints = new List<WayPoint>();

        List<WayPoint> nextWaypoints = new List<WayPoint>();
        List<WayPoint> currentWaypoints = new List<WayPoint>() { new WayPoint(startTileId) };

        for (int i = 0;  i < range; i++)
        {
            foreach(WayPoint wayPoint in currentWaypoints)
            {
                foreach(TileGraphVertex vertex in tileGraphVertices[wayPoint.id].neighbourVertices)
                {
                    WayPoint current = new WayPoint(vertex.id);
                    if (avaliableWaypoints.Contains(current) || obstacleId.Contains(current.id))
                        continue;
                    current.parent = wayPoint;
                    avaliableWaypoints.Add(current);
                    nextWaypoints.Add(current);
                }
            }

            currentWaypoints.Clear();
            foreach (WayPoint waypoint in nextWaypoints)
            {
                currentWaypoints.Add(waypoint);
            }
            nextWaypoints.Clear();
        }
    }

    public List<int> FindPath(int from, int to)
    {
        WayPoint start = new WayPoint(from);
        WayPoint end = new WayPoint(to);

        SortingTree openList = new SortingTree();
        List<int> closedList = new List<int>();
        
        openList.Add(start);

        while (openList.Count() > 0) 
        { 
            WayPoint current = openList.GetMax();

            closedList.Add(current.id);

            foreach(TileGraphVertex tile in tileGraphVertices[current.id].neighbourVertices)
            {
                if (closedList.Contains(tile.id))
                {
                    continue;
                }
                int newG = current.g + 1;

            }
        }
        return null;
    }
    
    private class WayPoint : IComparable
    {
        public int id;
        public int f;
        public WayPoint parent;
        public int g;
        public int h;
        public WayPoint(int id = -1, WayPoint parent = null)
        {
            this.id = id;
            this.f = 0;
            this.parent = parent;
            this.g = 0;
            this.h = 0;
        }

        public int CompareTo(object obj)
        {
            WayPoint other = obj as WayPoint;
            return other.f - this.f;
        }
        public override bool Equals(object obj)
        {
            return obj is WayPoint point &&
                   id == point.id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(id, f, parent, g, h);
        }
    }
    private class SortingTree
    {
        List<WayPoint> heap;

        private WayPoint tmp;
        public SortingTree()
        {
            heap = new List<WayPoint>();
        }

        public void Add(WayPoint key)
        {
            heap.Add(new WayPoint(int.MinValue));
            HeapIncreaseKey(heap.Count+1, key);
        }

        public int Count()
        {
            return heap.Count;
        }

        public WayPoint GetMax()
        {
            if(heap.Count < 1)
            {
                return null;
            }
            WayPoint max = heap[0];
            heap[0] = heap[heap.Count - 1];
            heap.RemoveAt(heap.Count - 1);
            Heapify(0);
            return max;
        } 

        private void Heapify(int index)
        {
            int left = 2 * index + 1;
            int right = 2 * index + 2;
            int largest = index;
            if (left <= heap.Count && heap[left].CompareTo(heap[largest]) > 0)
            {
                largest = left;
            }
            if (right <= heap.Count && heap[right].CompareTo(heap[largest]) > 0)
            {
                largest = right;
            }
            if (largest != index)
            {
                tmp = heap[index];
                heap[index] = heap[largest];
                heap[largest] = tmp;
                Heapify(largest);
            }
        }
        private void HeapIncreaseKey(int index, WayPoint key)
        {
            if (key.CompareTo(heap[index]) < 0)
            {
                Debug.LogError("New key is less that previous");
            }
            heap[index] = key;
            while (index > 1 && heap[(int)MathF.Floor(index / 2f)].CompareTo(heap[index]) > 0)
            {
                tmp = heap[index];
                heap[index] = heap[(int)MathF.Floor(index / 2f)];
                heap[(int)MathF.Floor(index / 2f)] = tmp;
                index = (int)MathF.Floor(index / 2f);
            }
        }
    }
}
