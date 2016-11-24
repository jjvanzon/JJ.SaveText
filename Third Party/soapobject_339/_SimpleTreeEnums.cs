// This collection of non-binary tree data structures created by Dan Vanderboom.
// Critical Development blog: http://dvanderboom.wordpress.com
// Original Tree<T> blog article: http://dvanderboom.wordpress.com/2008/03/15/treet-implementing-a-non-binary-tree-in-c/
// Linked-in: http://www.linkedin.com/profile?viewProfile=&key=13009616&trk=tab_pro

using System;
using System.Text;

#region unity specifc code
using UnityEngine;
public class _SimpleTreeEnums : MonoBehaviour {}
#endregion

namespace System.Collections.Generic
{
    public enum TreeTraversalType
    {
		//veg: Added Siblings.
		Siblings,
        DepthFirst,
        BreadthFirst
    }

    public enum TreeTraversalDirection
    {
        TopDown,
        BottomUp
    }
}