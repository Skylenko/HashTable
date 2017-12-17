using System;
using NUnit.Framework;
using Epam.Mentoring.Collections;

namespace Epam.Mentoring.Collections.Tests
{
    [TestFixture]
    public class Tests
    {
        private HashTable _hashTable;

        [SetUp]
        public void Init()
        {
            _hashTable = new HashTable();
        }

        [Test]
        public void Contains_AddKeyAndValue_IsTrue()
        {
            _hashTable.Add("Test", "one");
            Assert.True(_hashTable.Contains("Test"));
        }

        [Test]
        public void Contains_EmptyHashtable_False()
        {
            Assert.False(_hashTable.Contains("qwe"));
        }

        [Test]
        public void Contains_WhenItIsCollision_IsTrue()
        {
            object key = new ObjectWithConstantHashcode();
            _hashTable.Add(new ObjectWithConstantHashcode(), "One");
            _hashTable.Add(key, "Done");
            _hashTable.Add(new ObjectWithConstantHashcode(), 90);
            Assert.True(_hashTable.Contains(key));
        }

        [Test]
        public void Add_AddKeyAndValue_LengthOfHashtableChanged()
        {
            _hashTable.Add("Something", 9);
            Assert.AreEqual(_hashTable.Size, 1);
        }

        [Test]
        public void Add_AddKeyAndValueIsCollision_LengthOfHashtableChanged()
        {
            _hashTable.Add(new ObjectWithConstantHashcode(), "Red");
            _hashTable.Add(new ObjectWithConstantHashcode(), "one");
            _hashTable.Add(new ObjectWithConstantHashcode(), 76);
            Assert.AreEqual(_hashTable.Size, 3);
        }

        [Test]
        public void Add_AddKeyNull_ThrowException()
        {
            Exception exception;

            try
            {
                _hashTable.Add(null, 0);
                Assert.Fail();
            }
            catch (Exception ex)
            {
            }
        }

        [Test]
        public void Add_AddKeyWhichAlreadyExists_ThrowException()
        {
            Exception exception = null;
            _hashTable.Add("test", 8);

            try
            {
                _hashTable.Add("test", 0);
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            if (exception != null) Assert.AreEqual(exception.Message, "Key already exists in the hash table");
        }

        [Test]
        public void Indexator_GetValueByKeyWhenIsСollision_ValueEqualsValueByKey()
        {
            object key = new ObjectWithConstantHashcode();
            _hashTable.Add(key, "Red");
            _hashTable.Add(new ObjectWithConstantHashcode(), "one");

            Assert.True(_hashTable[key].Equals("Red"));
        }

        [Test]
        public void Indexator_GetValueByKeyWithoutСollision_ValueEqualsValueByKey()
        {
            _hashTable.Add("Flower", "Blue");

            Assert.True(_hashTable["Flower"].Equals("Blue"));
        }

        [Test]
        public void Indexator_GetValueByKeyWhichDoesNotExist_ThrowException()
        {
            Exception exception = null;
            _hashTable.Add(4.44, 'T');

            try
            {
                var o = _hashTable["i"];
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            if (exception != null) Assert.AreEqual(exception.Message, "Key does not exist in the hash table");
        }

        [Test]
        public void Indexator_SetValueByKeyWhenIsCollision_ValueEqualsValueWhichWasSet()
        {
            object key = new ObjectWithConstantHashcode();
            _hashTable.Add(new ObjectWithConstantHashcode(), "Red");
            _hashTable.Add(key, "one");
            _hashTable.Add(new ObjectWithConstantHashcode(), 76);
            _hashTable[key] = "o";
            Assert.True(_hashTable[key].Equals("o"));
        }

        [Test]
        public void Indexator_SetValueByKey_ValueEqualsValueWhichWasSet()
        {
            _hashTable.Add("Mark", 89);
            _hashTable["Mark"] = 8;
            Assert.True(_hashTable["Mark"].Equals(8));
        }

        [Test]
        public void Indexator_SetNullValueByKey_DeletedPairFromHashTable()
        {
            _hashTable.Add("Sun", 98);
            _hashTable["Sun"] = null;
            Exception exception = null;

            try
            {
                var o = _hashTable["Sun"];
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            if (exception != null) Assert.AreEqual(exception.Message, "Key does not exist in the hash table");
        }

        [Test]
        public void TryGet_ValueNotNull_isTrue()
        {
            _hashTable.Add(9, 8);
            object o;
            Assert.True(_hashTable.TryGet(9, out o));
        }

        [Test]
        public void TryGet_ValueIsNull_False()
        {
            _hashTable.Add(9, null);
            object o;
            Assert.False(_hashTable.TryGet(9, out o));
        }

        private class ObjectWithConstantHashcode
        {
            public override bool Equals(object obj)
            {
                return base.Equals(obj);
            }

            public override int GetHashCode()
            {
                return 10;
            }
        }
    }
}