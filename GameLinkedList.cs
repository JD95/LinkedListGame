using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class GameLinkedList
{
    public GameNode first;
    public GameNode last;

    public GameNode find(GameObject cubeToFind, List<GameNode> otherNodes)
    {
        var searchNode = first;

        while (searchNode != null && searchNode.value != cubeToFind)
            searchNode = searchNode.next;

        if (searchNode == null)
            searchNode = otherNodes.Where(n => n.value == cubeToFind)
                                    .FirstOrDefault();

        return searchNode;
    }

    public int length()
    {
        int count = 0;
        var searchNode = first;

        while (searchNode != null)
        {
            searchNode = searchNode.next;
            count++;
        }

        return count;
    }

    public bool listIs(List<int> values)
    {
        GameNode ptr = first;

        for (int i = 0; i < values.Count; i++)
        {

            if (ptr == null)
                return false;

            if (ptr.nodeValue != values.ElementAt(i))
                return false;
        }

        return true;
    }
}
