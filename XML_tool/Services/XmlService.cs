using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace XML_tool.Services
{
    public class XmlService
    {
        private static string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "test.xml");

        public static XmlDocument ReadXml()
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(path);
                return doc;
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File not found");
            }

            return null;
        }

        public static List<XmlNode> ParseXml(XmlDocument doc)
        {
            List<XmlNode> nodes = new List<XmlNode>();

            if (doc != null)
            {
                foreach (XmlNode node in doc.DocumentElement.ChildNodes)
                {
                    nodes.Add(node);
                }
            }

            return nodes;
        }

        public static void SaveXml(XmlDocument doc)
        {
            doc.Save(path);
        }

        public static string Edit_xml(List<XmlNode> node, string xmlValueType, string author, string title, string year, string newValue, XmlDocument doc)
        {
            XmlNode current_xmlNode = null;
            foreach (XmlNode book in node)
            {
                if (book.SelectSingleNode("Title").InnerText == title && book.SelectSingleNode("Author").InnerText == author && book.SelectSingleNode("Year").InnerText == year)
                {
                    current_xmlNode = book;
                    break;
                }
            }

            if (current_xmlNode == null)
            {
                Console.WriteLine("The book was not found");
                return null;
            }

            switch (xmlValueType)
            {
                case "Title":
                    current_xmlNode.SelectSingleNode("Title").InnerText = newValue;
                    break;
                case "Author":
                    current_xmlNode.SelectSingleNode("Author").InnerText = newValue;
                    break;
                case "Year":
                    current_xmlNode.SelectSingleNode("Year").InnerText = newValue;
                    break;
            }

            Save_xml(doc);
            return null;
        }

        public static void Create_new_book(List<XmlNode> node, string title, string author, string year, XmlDocument doc)
        {
            XmlNode newBook = node[0].Clone();
            newBook.SelectSingleNode("Title").InnerText = title;
            newBook.SelectSingleNode("Author").InnerText = author;
            newBook.SelectSingleNode("Year").InnerText = year;
            node[0].ParentNode.AppendChild(newBook);

            Save_xml(doc);
        }

        public static void Delete_book(List<XmlNode> node, string title, string author, string year, XmlDocument doc)
        {
            XmlNode current_xmlNode = null;

            foreach (XmlNode book in node)
            {
                if (book.SelectSingleNode("Title").InnerText == title && book.SelectSingleNode("Author").InnerText == author && book.SelectSingleNode("Year").InnerText == year)
                {
                    current_xmlNode = book;
                    break;
                }
            }

            if (current_xmlNode == null)
            {
                Console.WriteLine("The book was not found");
                return;
            }

            current_xmlNode.ParentNode.RemoveChild(current_xmlNode);
            Save_xml(doc);
        }

        public static XmlDocument Save_xml(XmlDocument doc)
        {
            doc.Save(path);
            return doc;
        }

        public static XmlDocument Copy_Xml(XmlDocument doc, string name, string path)
        {
            XmlDocument xmlDoc = new XmlDocument();

            XmlNode importedRoot = xmlDoc.ImportNode(doc.DocumentElement, true);

            xmlDoc.AppendChild(importedRoot);

            xmlDoc.Save(path);

            return xmlDoc;
        }
    }
}
