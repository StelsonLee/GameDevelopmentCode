/*
 * Name: Lee Kong Hue
 * ID: 1303181
 * Course: GV
 * 
 * Description
 * To store the gCost and hCost, walkable or not walkable, and the position 
 * of each node and allow comparing with others node fCost and hCost
 */
using UnityEngine;
using System.Collections;
using System;

public class Node : IHeapItem<Node>
{
    public bool walkable; //check walkable for ai or not
    public Vector3 worldPosition; //get the node in world position
    public int gridX; //number of the grid position
    public int gridY; //number of the grid position

    public int gCost; 
    public int hCost;
    public Node parent;
    int heapIndex;

    //store each node information in each grid on game world
    public Node(bool _walkable, Vector3 _worldPosition, int _gridX, int _gridY)
    {
        walkable = _walkable;
        worldPosition = _worldPosition;
        gridX = _gridX;
        gridY = _gridY;
    }

    public int fCost
    {
        get
        {
            return gCost + hCost;
        }
    }

    public int HeapIndex
    {
        get
        {
            return heapIndex;
        }
        set
        {
            HeapIndex = value;
        }
    }

    public int HeapIdex
    {
        get
        {
            return heapIndex;
        }

        set
        {
            heapIndex = value;
        }
    }

    //compare two different node fcost and hcost to get shortest path
    public int CompareTo(Node nodeToCompare)
    {
        int compare = fCost.CompareTo(nodeToCompare.fCost);

        if (compare == 0)
        {
            compare = hCost.CompareTo(nodeToCompare.hCost);
        }
        return -compare;
    }
}
