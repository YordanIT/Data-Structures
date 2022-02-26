namespace Problem02.DoublyLinkedList
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class DoublyLinkedList<T> : IAbstractLinkedList<T>
    {
        private Node<T> head;
        private Node<T> tail;

        public int Count { get; private set; }

        public void AddFirst(T item)
        {
            var newNode = new Node<T>(item, null, null);

            if (this.Count == 0)
            {
                this.head = this.tail = newNode;
                this.Count++;
                return;
            }

            var currentHead = this.head;
            currentHead.Previous = newNode;
            
            this.head = newNode;
            this.head.Next = currentHead;
            this.Count++;
        }

        public void AddLast(T item)
        {
            var newNode = new Node<T>(item, null, null);

            if (this.Count == 0)
            {
                this.head = this.tail = newNode;
                this.Count++;
                return;
            }

            var currentTail = this.tail;
            currentTail.Next = newNode;

            this.tail = newNode;
            this.head.Previous = currentTail;
            this.Count++;
        }

        public T GetFirst()
        {
            this.EnsureNotEmpty();

            return this.head.Item;
        }

        public T GetLast()
        {
            this.EnsureNotEmpty();

            return this.tail.Item;
        }

        public T RemoveFirst()
        {
            this.EnsureNotEmpty();

            var headItem = this.head.Item;
            var newHead = this.head.Next;
            this.head.Next = null;
            this.head = newHead;
            this.Count--;

            return headItem;
        }

        public T RemoveLast()
        {
            this.EnsureNotEmpty();

            var tailItem = this.tail.Item;
            var newTail = this.tail.Previous;
            this.tail.Previous = null;
            this.tail = newTail;
            this.Count--;

            return tailItem;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var current = this.head;

            while (current != null)
            {
                yield return current.Item;
                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        private void EnsureNotEmpty()
        {
            if (this.Count == 0)
                throw new InvalidOperationException();
        }
    }
}