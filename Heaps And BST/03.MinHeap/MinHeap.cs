namespace _03.MinHeap
{
    using System;
    using System.Collections.Generic;

    public class MinHeap<T> : IAbstractHeap<T>
        where T : IComparable<T>
    {
        private List<T> elements;

        public MinHeap()
        {
            elements = new List<T>();
        }

        public int Size => elements.Count;

        public T Dequeue()
        {
            if (elements.Count == 0)
            {
                throw new InvalidOperationException();
            }

            var minElement = elements[0];
            elements[0] = elements[elements.Count - 1];
            elements.RemoveAt(elements.Count - 1);
            HeapifyDown(0);

            return minElement;
        }
                
        public void Add(T element)
        {
            elements.Add(element);
            Heapify(elements.Count - 1);
        }

        public T Peek()
        {
            if (elements.Count == 0)
            {
                throw new InvalidOperationException();
            }
           
            return elements[0];
        }

        private void Heapify(int index)
        {
            if (index == 0)
            {
                return;
            }

            int parentIndex = (index - 1) / 2;
            
            if (elements[index].CompareTo(elements[parentIndex]) < 0)
            {
                var child = elements[parentIndex];
                var parent = elements[index];

                elements[parentIndex] = parent;
                elements[index] = child;

                Heapify(parentIndex);
            }
        }

        private void HeapifyDown(int index)
        {
            var leftChildIndex = 2 * index + 1;
            var rightChildIndex = 2 * index + 2;
            var minChildIndex = leftChildIndex;

            if (rightChildIndex >= elements.Count)
            {
                return;
            }

            if (elements[leftChildIndex].CompareTo(elements[rightChildIndex]) > 0 &&
                rightChildIndex < elements.Count)
            {
                minChildIndex = rightChildIndex;
            }
            
            if (elements[minChildIndex].CompareTo(elements[index]) < 0)
            {
                var element = elements[index];
                elements[index] = elements[minChildIndex];
                elements[minChildIndex] = element;
                HeapifyDown(minChildIndex);
            }
        }
    }
}
