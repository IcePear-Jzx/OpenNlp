﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using System.Threading.Tasks;

namespace OpenNLP.Tools.Util.Trees.TRegex.Tsurgeon
{
    public class JJTTsurgeonParserState
    {
        private List<Node> nodes;
        private List<int> marks;

        private int sp; // number of nodes on stack
        private int mk; // current mark
        private bool node_created;

        public JJTTsurgeonParserState()
        {
            nodes = new List<Node>();
            marks = new List<int>();
            sp = 0;
            mk = 0;
        }

        /* Determines whether the current node was actually closed and
     pushed.  This should only be called in the final user action of a
     node scope.  */

        public bool nodeCreated()
        {
            return node_created;
        }

        /* Call this to reinitialize the node stack.  It is called
     automatically by the parser's ReInit() method. */

        public void reset()
        {
            nodes.Clear();
            marks.Clear();
            sp = 0;
            mk = 0;
        }

        /* Returns the root node of the AST.  It only makes sense to call
     this after a successful parse. */

        public Node rootNode()
        {
            return nodes[0];
        }

        /* Pushes a node on to the stack. */

        public void pushNode(Node n)
        {
            nodes.Add(n);
            ++sp;
        }

        /* Returns the node on the top of the stack, and remove it from the
     stack.  */

        public Node popNode()
        {
            if (--sp < mk)
            {
                //mk = marks.remove(marks.size() - 1);
                mk = marks.Last();
                marks.Remove(mk);
            }
            var lNode = nodes.Last();
            nodes.Remove(lNode);
            return lNode;
            //return nodes.remove(nodes.size() - 1);
        }

        /* Returns the node currently on the top of the stack. */

        public Node peekNode()
        {
            return nodes[nodes.Count - 1];
        }

        /* Returns the number of children on the stack in the current node
     scope. */

        public int nodeArity()
        {
            return sp - mk;
        }


        public void clearNodeScope(Node n)
        {
            while (sp > mk)
            {
                popNode();
            }
            //mk = marks.remove(marks.size()-1);
            mk = marks[marks.Count - 1];
            marks.Remove(mk);
        }


        public void openNodeScope(Node n)
        {
            marks.Add(mk);
            mk = sp;
            n.jjtOpen();
        }


        /* A definite node is constructed from a specified number of
     children.  That number of nodes are popped from the stack and
     made the children of the definite node.  Then the definite node
     is pushed on to the stack. */

        public void closeNodeScope(Node n, int num)
        {
            //mk = marks.remove(marks.size()-1);
            mk = marks[marks.Count - 1];
            marks.Remove(mk);
            while (num-- > 0)
            {
                Node c = popNode();
                c.jjtSetParent(n);
                n.jjtAddChild(c, num);
            }
            n.jjtClose();
            pushNode(n);
            node_created = true;
        }


        /* A conditional node is constructed if its condition is true.  All
     the nodes that have been pushed since the node was opened are
     made children of the conditional node, which is then pushed
     on to the stack.  If the condition is false the node is not
     constructed and they are left on the stack. */

        public void closeNodeScope(Node n, bool condition)
        {
            if (condition)
            {
                int a = nodeArity();
                //mk = marks.remove(marks.size()-1);
                mk = marks.Last();
                marks.Remove(mk);
                while (a-- > 0)
                {
                    Node c = popNode();
                    c.jjtSetParent(n);
                    n.jjtAddChild(c, a);
                }
                n.jjtClose();
                pushNode(n);
                node_created = true;
            }
            else
            {
                //mk = marks.remove(marks.size()-1);
                mk = marks.Last();
                marks.Remove(mk);
                node_created = false;
            }
        }
    }
}