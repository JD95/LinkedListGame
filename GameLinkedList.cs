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

    public bool listIs(List<string> values)
    {
        GameNode node_ptr = first;

        if (values.Count != length()) return false;

        for (int i = 0; i < values.Count; i++)
        {
            if (values[i] != node_ptr.nodeValue) return false;
            node_ptr = node_ptr.next;
        }

        return true;
    }

    public override string ToString()
    {
        string message = "";
        GameNode node_ptr = first;

        if (node_ptr == null) return "[]";

        message = "[";

        while (node_ptr != null)
        {
            if (node_ptr.next == null)
                message += node_ptr.nodeValue + "]";
            else
                message += node_ptr.nodeValue + ", ";

            node_ptr = node_ptr.next;
        }

        return message;
    }

}
