using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDataStructures
{
    public class MyLinkedList<T> : ILinkedList<T> where T : IEquatable<T>
    {
        MyLinkedListNode<T>? head;
        MyLinkedListNode<T>? tail;

        MyLinkedListNode<T>? ILinkedList<T>.GetHeadNode()
        {
            return head;
        }

        MyLinkedListNode<T>? ILinkedList<T>.GetTailNode()
        {
            return tail;
        }

        public int Count { get; private set; } = 0;

        public bool IsReadOnly { get; private set; } = false;

        public T this[int index]
        {
            // Do this last
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        MyLinkedListNode<T> at(int index)
        {
            if (index < 0 || index > Count) throw new IndexOutOfRangeException();

            var currNode = head;
            for (int i = 0; i < index; i++)
            {
                currNode = currNode.Next;
                if (currNode == null) throw new Exception("Unreachable");
            }

            return currNode;
        }

        /// <summary>
        /// Add a new node to the beginning of the list
        /// </summary>
        /// <param name="item"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void AddToBeginning(T item)
        {
            if (head == null)
            {
                if (tail != null)
                {
                    throw new Exception("List not structured correctly");
                }

                head = new MyLinkedListNode<T>(item);
                tail = head;
            }
            else if (head != tail)
            {
                if (head.Next == null || tail == null || tail.Prev == null)
                {
                    throw new Exception("List not structured correctly");
                }

                MyLinkedListNode<T> new_head = new MyLinkedListNode<T>(item, head);
                head.Prev = new_head;
                head = new_head;
            }
            else
            {
                if (head.Next != null || head.Prev != null)
                {
                    throw new Exception("Only one node in list, but is referencing another node.");
                }

                tail = head;
                head = new MyLinkedListNode<T>(item, tail);
                tail.Prev = head;
            }

            Count++;
        }

        MyLinkedListNode<T>? Find(T item)
        {
            MyLinkedListNode<T>? currNode = head;
            while (currNode != null)
            {
                if (currNode.Item.Equals(item)) return currNode;
                currNode = currNode.Next;
            }

            return null;
        }

        public bool Contains(T item)
        {
            return Find(item) != null;
        }

        /// <summary>
        /// Call Add To Beginning
        /// </summary>
        /// <param name="item"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void Add(T item)
        {
            AddToBeginning(item);
        }

        public void AddToEnd(T item)
        {
            if (tail == null)
            {
                tail = new MyLinkedListNode<T>(item);
                head = tail;
            }
            else if (tail != head)
            {
                if (tail.Prev == null || head == null || head.Next == null)
                {
                    throw new Exception("List not structured correctly");
                }

                MyLinkedListNode<T> new_tail = new MyLinkedListNode<T>(item, null, head);
                tail.Next = new_tail;
            }
            else
            {
                tail = new MyLinkedListNode<T>(item, null, head);
                head.Next = tail;
            }

            Count++;
        }

        public void Clear()
        {
            // I am assuming that GC does the rest (not great preformance here)
            head = null;
            tail = null;
            Count = 0;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            var currentNode = head;
            while (currentNode != null)
            {
                array[arrayIndex] = currentNode.Item;
                arrayIndex++;
                currentNode = currentNode.Next;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public bool Remove(T item)
        {
            var nodeToRemove = Find(item);
            if (nodeToRemove == null) return false;

            if (nodeToRemove.Prev != null)
            {
                nodeToRemove.Prev.Next = nodeToRemove.Next;
            }
            else
            {
                head = nodeToRemove.Next;
            }
            if (nodeToRemove.Next != null)
            {
                nodeToRemove.Next.Prev = nodeToRemove.Prev;
            }
            else
            {
                tail = nodeToRemove.Prev;
            }

            Count--;
            return true;
        }

        public int IndexOf(T item)
        {
            int count = 0;
            MyLinkedListNode<T>? currNode = head;
            while (currNode != null)
            {
                if (currNode.Item.Equals(item)) return count;
                currNode = currNode.Next;
                count++;
            }

            return -1;
        }

        public void Insert(int index, T item)
        {
            if (index < 0 || index > Count) throw new IndexOutOfRangeException();

            if (index == 0)
            {
                AddToBeginning(item);
                return;
            }
            else if (index == Count)
            {
                AddToEnd(item);
                return;
            }

            var nodeToMove = at(index);

            var newNode = new MyLinkedListNode<T>(item, nodeToMove.Prev, nodeToMove);

            // We don't need to worry about the case where Next or Prev should
            // be null, as we have checked that index != 0 and index != count
            nodeToMove.Prev.Next = newNode;
            nodeToMove.Prev = newNode;

            Count++;
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index > Count) throw new IndexOutOfRangeException();

            var nodeToRemove = at(index);

            if (nodeToRemove == head)
            {
                head = head.Next;
            } else
            {
                nodeToRemove.Prev.Next = nodeToRemove.Next;
            }

            if (nodeToRemove == tail)
            {
                tail = tail.Prev;
            } else
            {
                nodeToRemove.Next.Prev = nodeToRemove.Prev;
            }

            Count--;
        }

    }
}
