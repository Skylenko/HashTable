using System;
using NUnit.Framework;
using HashTable;

namespace Test
{
    [TestFixture]
    public class Tests
    {
        private OwnHashTable _hashTable;

        [SetUp]
        public void Init()
        {
            _hashTable = new OwnHashTable();
        }

        [Test]
        public void ShouldContainElement()
        {
            _hashTable.Add("Test", "one");
            Assert.True(_hashTable.Contains("Test"));
        }

        [Test]
        public void ShouldNotContainKeyWhenEmpty()
        {
            Assert.False(_hashTable.Contains("qwe"));
        }

        [Test]
        public void ShouldAddKeyValue()
        {
            _hashTable.Add("Something", 9);
            Assert.AreEqual(_hashTable.Length(), 1);
        }

        [Test]
        public void ShouldThrowExceptionAddMethod()
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
        public void ShouldGetValueByKey()
        {
            _hashTable.Add("Flower", "Red");
            Assert.True(_hashTable["Flower"].Equals("Red"));
        }

        [Test]
        public void ShouldThrowExceptionWhenKeyDoesNotExist()
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
        public void ShouldSetValueByKey()
        {
            _hashTable.Add("Mark", 98);
            _hashTable["Mark"] = 89;
            Assert.True(_hashTable["Mark"].Equals(89));
        }

        [Test]
        public void ShouldDeletePairWhenTrySetNullValueByKey()
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
        public void ShouldTryGetKeyValue()
        {
            _hashTable.Add(9, 8);
            object o;
            Assert.True(_hashTable.TryGet(9, out o));
        }

        [Test]
        public void ShouldNotContainPairEmpty()
        {
            _hashTable.Add(9, null);
            object o;
            Assert.False(_hashTable.TryGet(9, out o));
        }
    }
}