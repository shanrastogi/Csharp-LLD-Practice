using System;
using System.Collections.Generic;

namespace LFUCacheApp
{
    // To implement a node in doubly linked list that will store data items
    public class Node
    {
        public int key, value, cnt;
        public Node next;
        public Node prev;

        public Node(int _key, int _value)
        {
            key = _key;
            value = _value;
            cnt = 1;
        }
    }

    // To implement the doubly linked list
    public class DoublyLinkedList
    {
        public int size; // Size 
        public Node head; // Dummy head
        public Node tail; // Dummy tail

        // Constructor
        public DoublyLinkedList()
        {
            head = new Node(0, 0);
            tail = new Node(0, 0);
            head.next = tail;
            tail.prev = head;
            size = 0;
        }

        // Function to add node in front 
        public void AddFront(Node node)
        {
            Node temp = head.next;
            node.next = temp;
            node.prev = head;
            head.next = node;
            temp.prev = node;
            size++;
        }

        // Function to remove node from the list
        public void RemoveNode(Node delnode)
        {
            Node prevNode = delnode.prev;
            Node nextNode = delnode.next;
            prevNode.next = nextNode;
            nextNode.prev = prevNode;
            size--;
        }
    }

    // Class to implement LFU cache
    public class LFUCache
    {
        // Dictionary to store the key-nodes pairs
        private Dictionary<int, Node> keyNode = new Dictionary<int, Node>();

        // Dictionary to maintain the lists having different frequencies
        private Dictionary<int, DoublyLinkedList> freqListMap = new Dictionary<int, DoublyLinkedList>();

        private int maxSizeCache; // Max size of cache
        private int minFreq;      // To store the frequency of least frequently used data-item
        private int curSize;      // To store current size of cache

        // Constructor
        public LFUCache(int capacity)
        {
            maxSizeCache = capacity;
            minFreq = 0;
            curSize = 0;
        }

        // Method to update frequency of data-items
        public void UpdateFreqListMap(Node node)
        {
            // Remove from Dictionary
            keyNode.Remove(node.key);

            // Update the frequency list dictionary
            freqListMap[node.cnt].RemoveNode(node);

            // If node was the last node having its frequency
            if (node.cnt == minFreq && freqListMap[node.cnt].size == 0)
            {
                // Update the minimum frequency
                minFreq++;
            }

            // Create or get the list for the next higher frequency
            if (!freqListMap.TryGetValue(node.cnt + 1, out DoublyLinkedList? nextHigherFreqList))
            {
                nextHigherFreqList = new DoublyLinkedList();
            }

            // Increment the count of data-item
            node.cnt += 1;

            // Add the node in front of higher frequency list
            nextHigherFreqList.AddFront(node);

            // Update the maps
            freqListMap[node.cnt] = nextHigherFreqList;
            keyNode[node.key] = node;
        }

        // Method to get the value of key from LFU cache
        public int Get(int key)
        {
            // Return the value if key exists
            if (keyNode.TryGetValue(key, out Node? node))
            {
                int val = node.value; // Get the value
                UpdateFreqListMap(node); // Update the frequency

                // Return the value
                return val;
            }

            // Return -1 if key is not found
            return -1;
        }

        public void Put(int key, int value)
        {
            /* If the size of Cache is 0, 
               no data-items can be inserted */
            if (maxSizeCache == 0)
            {
                return;
            }

            // If key already exists
            if (keyNode.TryGetValue(key, out Node? node))
            {
                // Update the value
                node.value = value;

                // Update the frequency
                UpdateFreqListMap(node);
            }
            // Else if the key does not exist
            else
            {
                // If cache limit is reached
                if (curSize == maxSizeCache)
                {
                    // Remove the least frequently used data-item
                    DoublyLinkedList list = freqListMap[minFreq];
                    keyNode.Remove(list.tail.prev.key);

                    // Update the frequency map 
                    list.RemoveNode(list.tail.prev);

                    // Decrement the current size of cache
                    curSize--;
                }

                // Increment the current cache size
                curSize++;

                // Adding new value to the cache
                minFreq = 1; // Set its frequency to 1

                // Create or get the list for frequency 1
                if (!freqListMap.TryGetValue(minFreq, out DoublyLinkedList? listFreq))
                {
                    listFreq = new DoublyLinkedList();
                }

                // Create the node to store data-item
                Node newNode = new Node(key, value);

                // Add the node to dummy list
                listFreq.AddFront(newNode);

                // Add the node to Dictionary
                keyNode[key] = newNode;

                // Update the frequency list map 
                freqListMap[minFreq] = listFreq;
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // LFU Cache
            LFUCache cache = new LFUCache(2);

            // Queries
            cache.Put(1, 1);
            cache.Put(2, 2);
            Console.Write(cache.Get(1) + " ");
            cache.Put(3, 3);
            Console.Write(cache.Get(2) + " ");
            Console.Write(cache.Get(3) + " ");
            cache.Put(4, 4);
            Console.Write(cache.Get(1) + " ");
            Console.Write(cache.Get(3) + " ");
            Console.Write(cache.Get(4) + " ");

            // Expected Output: 1 -1 3 -1 3 4
            Console.WriteLine();
        }
    }
}