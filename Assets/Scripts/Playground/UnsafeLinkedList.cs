using System;
using System.Runtime.InteropServices;
using UnityEngine;


public class UnsafeLinkedList
{
    unsafe public struct Node
    {
        public int data;
        public Node* next;
    }

    //
    unsafe static Node* CreateNode(int data)
    {
        Node* node = (Node*) Marshal.AllocHGlobal(sizeof(Node));
        node->data = data;
        return node;
    }

#region to implement later
    unsafe static void InsertNode()
    {

    }

    unsafe static void DeleteNode()
    {

    }
#endregion

    unsafe static void PrintLinkedList(Node* head)
    {
        Node* tmp = head;

        int i = 0;
        while (tmp != null)
        {

            print(tmp->data);
            ++i;
            tmp = tmp->next;
        }
    }

    unsafe static void DesroyLinkedList(Node* head)
    {
        Node* tmp = head;
        while (tmp != null)
        {
            tmp = head;
            head = head->next;
            Marshal.FreeHGlobal((IntPtr) tmp);
        }
    }

    // [RuntimeInitializeOnLoadMethod]
    unsafe static void Main(string[] args)
    {
        // create a singly linked list:
        // 1 -> 2 -> 3 -> 4 -> 5
        Node* head = CreateNode(1);
        print(head->data);
        Node* second = CreateNode(2);

        // 1 -> 2
        head->next = second;
        Node* tmp = second;

        for (int i = 3; i < 5; ++i)
        {
            // create new node
            Node* newNode = CreateNode(i);
            // print(newNode->data);

            // link prev node to this node
            tmp->next = newNode;
            print("tmp next data" + tmp->next->data);
            tmp = tmp->next;
        }

        tmp = head;
        print("head->data: "+ head->data);

        if (tmp == null) return;

        for (int i = 0; i < 5; ++i)
        {
            if (tmp->next == null) { return; }
            print(tmp->data + " at i = " + i);
        }


        // PrintLinkedList(head);


    }

    static void print(object msg) => Debug.Log(msg);
}


