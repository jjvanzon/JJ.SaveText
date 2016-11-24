// This collection of non-binary tree data structures created by Dan Vanderboom.
// Critical Development blog: http://dvanderboom.wordpress.com
// Original Tree<T> blog article: http://dvanderboom.wordpress.com/2008/03/15/treet-implementing-a-non-binary-tree-in-c/
// Linked-in: http://www.linkedin.com/profile?viewProfile=&key=13009616&trk=tab_pro

using System;
using System.Text;

#region unity specifc code
using UnityEngine;
public class _SimpleTreeNode : MonoBehaviour {}
#endregion
	
namespace System.Collections.Generic
{
    /// <summary>
    /// Represents a node in a SimpleTree structure, with a parent node and zero or more child nodes.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SimpleTreeNode<T> : IDisposable
    {
        private SimpleTreeNode<T> _Parent;
        public SimpleTreeNode<T> Parent
        {
            get { return _Parent; }
            set
            {
                if (value == _Parent)
                {
                    return;
                }

                if (_Parent != null)
                {
                    if (_Parent.Children.Contains(this)) {		//fix
						_Parent.Children.Remove(this);
					}														//fix
                }

                if (value != null && !value.Children.Contains(this))
                {
                    value.Children.Add(this);
                }

                _Parent = value;
            }
        }

        public SimpleTreeNode<T> Root
        {
            get
            {
                //return (Parent == null) ? this : Parent.Root;

                SimpleTreeNode<T> node = this;
                while (node.Parent != null)
                {
                    node = node.Parent;
                }
                return node;
            }
        }

        private SimpleTreeNodeList<T> _Children;
        public SimpleTreeNodeList<T> Children
        {
            get { return _Children; }
            private set { _Children = value; }
        }

        private T _Value;
        public T Value
        {
            get { return _Value; }
            set { _Value = value; }
        }

        private TreeTraversalDirection _DisposeTraversalDirection = TreeTraversalDirection.BottomUp;
        public TreeTraversalDirection DisposeTraversalDirection
        {
            get { return _DisposeTraversalDirection; }
            set { _DisposeTraversalDirection = value; }
        }

        private TreeTraversalType _DisposeTraversalType = TreeTraversalType.DepthFirst;
        public TreeTraversalType DisposeTraversalType
        {
            get { return _DisposeTraversalType; }
            set { _DisposeTraversalType = value; }
        }

        public SimpleTreeNode()
        {
            Parent = null;
            Children = new SimpleTreeNodeList<T>(this);
        }

        public SimpleTreeNode(T Value)
        {
            this.Value = Value;
            Children = new SimpleTreeNodeList<T>(this);
        }

        public SimpleTreeNode(SimpleTreeNode<T> Parent)
        {
            this.Parent = Parent;
            Children = new SimpleTreeNodeList<T>(this);
        }

        public SimpleTreeNode(SimpleTreeNodeList<T> Children)
        {
            Parent = null;
            this.Children = Children;
            Children.Parent = this;
        }

        public SimpleTreeNode(SimpleTreeNode<T> Parent, SimpleTreeNodeList<T> Children)
        {
            this.Parent = Parent;
            this.Children = Children;
            Children.Parent = this;
        }

        /// <summary>
        /// Reports a depth of nesting in the tree, starting at 0 for the root.
        /// </summary>
        public int Depth
        {
            get
            {
                //return (Parent == null ? -1 : Parent.Depth) + 1;

                int depth = 0;
                SimpleTreeNode<T> node = this;
                while (node.Parent != null)
                {
                    node = node.Parent;
                    depth++;
                }
                return depth;
            }
        }

        public IEnumerator<SimpleTreeNode<T>> GetEnumerator(TreeTraversalType TraversalType, TreeTraversalDirection TraversalDirection)
        {
            switch (TraversalType)
            {
				//veg: Added Siblings.
				case TreeTraversalType.Siblings: return GetSiblingsEnumerator(TraversalDirection);
                case TreeTraversalType.DepthFirst: return GetDepthFirstEnumerator(TraversalDirection);
                case TreeTraversalType.BreadthFirst: return GetBreadthFirstEnumerator(TraversalDirection);
                default: return null;
            }
        }

        // TODO: adjust for traversal direction
        private IEnumerator<SimpleTreeNode<T>> GetSiblingsEnumerator(TreeTraversalDirection TraversalDirection)
        {
			//veg: Added Siblings.
            foreach (SimpleTreeNode<T> child in Children)
            {
				yield return child;
            }
        }

        // TODO: adjust for traversal direction
        private IEnumerator<SimpleTreeNode<T>> GetDepthFirstEnumerator(TreeTraversalDirection TraversalDirection)
        {
            yield return this;
            foreach (SimpleTreeNode<T> child in Children)
            {
                IEnumerator<SimpleTreeNode<T>> enumerator = child.GetDepthFirstEnumerator(TraversalDirection);
                while (enumerator.MoveNext())
                {
                    yield return enumerator.Current;
                }
            }
        }

        // TODO: adjust for traversal direction
        private IEnumerator<SimpleTreeNode<T>> GetBreadthFirstEnumerator(TreeTraversalDirection TraversalDirection)
        {
            Queue<SimpleTreeNode<T>> queue = new Queue<SimpleTreeNode<T>>();
            queue.Enqueue(this);
            while (0 < queue.Count)
            {
                SimpleTreeNode<T> node = queue.Dequeue();
                foreach (SimpleTreeNode<T> child in node.Children)
                {
                    queue.Enqueue(child);
                }
                yield return node;
            }
        }

        public override string ToString()
        {
            string Description = "[" + (Value == null ? "<null>" : Value.ToString()) + "] ";

            Description += "Depth=" + Depth.ToString() + ", Children=" + Children.Count.ToString();
            if (Root == this)
            {
                Description += " (Root)";
            }
            return Description;
        }

		//veg: added
		public void Clear()
        {
            //Clean up contained objects (in Value property)
            Children.Clear();
			//veg: Fails Value = new T();
			//veg: Fails Value = unassigned(T)null;
        }
		
        #region IDisposable

        private bool _IsDisposed;
        public bool IsDisposed
        {
            get { return _IsDisposed; }
        }

        // TODO: update this to use GetEnumerator once that's working
        public virtual void Dispose()
        {
            CheckDisposed();

            // clean up contained objects (in Value property)
            if (DisposeTraversalDirection == TreeTraversalDirection.BottomUp)
            {
                foreach (SimpleTreeNode<T> node in Children)
                {
                    node.Dispose();
                }
            }

            OnDisposing();

            if (DisposeTraversalDirection == TreeTraversalDirection.TopDown)
            {
                foreach (SimpleTreeNode<T> node in Children)
                {
                    node.Dispose();
                }
            }

            _IsDisposed = true;
        }

        public event EventHandler Disposing;

        protected void OnDisposing()
        {
            if (Disposing != null)
            {
                Disposing(this, EventArgs.Empty);
            }
        }

        protected void CheckDisposed()
        {
            if (IsDisposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }
        }

        #endregion
    }
}