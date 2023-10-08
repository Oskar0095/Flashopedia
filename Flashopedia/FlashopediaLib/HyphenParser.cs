using System;
using System.Collections.Generic;

namespace Flashopedia
{
	public class HyphenParser
	{
		public enum ParseMode
		{
			/// <summary>
			/// Line with hyphen will be parsed as:
			/// <para>(left side) definition - term (right side)</para>
			/// </summary>
			DefinitionTerm,
			/// <summary>
			/// Line with hyphen will be parsed as:
			/// <para>(left side) term - definition (right side)</para>
			/// </summary>
			TermDefinition
		}

		public static List<FlashCard> Parse(string text, ParseMode parseMode)
		{
			string[] lines = text.Split(new char[] { '\n' }, StringSplitOptions.None);
			List<FlashCard> flashCards = new List<FlashCard>();
			for (int i = 0; i < lines.Length; i++)
			{
				string[] split = lines[i].Split(new string[] { " - ", " – " }, StringSplitOptions.None);
				if (split.Length == 2)
				{
					string term = parseMode == ParseMode.DefinitionTerm ? split[1] : split[0];
					string definition = parseMode == ParseMode.DefinitionTerm ? split[0] : split[1];

					FlashCard flashCard = new FlashCard
					{
						Term = term.Trim(),
						Definition = definition.Trim()
					};
					flashCards.Add(flashCard);
				}

			}
			return flashCards;
		}
	}
}