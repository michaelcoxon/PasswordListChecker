using System;
using System.Collections;
using System.Collections.Generic;

namespace PasswordListChecker
{
    public sealed class PasswordList : ICollection<string>
    {
        private readonly HashSet<string> _internalPasswordList;

        public PasswordList()
        {
            this._internalPasswordList = new HashSet<string>();
        }

        public PasswordList(IEnumerable<string> passwords)
            : this()
        {
            this.AddRange(passwords);
        }

        public void AddRange(IEnumerable<string> passwords)
        {
            foreach (var password in passwords)
            {
                this.Add(password);
            }
        }

        public int Count => this._internalPasswordList.Count;

        public bool IsReadOnly => false;

        public void Add(string item)
        {
            if (!this._internalPasswordList.Contains(item))
            {
                this._internalPasswordList.Add(item);
            }
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
            return this._internalPasswordList.Remove(item);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this._internalPasswordList.GetEnumerator();
        }
    }
}
