using System;

namespace HashTable
{
    public class OwnHashTable : IHashTable
    {
        public int Size;

        private class HashTableEntry
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
            int index = Index(key);
            HashTableEntry res = _tableEntries[index];
            value = res.Value;
            return value != null;
        }
    }
}