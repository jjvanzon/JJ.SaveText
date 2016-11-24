// This collection of non-binary tree data structures created by Dan Vanderboom.
// Critical Development blog: http://dvanderboom.wordpress.com
// Original Tree<T> blog article: http://dvanderboom.wordpress.com/2008/03/15/treet-implementing-a-non-binary-tree-in-c/
// Linked-in: http://www.linkedin.com/profile?viewProfile=&key=13009616&trk=tab_pro

using System;
using System.Text;

#region unity specifc code
using UnityEngine;
public class _SimpleTreeNodeList : MonoBehaviour {}
#endregion
	
namespace System.Collections.Generic
{
    /// <summary>
    /// Contains a list of SimpleTreeNode (or SimpleTreeNode-derived) objects, with the capability of linking parents and children in both directions.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SimpleTreeNodeList<T> : List<SimpleTreeNode<T>>
    {
        public SimpleTreeNode<T> Parent;

        public SimpleTreeNodeList(SimpleTreeNode<T> Parent)
        {
            this.Parent = Parent;
        }

        public new SimpleTreeNode<T> Add(SimpleTreeNode<T> Node)
        {
            base.Add(Node);
            Node.Parent = Parent;
            return Node;
        }

        public SimpleTreeNode<T> Add(T Value)
        {
            SimpleTreeNode<T> Node = new SimpleTreeNode<T>(Parent);
            Node.Value = Value;
            return Node;
        }

        public new void Remove(SimpleTreeNode<T> Node)
        {
            base.Remove(Node);			//fix
            if (Node != null)
            {
                Node.Parent = null;
            }
            //base.Remove(Node);			//fix
        }

        public override string ToString()
        {
            return "Count=" + Count.ToString();
        }
    }
}