using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace ImageViewer.Common
{
    public class ObservableLinkedList<T> : INotifyCollectionChanged, IEnumerable
    {
        private readonly LinkedList<T> _linkedList;

        #region Variables accessors
        public int Count => _linkedList.Count;
        public LinkedListNode<T> First => _linkedList.First;
        public LinkedListNode<T> Last => _linkedList.Last;
        #endregion

        #region Constructors
        public ObservableLinkedList()
        {
            _linkedList = new LinkedList<T>();
        }

        public ObservableLinkedList(IEnumerable<T> collection)
        {
            _linkedList = new LinkedList<T>(collection);
        }
        #endregion

        #region LinkedList<T> Composition
        public LinkedListNode<T> AddAfter(LinkedListNode<T> prevNode, T value)
        {
            LinkedListNode<T> ret = _linkedList.AddAfter(prevNode, value);
            OnNotifyCollectionChanged();
            return ret;
        }

        public void AddAfter(LinkedListNode<T> node, LinkedListNode<T> newNode)
        {
            _linkedList.AddAfter(node, newNode);
            OnNotifyCollectionChanged();
        }

        public LinkedListNode<T> AddBefore(LinkedListNode<T> node, T value)
        {
            LinkedListNode<T> ret = _linkedList.AddBefore(node, value);
            OnNotifyCollectionChanged();
            return ret;
        }

        public void AddBefore(LinkedListNode<T> node, LinkedListNode<T> newNode)
        {
            _linkedList.AddBefore(node, newNode);
            OnNotifyCollectionChanged();
        }

        public LinkedListNode<T> AddFirst(T value)
        {
            LinkedListNode<T> ret = _linkedList.AddFirst(value);
            OnNotifyCollectionChanged();
            return ret;
        }

        public void AddFirst(LinkedListNode<T> node)
        {
            _linkedList.AddFirst(node);
            OnNotifyCollectionChanged();
        }

        public LinkedListNode<T> AddLast(T value)
        {
            LinkedListNode<T> ret = _linkedList.AddLast(value);
            OnNotifyCollectionChanged();
            return ret;
        }

        public void AddLast(LinkedListNode<T> node)
        {
            _linkedList.AddLast(node);
            OnNotifyCollectionChanged();
        }

        public void Clear()
        {
            _linkedList.Clear();
            OnNotifyCollectionChanged();
        }

        public bool Contains(T value)
        {
            return _linkedList.Contains(value);
        }

        public void CopyTo(T[] array, int index)
        {
            _linkedList.CopyTo(array, index);
        }

        public bool LinkedListEquals(object obj)
        {
            return _linkedList.Equals(obj);
        }

        public LinkedListNode<T> Find(T value)
        {
            return _linkedList.Find(value);
        }

        public LinkedListNode<T> FindLast(T value)
        {
            return _linkedList.FindLast(value);
        }

        public Type GetLinkedListType()
        {
            return _linkedList.GetType();
        }

        public bool Remove(T value)
        {
            bool ret = _linkedList.Remove(value);
            OnNotifyCollectionChanged();
            return ret;
        }

        public void Remove(LinkedListNode<T> node)
        {
            _linkedList.Remove(node);
            OnNotifyCollectionChanged();
        }

        public void RemoveFirst()
        {
            _linkedList.RemoveFirst();
            OnNotifyCollectionChanged();
        }

        public void RemoveLast()
        {
            _linkedList.RemoveLast();
            OnNotifyCollectionChanged();
        }
        #endregion

        #region INotifyCollectionChanged Members

        public event NotifyCollectionChangedEventHandler CollectionChanged;
        public void OnNotifyCollectionChanged()
        {
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (_linkedList as IEnumerable).GetEnumerator();
        }

        #endregion
    }
}
