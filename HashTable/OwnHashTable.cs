using System;

namespace HashTable
{
    public class OwnHashTable : IHashTable
    {
        private int _size;

        class HashTableEntry
        {
            internal object Key;
            internal object Value;
        }

        HashTableEntry[] _tableEntries = new HashTableEntry[100];

        public bool Contains(object key)
        {
            int index = Index(key);

            if (_tableEntries[index] == null)
            {
                return false;
            }
            return true;
        }

        private static int IndexFor(int h, int length)
        {
            return h & (length - 1);
        }

        public int Length()
        {
            return _size;
        }

        private int Index(object key)
        {
            int hashCode = key.GetHashCode();
            int index = IndexFor(hashCode, _tableEntries.Length);
            return index;
        }

        public void Add(object key, object value)
        {
            HashTableEntry hashTableEntry = new HashTableEntry();

            int index = Index(key);

            if (Contains(key))
            {
                throw new Exception("Key already exists in the hash table");
            }

            hashTableEntry.Key = key;
            hashTableEntry.Value = value;

            _tableEntries[index] = hashTableEntry;

            _size++;
        }

        public object this[object key]
        {
            get
            {
                if (!Contains(key))
                {
                    throw new Exception("Key does not exist in the hash table");
                }
                int index = Index(key);

                return _tableEntries[index].Value;
            }

            set
            {
                int index = Index(key);

                if (value == null)
                {
                    _tableEntries[index] = null;
                }

                else
                {
                    _tableEntries[index].Value = value;
                }
            }
        }

        public bool TryGet(object key, out object value)
        {
            value = _tableEntries[Index(key)].Value;
            return value != null;
        }
    }
}