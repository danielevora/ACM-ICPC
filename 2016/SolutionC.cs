using System;
using System.Text;
using System.Collections.Generic;

public namespace ACMICPC2016
{
    public class BinaryTree {
        public BinaryTreeNode Root { get; private set; }

        public BinaryTree(int value) {
            Root = new BinaryTreeNode(value);
        }

        public void Insert(int value) {
            Insert(Root, value);
        }

        private void Insert(BinaryTreeNode btn, int value) {
            if (btn.Value > value) {
                if (btn.Left == null) {
                    btn.InsertLeft(value);
                } else {
                    Insert(btn.Left, value);
                }
            } else {
                if (btn.Right == null) {
                    btn.InsertRight(value);
                } else {
                    Insert(btn.Right, value);
                }
            }
        }

        public void ReValueTree() {
            var count = 0;
            var stack = new Stack<BinaryTreeNode>();
            stack.Push(Root);
            while (stack.Count > 0) {
                count += 1;
                var current = stack.Pop();
                current.UpdateValue(count);
                if (current.Left != null) {
                    stack.Push(current.Left);
                }
                if (current.Right != null) {
                    stack.Push(current.Right);
                }
            }
        }

        public string Serialize() {
            var sb = new StringBuilder();
            Serialize(Root, sb);
            return sb.ToString();
        }

        private void Serialize(BinaryTreeNode btn, StringBuilder sb) {
            if (btn == null) {
                sb.Append("$ ");
            } else {
                sb.Append(btn.Value + " ");
                Serialize(btn.Left, sb);
                Serialize(btn.Right, sb);
            }
        }
    }

    public class BinaryTreeNode {
        public int Value { get; private set; }
        public BinaryTreeNode Left { get; private set; }
        public BinaryTreeNode Right { get; private set; }

        public BinaryTreeNode(int value) {
            Value = value;
        }

        public void UpdateValue(int value) {
            Value = value;
        }

        public BinaryTreeNode InsertLeft(int leftValue) {
            Left = new BinaryTreeNode(leftValue);
            return Left;
        }

        public BinaryTreeNode InsertRight(int rightValue) {
            Right = new BinaryTreeNode(rightValue);
            return Right;
        }
    }

    public class SolutionC {
        public static void Main(String[] args) {
            var treeShapes = new HashSet<string>();

            //read input line 1: n, k
            var line1 = Console.ReadLine().Split(null);
            var n = int.Parse(line1[0]);
            var k = int.Parse(line1[1]);

            //loop ceiling prototypes, build tree from collapse resistance values
            for (var i = 0; i < n; i++) {
                var prototype = Console.ReadLine().Split(null);
                var tree = new BinaryTree(int.Parse(prototype[0]));
                for (var j = 1; j < k; j++) {
                    tree.Insert(int.Parse(prototype[j]));
                }
                
                //reorder/value
                tree.ReValueTree();

                //serialize tree
                var serializedTree = tree.Serialize();

                //add tree shape if necessary
                if (!treeShapes.Contains(serializedTree)) {
                    treeShapes.Add(serializedTree);
                }
            }

            //output count
            Console.WriteLine(treeShapes.Count);
        }
    }
}
