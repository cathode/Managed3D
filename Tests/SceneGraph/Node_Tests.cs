/******************************************************************************
 * Managed3D: A 3D Graphics API for .NET and Mono - http://gearedstudios.com/ *
 * Copyright © 2009-2012 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the MIT/X11    *
 * license. See the 'license.txt' file for details.                           *
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Managed3D.SceneGraph;
using NUnit.Framework;
using Managed3D.Geometry;

namespace Tests.SceneGraph
{
    [TestFixture]
    public class Node_Tests
    {
        /// <summary>
        /// Test to ensure that when adding a node as a child of another node,
        /// the child correctly references it's parent forming a doubly-linked relationship.
        /// </summary>
        [Test]
        public void ChildNodeParentRelationship()
        {
            Node parent = new Node();

            Node child = new Node();
            parent.Add(child);

            Assert.IsTrue(child.Parent == parent);
            Assert.IsTrue(parent.Contains(child));
        }

        [Test]
        public void DeepContainsBehavior()
        {
            var root = new Node();

            var c1 = new Node();
            root.Add(c1);

            Assert.IsTrue(root.Contains(c1));
            Assert.IsTrue(root.ContainsDeep(c1));

            var c2 = new Node();
            c1.Add(c2);

            Assert.IsFalse(root.Contains(c2));
            Assert.IsTrue(root.ContainsDeep(c2));
            Assert.IsTrue(c1.Contains(c2));

            root.Remove(c1);

            Assert.IsFalse(root.Contains(c1));
            Assert.IsFalse(root.ContainsDeep(c2));
            Assert.IsTrue(c1.Contains(c2));
            Assert.IsTrue(c1.ContainsDeep(c2));
        }

        [Test]
        public void InvalidOperationsShouldFail()
        {
            var root = new Node();

            try
            {
                root.Add(null);
            }
            catch (Exception ex)
            {
                Assert.Pass();
            }
            Assert.Fail();
        }

        /// <summary>
        /// Ensures that trying to create loops of parent-child relationships fails.
        /// </summary>
        [Test]
        public void RecursiveRelationshipShouldFail()
        {
            var root = new Node();

            var c1 = new Node();
            root.Add(c1);

            Assert.That(() => c1.Add(root), Throws.InvalidOperationException);

        }

        [Test]
        public void InOrderChildRemoval()
        {
            var root = new Node();

            var c1 = new Node();

            root.Add(c1);
            Assert.IsTrue(c1.Parent == root);

            var r2 = new Node();
            r2.Add(c1);

            Assert.IsTrue(c1.Parent == r2);
            Assert.IsTrue(root.Contains(c1));

            // Test in-order removal
            r2.Remove(c1);

            Assert.IsTrue(c1.Parent == root);
            Assert.IsFalse(r2.Contains(c1));
        }

        [Test]
        public void OutOfOrderChildRemoval()
        {
            var r1 = new Node();
            var r2 = new Node();
            var c1 = new Node();

            r1.Add(c1);
            r2.Add(c1);

            Assert.IsTrue(c1.Parent == r2);

            r1.Remove(c1);

            Assert.IsTrue(c1.Parent == r2);
            Assert.IsTrue(r2.Contains(c1));
            Assert.IsFalse(r1.Contains(c1));

        }

        [Test]
        public void EnsureCorrectExtentsForNode()
        {
            var root = new Node();
            var extents = root.GetExtents();
            Assert.AreEqual(Extents3.Empty, extents);
        }

        [Test]
        public void EnsureCorrectGraphExtentsForNodes()
        {

        }
    }
}
