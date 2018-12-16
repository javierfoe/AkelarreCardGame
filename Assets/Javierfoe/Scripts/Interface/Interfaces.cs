﻿using UnityEngine;

namespace Bang
{
    public interface IBoardView
    {
        void EnableGeneralStore(bool value);
        void EnableGeneralStoreCards(bool value);
        void AddGeneralStoreCard(int index, string name, Suit suit, Rank rank, Color color);
        void RemoveGeneralStoreCard(int index);
        void SetDeckSize(int cards);
        void SetDiscardTop(string name, Suit suit, Rank rank, Color color);
        void EmptyDiscardStack();
    }

    public interface IDropView
    {
        GameObject GameObject();
        bool GetDroppable();
        void SetDroppable(bool value);
        Drop GetDropEnum();
        int GetDropIndex();
    }

    public interface ICardView : IDropView
    {
        void Playable(bool value);
        void SetIndex(int index);
        void SetRank(Rank rank);
        void SetSuit(Suit suit);
        void SetName(string name, Color color);
        void Empty();
    }

    public interface IGeneralStoreCardView : ICardView
    {
        void Enable(bool value);
    }

    public interface IPlayerView : IDropView
    {
        void SetStealable(bool value, bool weapon);
        void SetPlayerIndex(int index);
        void SetClientButtons();
        int GetPlayerIndex();
        void UpdateHP(int hp);
        void SetSheriff();
        void SetRole(Role role);
        void EnableEndTurnButton(bool enable);
        void EnableTakeHitButton(bool enable);
        void EnableDieButton(bool enable);
        void EnableCard(int index, bool enable);
        void AddCard();
        void AddCard(int index, string name, Suit suit, Rank rank, Color color);
        void EquipProperty(int index, string name, Suit suit, Rank rank, Color color);
        void RemoveProperty(int index);
        void RemoveCard();
        void RemoveCard(int index);
        void EquipWeapon(string name, Suit suit, Rank rank, Color color);
    }
}
