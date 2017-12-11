using System;

//TODO: Namespace shoulbe have some sense. Probably in this case Epam.Mentoring.Collections
namespace HashTable
{
    //TODO: OwnHashTable does not sound like prod name :)
    public class OwnHashTable : IHashTable
    {
        private int _size;

        //TODO: add private to be more obvious
        class HashTableEntry
        {
            //TODO: No need in internal - your class is private :)
            internal object Key;
            internal object Value;
        }
        //TODO: add access modifier to be more cpecific
        //TODO: if you use constant - use a variable for it, like : private int const DefaultBucketsNumber
        //TODO: naming: let's follow naming better - it's rather buckets then just entries
        HashTableEntry[] _tableEntries = new HashTableEntry[100];

        public bool Contains(object key)
        {
            int index = Index(key);
            
            
            //TODO: think how we can have only one line instead of three
            if (_tableEntries[index] == null)
            {
                return false;
            }
            return true;
        }

        private static int IndexFor(int h, int length)
        {
            //TODO: please explain a logic
            return h & (length - 1);
        }
        
        //TODO: in such cases it's better to have property
        public int Length()
        {
            return _size;
        }

        //TODO: not clear naming - Index ? :) what does it mean ? I do need to understand it from a name
        private int Index(object key)
        {
            int hashCode = key.GetHashCode();
            int index = IndexFor(hashCode, _tableEntries.Length);
            return index;
        }


        //TODO: What does happen in case of collision? 
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
            //TODO: what happens in case we have no such value ? what should happen?
            value = _tableEntries[Index(key)].Value;
            return value != null;
        }
    }
}
