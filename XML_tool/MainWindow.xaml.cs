using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

class Program
{
    public static string path = "C:\\Users\\worys\\OneDrive\\Pulpit\\NaukaH\\c#_vscode\\ConsoleApp1\\test.xml";

    public static XmlDocument Read_Xml(string path)
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

    public static List<XmlNode> Parse_xml(XmlDocument doc)
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

    public static string Print_xml_values(List<XmlNode> node)
    {
        foreach (XmlNode book in node)
        {
            if (book == null)
            {
                Console.WriteLine("No books found");
                return null;
            }

            string title = book.SelectSingleNode("Title").InnerText ?? "No Title";
            string author = book.SelectSingleNode("Author").InnerText ?? "No author";
            string year = book.SelectSingleNode("Year").InnerText ?? "No year";

            Console.WriteLine($"Title: {title}, Author: {author}, Year: {year} \n");
        }

        return null;
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

    public static XmlDocument Copy_Xml(XmlDocument doc, string name)
    {
        string path = "C:\\Users\\worys\\OneDrive\\Pulpit\\NaukaH\\c#_vscode\\ConsoleApp1\\" + name + ".xml";
        XmlDocument xmlDoc = new XmlDocument();

        XmlNode importedRoot = xmlDoc.ImportNode(doc.DocumentElement, true);

        xmlDoc.AppendChild(importedRoot);

        xmlDoc.Save(path);

        return xmlDoc;
    }
    public static void Main()
    {
        XmlDocument reading = Read_Xml(path);
        List<XmlNode> node = Parse_xml(reading);
        Console.WriteLine("Lista książek: \n");
        string text = Print_xml_values(node);
        Edit_xml(node, "Title", "Henryk Sienkiewicz", "Potop", "1886", "Test", reading);
        //Create_new_book(node, "Test", "Test", "Test", reading);
        Delete_book(node, "Test", "Test", "Test", reading);

        Copy_Xml(reading, "test2");

    }
}
