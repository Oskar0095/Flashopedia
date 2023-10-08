using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.IO;
using System.Text;

namespace Flashopedia
{
	public class FormatsHelper
	{
		public static string RawTextFromDocx(string path)
		{
			StringBuilder rawText = new StringBuilder();
			try
			{
				using (WordprocessingDocument doc = WordprocessingDocument.Open(path, false))
				{
					var body = doc.MainDocumentPart.Document.Body;

					if (body != null)
					{
						foreach (var paragraph in body.Elements<Paragraph>())
						{
							rawText.AppendLine(paragraph.InnerText);
						}
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("Error reading DOCX file: " + ex.Message);
			}

			return rawText.ToString();
		}

		public static string RawTextFromDocx(Stream docxStream)
		{
			StringBuilder rawText = new StringBuilder();
			try
			{
				using (WordprocessingDocument doc = WordprocessingDocument.Open(docxStream, false))
				{
					var body = doc.MainDocumentPart.Document.Body;

					if (body != null)
					{
						foreach (var paragraph in body.Elements<Paragraph>())
						{
							rawText.AppendLine(paragraph.InnerText);
						}
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("Error reading DOCX bytes: " + ex.Message);
			}

			return rawText.ToString(); ;
		}

		public static string RawTextFromDocx(byte[] docxBytes)
		{
			StringBuilder rawText = new StringBuilder();
			try
			{
				using (MemoryStream stream = new MemoryStream(docxBytes))
				{
					using (WordprocessingDocument doc = WordprocessingDocument.Open(stream, false))
					{
						var body = doc.MainDocumentPart.Document.Body;

						if (body != null)
						{
							foreach (var paragraph in body.Elements<Paragraph>())
							{
								rawText.AppendLine(paragraph.InnerText);
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("Error reading DOCX bytes: " + ex.Message);
			}

			return rawText.ToString(); ;
		}
	}
}
