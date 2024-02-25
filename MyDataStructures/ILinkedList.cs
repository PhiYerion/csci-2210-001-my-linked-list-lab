using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDataStructures
{
    public interface ILinkedList<T> : IEnumerable<T>, ICollection<T>, IList<T> where T : IEquatable<T>
    {
        internal MyLinkedListNode<T>? GetHeadNode();
        internal MyLinkedListNode<T>? GetTailNode();
        public void AddToBeginning(T item);
        public void AddToEnd(T item);
    }
}
