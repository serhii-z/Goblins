using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace GoblinProject
{
    class PriorityQueue : ICollection, ICloneable
    {
        private Object[] array;
        private Object syncRoot;
        private int size;
        //private int capasity;
        private int minimum = 4;
        private int grow = 50;
        private int tail;
        private int version;
        private int head;
        public int Count { get { return size; } }

        public object SyncRoot
        {
            get
            {
                if (syncRoot == null)
                {
                    System.Threading.Interlocked.CompareExchange(ref syncRoot, new Object(), null);
                }
                return syncRoot;
            }
        }

        public bool IsSynchronized { get { return false; } }

        public PriorityQueue()
        {
            array = new Object[4];
        }

        public PriorityQueue(int capacity)
        {
            if (capacity < 0)
                throw new ArgumentOutOfRangeException("ArgumentOutOfRangeException");
            if (capacity == 0)
                array = new object[4];
            else
                array = new object[capacity];
        }



        public object Clone()
        {
            object[] temp = new object[size];
            int i = 0;
            foreach (object item in array)
            {
                temp[i] = item;
                i++;
            }
            return temp;
        }

        public void CopyTo(Array arr, int index)
        {
            var mass = arr as object[];
            if (mass == null)
                throw new ArgumentException("Expecting  array to be object");
            for (int i = 0; i < this.array.Length; i++)
            {
                mass[index++] = this.array[i];
            }
        }

        public IEnumerator GetEnumerator()
        {
            for (int i = 0; i < size; i++)
            {
                yield return array[i];
            }
        }

        public virtual void Enqueue(Object obj)
        {
            if (size == array.Length)
            {
                int newcapacity = (int)((long)array.Length * (long)grow / 100);
                if (newcapacity < array.Length + minimum)
                {
                    newcapacity = array.Length + minimum;
                }
                SetCapacity(newcapacity);
            }

            array[tail] = obj;
            tail = (tail + 1) % array.Length;
            size++;
            version++;
        }

        public void Insert(int index, object value)
        {
            if (size == array.Length)
            {
                int newcapacity = (int)((long)array.Length * (long)grow / 100);
                if (newcapacity < array.Length + minimum)
                {
                    newcapacity = array.Length + minimum;
                }
                SetCapacity(newcapacity);
            }

            size++;
            for (int i = size - 1; i > index; i--)
            {
                array[i] = array[i - 1];
            }
            array[index] = value;
            tail = (tail + 1) % array.Length;
            version++;
        }

        private void SetCapacity(int capacity)
        {
            Object[] newarray = new Object[capacity];
            if (size > 0)
            {
                if (head < tail)
                {
                    Array.Copy(array, head, newarray, 0, size);
                }
                else
                {
                    Array.Copy(array, head, newarray, 0, array.Length - head);
                    Array.Copy(array, 0, newarray, array.Length - head, tail);
                }
            }

            array = newarray;
            head = 0;
            tail = (size == capacity) ? 0 : size;
            version++;
        }

        public virtual Object Dequeue()
        {
            if (Count == 0)
                throw new InvalidOperationException("InvalidOperation_EmptyQueue");

            Object removed = array[head];            
            Object[] temp = new Object[size - 1];
            Array.Copy(array, 1, temp, 0, size-1);
            array = temp;
            size--;
            head = 0;
            //head = (head + 1) % array.Length;
            version++;
            return removed;
        }

        public virtual Object Peek()
        {
            if (Count == 0)
                throw new InvalidOperationException("InvalidOperation_EmptyQueue");

            return array[head];
        }

        public virtual bool Contains(Object obj)
        {
            int index = head;
            int count = size;

            while (count-- > 0)
            {
                if (obj == null)
                {
                    if (array[index] == null)
                        return true;
                }
                else if (array[index] != null && array[index].Equals(obj))
                {
                    return true;
                }
                index = (index + 1) % array.Length;
            }

            return false;
        }

        internal Object GetElement(int i)
        {
            return array[(head + i) % array.Length];
        }

        public Object Search(string number)
        {
            int ind = Convert.ToInt32(number);
            int i = 1;
            Object ob = "";
            foreach (var item in array)
            {
                if (i == ind)
                {
                    return item;
                }
                i++;
            }
            return ob;
        }

        public void RemoveAt(string number)
        {
            int index = Convert.ToInt32(number) - 1;
            if (index >= 0 && index <= size)
            {
                for (int i = index; i < size - 1; i++)
                {
                    array[i] = array[i + 1];
                }
                size--;
            }
        }

        public bool IsEmpty()
        {
            return size == 0;
        }
    }
}
