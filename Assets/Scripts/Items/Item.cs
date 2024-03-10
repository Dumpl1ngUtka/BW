using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    public abstract class Item
    {
        protected int MaxCount;
        protected int CurrentCount;

        public int Count => CurrentCount;
        public event Action<int> CurrentCountChanged;

        public virtual bool TryToUse()
        {
            if (CurrentCount == 0)
                return false;
            CurrentCountChanged?.Invoke(CurrentCount--);
            Use();
            return true;
        }
        protected abstract void Use();
    }
}