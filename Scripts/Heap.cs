/*
 * Name : Lee Kong Hue
 * ID   : 1303181
 * 
 * Description
 * To optimize path finding by using Heap sorting methods
 */
using UnityEngine;
using System.Collections;
using System;

public class Heap<T> where T : IHeapItem<T>
{
    T[] items;
    int currentItemCount;
	
    public Heap(int maxHeapSize)
    {
        items = new T[maxHeapSize];
    }

    public void Add(T item)
    {
        item.HeapIdex = currentItemCount;
        items[currentItemCount] = item;
        SortUp(item);
        currentItemCount++;
    }

    public T RemoveFirst()
    {
        T firstItem = items[0];
        currentItemCount--;
        items[0] = items[currentItemCount];
        items[0].HeapIdex = 0;
        SortDown(items[0]);

        return firstItem;
    }

    public void UpdateItem(T item)
    {
        SortUp(item);
    }

    public int Count
    {
        get
        {
            return currentItemCount;
        }
    }

    public bool Contains(T item)
    {
        return Equals(items[item.HeapIdex], item);
    }

    void SortDown(T item)
    {
        while (true)
        {
            int childIndexLeft = item.HeapIdex * 2 + 1;
            int childIndextRight = item.HeapIdex * 2 + 2;
            int swapIndex = 0;

            if (childIndexLeft < currentItemCount)
            {
                swapIndex = childIndexLeft;

                if (childIndextRight < currentItemCount)
                {
                    if (items[childIndexLeft].CompareTo(items[childIndextRight]) < 0)
                    {
                        swapIndex = childIndextRight;
                    }
                }

                if (item.CompareTo(items[swapIndex]) < 0)
                {
                    Swap(item, items[swapIndex]);
                }
                else
                {
                    return;
                }
            }
            else
            {
                return;
            }
        }
    }

    void SortUp(T item)
    {
        int parentIndex = (item.HeapIdex - 1) / 2;

        while (true)
        {
            T parentItem = items[parentIndex];
            if (item.CompareTo(parentItem) > 0)
            {
                Swap(item, parentItem);
            }
            else
            {
                break;
            }

            parentIndex = (item.HeapIdex - 1) / 2;
        }
    }

    void Swap(T itemA, T itemB)
    {
        items[itemA.HeapIdex] = itemB;
        items[itemB.HeapIdex] = itemA;
        int itemAIndex = itemA.HeapIdex;
        itemA.HeapIdex = itemB.HeapIdex;
        itemB.HeapIdex = itemAIndex;

    }
}

public interface IHeapItem<T> : IComparable<T>
{
    int HeapIdex
    {
        get;
        set;
    }
}
