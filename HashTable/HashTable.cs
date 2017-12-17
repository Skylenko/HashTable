using System;
using System.Dynamic;

namespace Epam.Mentoring.Collections
{
    public class HashTable : IHashTable
    {
        public int Size { get; private set; }

        private class HashTableEntry
        {
            public object Key;
            public object Value;
            public HashTableEntry Next;
        }

        private const int DefaultBucketsNumber = 100;
        private HashTableEntry[] _bucket = new HashTableEntry[DefaultBucketsNumber];

        public bool Contains(object key)
        {
            int index = PositionInArray(key);

            var cursor = _bucket[index];
            while (cursor != null)
            {
                bool keyExists = key.Equals(cursor.Key);
                if (keyExists) return true;
                cursor = cursor.Next;
            }
            return false;
        }

        //I found several ways to calculate hash function. One was used "%", in another - "&". I used second.
        //This operation allows to allocate a specific range of hash codes to the index in the limit of this array
        private static int IndexFor(int h, int length)
        {
            return h & (length - 1);
        }

        private int PositionInArray(object key)
        {
            int hashCode = key.GetHashCode();
            int index = IndexFor(hashCode, _bucket.Length);
            return index;
        }

        public void Add(object key, object value)
        {
            HashTableEntry hashTableEntry = new HashTableEntry();

            int index = PositionInArray(key);

            if (key == null)
            {
                throw new Exception("Key must be not null");
            }

            if (Contains(key))
            {
                throw new Exception("Key already exists in the hash table");
            }

            hashTableEntry.Key = key;
            hashTableEntry.Value = value;

            var tableEntry = _bucket[index];

            if (tableEntry == null)
            {
                _bucket[index] = hashTableEntry;
            }
            else
            {
                hashTableEntry.Next = tableEntry;
                _bucket[index] = hashTableEntry;
            }
            Size++;
        }

        public object this[object key]
        {
            get
            {
                if (!Contains(key))
                {
                    throw new Exception("Key does not exist in the hash table");
                }

                int index = PositionInArray(key);

                var cursor = _bucket[index];
                while (cursor != null)
                {
                    bool keyExists = key.Equals(cursor.Key);
                    if (keyExists) return cursor.Value;
                    cursor = cursor.Next;
                }
                return null;
            }

            set
            {
                int index = PositionInArray(key);

                if (value == null)
                {
                    _bucket[index] = null;
                }
                
                else
                {
                    var cursor = _bucket[index];
                    while (cursor != null)
                    {
                        bool keyExists = key.Equals(cursor.Key);
                        if (keyExists) cursor.Value = value;
                        cursor = cursor.Next;
                    }
                }
            }
        }

        public bool TryGet(object key, out object value)
        {
            var valueActual = _bucket[PositionInArray(key)].Value;
            if (valueActual != null)
            {
                value = valueActual;
                return true;
            }

            //Value is not associate with given key, so assign default value, i.e. null
            value = null;
            return false;
        }
    }
}