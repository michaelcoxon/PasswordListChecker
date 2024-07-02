using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace PasswordListChecker
{
    public sealed class PasswordList : ICollection<string>
    {
        private readonly List<string> _internalPasswordList;

        public PasswordList()
        {
            this._internalPasswordList = new();
        }

        public PasswordList(IEnumerable<string> passwords)
            : this()
        {
            this.AddRange((passwords));
            //this.AddRange(Distinct(passwords));
        }

        private static HashSet<string> Distinct(IEnumerable<string> passwords)
        {
            return new HashSet<string>(passwords);
        }

        public void AddRange(IEnumerable<string> passwords)
        {
            this._internalPasswordList.AddRange(passwords);
        }

        public int Count => this._internalPasswordList.Count;

        public bool IsReadOnly => false;

        public void Add(string item)
        {
            this._internalPasswordList.Add(item);
        }

        public void Clear()
        {
            this._internalPasswordList.Clear();
        }

        public bool Contains(string item)
        {
            return this._internalPasswordList.Contains(item);
        }

        public void CopyTo(string[] array, int arrayIndex)
        {
            this._internalPasswordList.CopyTo(array, arrayIndex);
        }

        public IEnumerator<string> GetEnumerator()
        {
            return this._internalPasswordList.GetEnumerator();
        }

        public bool Remove(string item)
        {
            this._internalPasswordList.Remove(item);
            return true;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this._internalPasswordList.GetEnumerator();
        }
    }
}
