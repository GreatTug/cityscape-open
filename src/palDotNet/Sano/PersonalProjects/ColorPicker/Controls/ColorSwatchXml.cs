using System.Collections;
using System.Drawing;
using System.IO;
using System.Text;
using System.Xml;
using Sano.Utility;

namespace Sano.PersonalProjects.ColorPicker.Controls;

internal class ColorSwatchXml
{
	private ColorSwatchXml()
	{
	}

	internal static ArrayList ReadSwatches(string file, bool isResourceFile)
	{
		ArrayList arrayList = new ArrayList();
		XmlTextReader xmlTextReader = null;
		try
		{
			xmlTextReader = ((!isResourceFile) ? new XmlTextReader(file) : new XmlTextReader(Resources.GetFileResource(file)));
			int red = 0;
			int green = 0;
			int blue = 0;
			bool flag = false;
			while (xmlTextReader.Read())
			{
				if (xmlTextReader.NodeType == XmlNodeType.Element && xmlTextReader.Name.CompareTo("color") == 0)
				{
					red = int.Parse(xmlTextReader.GetAttribute("red").ToString());
					green = int.Parse(xmlTextReader.GetAttribute("green").ToString());
					blue = int.Parse(xmlTextReader.GetAttribute("blue").ToString());
					flag = true;
				}
				else if (xmlTextReader.NodeType == XmlNodeType.Text && flag)
				{
					Color color = Color.FromArgb(red, green, blue);
					string description = xmlTextReader.ReadString();
					arrayList.Add(new ColorSwatch(color, description));
					flag = false;
				}
			}
			return arrayList;
		}
		finally
		{
			xmlTextReader?.Close();
		}
	}

	internal static void WriteSwatches(string file, ArrayList colors)
	{
		XmlTextWriter xmlTextWriter = null;
		try
		{
			xmlTextWriter = new XmlTextWriter(file, Encoding.UTF8);
			xmlTextWriter.Formatting = Formatting.Indented;
			xmlTextWriter.WriteStartDocument(standalone: false);
			xmlTextWriter.WriteStartElement("swatches");
			xmlTextWriter.WriteStartElement("swatch");
			xmlTextWriter.WriteAttributeString("id", "CustomSwatches");
			xmlTextWriter.WriteStartElement("colors");
			foreach (ColorSwatch color in colors)
			{
				xmlTextWriter.WriteStartElement("color");
				xmlTextWriter.WriteAttributeString("red", color.Color.R.ToString());
				xmlTextWriter.WriteAttributeString("green", color.Color.G.ToString());
				xmlTextWriter.WriteAttributeString("blue", color.Color.B.ToString());
				xmlTextWriter.WriteString(color.Description);
				xmlTextWriter.WriteEndElement();
			}
			xmlTextWriter.WriteEndElement();
			xmlTextWriter.WriteEndElement();
			xmlTextWriter.WriteEndElement();
			xmlTextWriter.WriteEndDocument();
		}
		catch (IOException)
		{
			throw;
		}
		finally
		{
			xmlTextWriter?.Close();
		}
	}
}
