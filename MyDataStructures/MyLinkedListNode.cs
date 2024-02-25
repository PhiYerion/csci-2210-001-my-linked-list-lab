namespace MyDataStructures
{
    internal class MyLinkedListNode<T> where T : IEquatable<T>
    {
        internal T Item { get; set; }
        internal MyLinkedListNode<T>? Next { get; set; }
        internal MyLinkedListNode<T>? Prev { get; set; }

        /// <summary>
        /// Contructor for a node in a doubly linked list.
        /// </summary>
        /// <param name="item">Optional. The value contained inside of the node.</param>
        /// <param name="next">Optional. A reference to the node that should come after this node.</param>
        /// <param name="prev">Optional. A reference to the node that should come before this node.</param>
        internal MyLinkedListNode(T item = default,
            MyLinkedListNode<T> next = null,
            MyLinkedListNode<T> prev = null)
        {
            this.Item = item;
            this.Next = next;
            this.Prev = prev;
        }
    }
}
