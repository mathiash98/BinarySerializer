﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BinarySerialization.Test.Subtype
{
    [TestClass]
    public class SubtypeTests : TestBase
    {
        [TestMethod]
        public void SubtypeTest()
        {
            var expected = new SubtypeClass {Field = new SubclassB{SomethingForClassB = 33}};
            var actual = Roundtrip(expected);

            Assert.AreEqual(SubclassType.B, actual.Subtype);
            Assert.IsInstanceOfType(actual.Field, typeof(SubclassB));
        }

        [TestMethod]
        public void SubSubtypeTest()
        {
            var expected = new SubtypeClass
            {
                Field = new SubSubclassC(3)
                {
                    SomeSuperStuff = 1,
                    SomethingForClassB = 2
                }
            };
            var actual = Roundtrip(expected);

            Assert.AreEqual(SubclassType.C, actual.Subtype);
            Assert.IsInstanceOfType(actual.Field, typeof(SubSubclassC));
            Assert.AreEqual(actual.Field.SomeSuperStuff, expected.Field.SomeSuperStuff);
            Assert.AreEqual(((SubSubclassC)actual.Field).SomethingForClassB, ((SubSubclassC)expected.Field).SomethingForClassB);
            Assert.AreEqual(((SubSubclassC)actual.Field).SomethingForClassC, ((SubSubclassC)expected.Field).SomethingForClassC);
        }

        //[TestMethod]
        //[ExpectedException(typeof(BindingException))]
        //public void MissingSubtypeTest()
        //{
        //    var expected = new IncompleteSubtypeClass { Field = new SubclassB() };
        //    Roundtrip(expected);
        //}

        //[TestMethod]
        //public void BestFitSubtypeTest()
        //{
        //    var expected = new SubtypeClass { Field = new UnspecifiedSubclass() };
        //    var actual = Roundtrip(expected);

        //    Assert.AreEqual(SubclassType.B, actual.Subtype);
        //    Assert.IsInstanceOfType(actual.Field, typeof(SubclassB));
        //}

        [TestMethod]
        public void AncestorSubtypeBindingTest()
        {
            var expected = new AncestorSubtypeBindingContainerClass
            {
                AncestorSubtypeBindingClass =
                    new AncestorSubtypeBindingClass
                    {
                        InnerClass = new AncestorSubtypeBindingInnerClass { Value = "hello" }
                    }
            };

            var actual = Roundtrip(expected);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void IncompatibleBindingsTest()
        {
            var expected = new IncompatibleBindingsClass();
            Roundtrip(expected);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void InvalidSubtypeTest()
        {
            var expected = new InvalidSubtypeClass();
            Roundtrip(expected);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void NonUniqueSubtypesTest()
        {
            var expected = new NonUniqueSubtypesClass();
            Roundtrip(expected);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void NonUniqueSubtypeValuesTest()
        {
            var expected = new NonUniqueSubtypeValuesClass();
            Roundtrip(expected);
        }
    }
}
