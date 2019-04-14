using UnityEngine;
using System.Collections;
using TankGame.Databases;
using System.Collections.Generic;
using System.Linq;

namespace TankGame.Game
{
	[CreateAssetMenu]
	public class CardsDatabase : Database
	{
		[SerializeField] List<CardData> cards;

		public CardData GetCardData(string id)
		{
			var card = cards.FirstOrDefault(x => x.id == id);
			return card;
		}

		public List<CardData> GetRandomCards(int count)
		{
			const int DUPLICATE_COUNT = 5;
			var cardsWithDuplicates = new List<CardData>();
			foreach (var card in cards)
			{
				for (int i = 0; i < DUPLICATE_COUNT; i++)
				{
					cardsWithDuplicates.Add(card);
				}
			}

			var randomCards = new List<CardData>();
			for (int i = 0; i < count; i++)
			{
				var card = cardsWithDuplicates.OrderBy(x => Random.Range(0, int.MaxValue)).FirstOrDefault();
				randomCards.Add((CardData)card.Clone());
			}

			foreach (var card in randomCards)
			{
				card.Initialize();
			}
			return randomCards;
		}
	}
}