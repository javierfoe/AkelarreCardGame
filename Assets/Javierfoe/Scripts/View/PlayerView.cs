﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Bang
{

    public class PlayerView : DropView, IPlayerView
    {

        [SerializeField] private Text hp = null, info = null;
        [SerializeField] private HandView handHidden = null;
        [SerializeField] private GameObject weapon = null;
        [SerializeField] private Transform hand = null, properties = null;

        private int playerIndex;
        private int hiddenCards;
        private ICardView weaponCard;
        private List<ICardView> handCards;
        private List<ICardView> propertyCards;
        private TakeHitButton takeHitButton;
        private EndTurnButton endTurnButton;
        private DieButton dieButton;

        private int HiddenCards
        {
            get
            {
                return hiddenCards;
            }
            set
            {
                hiddenCards = value;
                handHidden.Text = hiddenCards.ToString();
            }
        }

        protected override void Start()
        {
            base.Start();
            handCards = new List<ICardView>();
            propertyCards = new List<ICardView>();
            weaponCard = weapon.GetComponent<ICardView>();
        }

        public void SetClientButtons()
        {
            endTurnButton = FindObjectOfType<EndTurnButton>();
            takeHitButton = FindObjectOfType<TakeHitButton>();
            dieButton = FindObjectOfType<DieButton>();
            endTurnButton.Active = false;
            takeHitButton.Active = false;
            dieButton.Active = false;
        }

        public void SetPlayerIndex(int index)
        {
            playerIndex = index;
        }

        public int GetPlayerIndex()
        {
            return playerIndex;
        }

        public void SetSheriff()
        {
            SetRole(Roles.SheriffName, Roles.SheriffColor);
        }

        public void SetRole(Role role)
        {
            Color color = Roles.GetColorFromRole(role);
            string name = Roles.GetNameFromRole(role);
            SetRole(name, color);
        }

        public void UpdateHP(int hp)
        {
            this.hp.text = hp.ToString();
        }

        private void SetRole(string name, Color color)
        {
            info.text = name;
            info.color = color;
        }

        public void AddCard()
        {
            HiddenCards += 1;
        }

        public void RemoveCard()
        {
            HiddenCards -= 1;
        }

        public void AddCard(int index, string name, Suit suit, Rank rank, Color color)
        {
            ICardView cv = InstantiateCard(index, name, suit, rank, color, hand);
            handCards.Add(cv);
        }

        public void EquipProperty(int index, string name, Suit suit, Rank rank, Color color)
        {
            ICardView cv = InstantiateProperty(index, name, suit, rank, color, properties);
            propertyCards.Add(cv);
        }

        public void RemoveCard(int index)
        {
            RemoveCard(index, handCards);
        }

        public void RemoveProperty(int index)
        {
            RemoveCard(index, propertyCards);
        }

        private void RemoveCard(int index, List<ICardView> list)
        {
            Destroy(list[index].GameObject());
            list.RemoveAt(index);
            for (int i = index; i < list.Count; i++)
            {
                list[i].SetIndex(i);
            }
        }

        private ICardView InstantiateCard(int index, string name, Suit suit, Rank rank, Color color, Transform t)
        {
            return InstantiateCardView(GameController.CardPrefab, index, name, suit, rank, color, t);
        }

        private ICardView InstantiateProperty(int index, string name, Suit suit, Rank rank, Color color, Transform t)
        {
            return InstantiateCardView(GameController.PropertyPrefab, index, name, suit, rank, color, t);
        }

        private ICardView InstantiateCardView(GameObject prefab, int index, string name, Suit suit, Rank rank, Color color, Transform t)
        {
            ICardView cv = Instantiate(prefab, t).GetComponent<ICardView>();
            cv.SetIndex(index);
            cv.SetName(name, color);
            cv.SetSuit(suit);
            cv.SetRank(rank);
            return cv;
        }

        public void EquipWeapon(string name, Suit suit, Rank rank, Color color)
        {
            weaponCard.SetName(name, color);
            weaponCard.SetSuit(suit);
            weaponCard.SetRank(rank);
        }

        public void EnableCard(int index, bool enable)
        {
            handCards[index].Playable(enable);
        }

        public void SetStealable(bool value, bool weapon)
        {
            if (handHidden.gameObject.activeSelf)
            {
                handHidden.SetDroppable(value);
            }
            else
            {
                foreach (ICardView cv in handCards)
                    cv.SetDroppable(value);
            }
            if (weapon) weaponCard.SetDroppable(value);
            foreach (ICardView cv in propertyCards)
                cv.SetDroppable(value);
        }

        public void EnableEndTurnButton(bool value)
        {
            endTurnButton.Active = value;
        }

        public void EnableTakeHitButton(bool value)
        {
            takeHitButton.Active = value;
        }

        public void EnableDieButton(bool value)
        {
            dieButton.Active = value;
        }
    }
}