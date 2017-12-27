using System;
using System.Dynamic;

namespace Epam.Mentoring.Collections
{
    public class HashTable : IHashTable
    {
        private const int DefaultBucketsNumber = 10;

        private const float DefaultLoadFactor = 0.75f;

        public int Size { get; private set; }

        private float loadFactor;

        private Bucket[] buckets;

        public HashTable(float loadFactor, int capacity)
        {
            this.loadFactor = loadFactor;
            buckets = new Bucket[capacity];
        }

        public HashTable() : this(DefaultLoadFactor, DefaultBucketsNumber)
        {
        }

        private class Bucket
        {
            public object Key;
            public object Value;
            public Bucket Next;
        }


        public bool Contains(object key)
        {
            int index = PositionInArray(key);

            var cursor = buckets[index];
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
            int index = IndexFor(hashCode, buckets.Length);
            return index;
        }

        public void Add(object key, object value)
        {
            ExpandIfRequired();

            PutEntry(key, value, buckets);
        }

        private void ExpandIfRequired()
        {
            if (((float) Size / (float) buckets.Length) >= loadFactor)
            {
                Bucket[] newBuckets = new Bucket[buckets.Length * 2];

                for (int i = 0; i < buckets.Length; i++)
                {
                    var cursor = buckets[i];
                    while (cursor != null)
                    {
                        PutEntry(cursor.Key, cursor.Value, newBuckets);
                        cursor = cursor.Next;
                    }
                }
                buckets = newBuckets;
            }
        }

        private void PutEntry(object key, object value, Bucket[] buckets)
        {
            Bucket bucket = new Bucket();

            int index = PositionInArray(key);

            if (key == null)
            {
                throw new Exception("Key must be not null");
            }

            if (Contains(key))
            {
                throw new Exception("Key already exists in the hash table");
            }

            bucket.Key = key;
            bucket.Value = value;

            var tableEntry = buckets[index];

            if (tableEntry == null)
            {
                buckets[index] = bucket;
            }
            else
            {
                bucket.Next = tableEntry;
                buckets[index] = bucket;
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

                var cursor = buckets[index];
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
                    buckets[index] = null;
                }

                else
                {
                    var cursor = buckets[index];
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
            var valueActual = buckets[PositionInArray(key)].Value;
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