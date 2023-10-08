using BrotliSharpLib;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace Flashopedia
{
	/// <summary>
	/// Contains the collection of the flashcards and additional information about them.
	/// </summary>
	public class FlashCardsPack
	{
		public Guid Id { get; set; }
		public string Title { get; set; }
		public string Note { get; set; }
		public List<FlashCard> FlashCards { get; set; }

		public byte[] Compress()
		{
			string jsonString = JsonConvert.SerializeObject(this, Formatting.None);
			byte[] inputBytes = Encoding.UTF8.GetBytes(jsonString);
			return Brotli.CompressBuffer(inputBytes, 0, inputBytes.Length, 9);
		}

		public static FlashCardsPack Decompress(byte[] bytes)
		{
			byte[] decompressedBytes = Brotli.DecompressBuffer(bytes, 0, bytes.Length);
			string jsonString = Encoding.UTF8.GetString(decompressedBytes);
			return JsonConvert.DeserializeObject<FlashCardsPack>(jsonString);
		}
	}
}
