﻿using System.Collections.Generic;
using UnityEngine;

namespace PimPamPum
{
    public abstract class CardListView<T> : MonoBehaviour where T : ICardView
    {

        protected List<T> list;

        protected virtual void Awake()
        {
            list = new List<T>();
        }

        protected abstract GameObject GetPrefab();

        public void AddCard(int index, CardStruct cs)
        {
            GameObject prefab = GetPrefab();
            T cv = Instantiate(prefab, transform).GetComponent<T>();
            cv.SetIndex(index);
            cv.SetCard(cs);
            list.Add(cv);
        }

        public void RemoveCard(int index)
        {
            Destroy(list[index].GameObject());
            list.RemoveAt(index);
            for (int i = index; i < list.Count; i++) list[i].SetIndex(i);
        }

        public void RemoveAllCards()
        {
            for(int i = 0; i < list.Count; i++)
            {
                Destroy(list[i].GameObject());
            }
            list.Clear();
        }

        public void SetDroppable(bool value)
        {
            foreach(T cv in list) cv.SetDroppable(value);
        }

        public void SetPlayable(int index, bool value)
        {
            list[index].Playable(value);
        }
    }
}