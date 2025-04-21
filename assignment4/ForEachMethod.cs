using System;
namespace homework
{
    public class Node<T>
    {
        public T Value { get; set; }
        public Node<T> Next { get; set; }

        public Node(T value)
        {
            Value = value;
            Next = null;
        }
    }
    public class GenericList<T>
    {
        public delegate void Action<T>(T obj);
        private Node<T> head;
        private Node<T> tail;

        public GenericList()
        {
            head = tail = null;
        }
        public Node<T> Head
        {
            get => head;
        }
        public void Add(T val)
        {
            Node<T> n = new Node<T>(val);
            if(tail == null)
            {
                head = tail = n;
            }
            else
            {
                tail.Next = n;
                tail = n;
            }
        }
        public void ForEach(Action<T> action)
        {
            if (head == null)
            {
                throw new ArgumentNullException("链表不能为空！");
            }
            Node<T> node = head;
            while (node != null)
            {
                action(node.Value);
                node = node.Next;
            }
        }
    }
}