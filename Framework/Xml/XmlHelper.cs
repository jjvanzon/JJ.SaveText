﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using JJ.Framework.Exceptions;

namespace JJ.Framework.Xml
{
	public static class XmlHelper
	{
		// XPath

		/// <summary>
		/// Gets the node that matches the XPath.
		/// If the node is not unique or not found, an exception is thrown with a descriptive error message.
		/// </summary>
		public static XmlNode SelectNode(XmlNode parentNode, string xpath)
		{
			XmlNode childNode = TrySelectNode(parentNode, xpath);
			if (childNode == null)
			{
				throw new Exception($"Node '{xpath}' does not exist in parent node '{parentNode.Name}'.");
			}
			return childNode;
		}

		/// <summary>
		/// Tries to get the node that matches the XPath.
		/// If the node is not unique, an exception is thrown with a descriptive error message.
		/// Null is returned if the node is not found.
		/// </summary>
		public static XmlNode TrySelectNode(XmlNode parentNode, string xpath)
		{
			if (parentNode == null) throw new NullException(() => parentNode);

			XmlNodeList nodes = parentNode.SelectNodes(xpath);
			if (nodes == null)
			{
				throw new NullException(() => parentNode.SelectNodes(xpath));
			}

			switch (nodes.Count)
			{
				case 0:
					return null;

				case 1:
					return nodes[0];

				default:
					throw new Exception($"Node '{xpath}' is not unique inside parent node '{parentNode.Name}'.");
			}
		}

		// Elements

		/// <summary>
		/// Gets a child element with a given name.
		/// If the element is not unique or not found, an exception is thrown with a descriptive error message.
		/// </summary>
		public static XmlElement GetElement(XmlElement parentElement, string childElementName)
		{
			if (parentElement == null) throw new NullException(() => parentElement);

			XmlElement childElement = TryGetElement(parentElement, childElementName);
			if (childElement == null)
			{
				throw new Exception($"Parent element '{parentElement.Name}' does not contain any element named '{childElementName}'.");
			}
			return childElement;
		}

		/// <summary>
		/// Tries to get a child element with a given name.
		/// Null is returned if the element is not found.
		/// If the element is not unique, an exception is thrown.
		/// </summary>
		public static XmlElement TryGetElement(XmlElement parentElement, string childElementName)
		{
			IList<XmlElement> childElements = GetElements(parentElement, childElementName);

			switch (childElements.Count )
			{
				case 0: 
					return null;

				case 1: 
					return childElements[0];

				default:
					throw new Exception($"Element '{childElementName}' was found multiple times inside '{parentElement.Name}'.");
			}
		}

		/// <summary>
		/// Gets the child elements with a given name.
		/// </summary>
		public static IList<XmlElement> GetElements(XmlElement parentElement, string childElementName)
		{
			if (parentElement == null) throw new NullException(() => parentElement);
			if (string.IsNullOrEmpty(childElementName)) throw new ArgumentException("childElementName cannot be null or white space.");

			// The outcommented line below selected descendents, not children.
			//return parentElement.GetElementsByTagName(childElementName).OfType<XmlElement>().ToArray();
			return parentElement.SelectNodes(childElementName)?.OfType<XmlElement>().ToArray() ?? new XmlElement[0];
		}

		// Attributes

		/// <summary>
		/// Gets an attribute with a given name.
		/// If the attribute is not found, an exception is thrown with a descriptive error message.
		/// </summary>
		public static XmlAttribute GetAttribute(XmlElement element, string attributeName)
		{
			XmlAttribute attribute = TryGetAttribute(element, attributeName);
			if (attribute == null)
			{
				throw new Exception($"Element '{element.Name}' does not contain attribute '{attributeName}'.");
			}
			return attribute;
		}

		/// <summary>
		/// Tries to get an attribute with a given name.
		/// Null is returned if the attribute is not found.
		/// </summary>
		public static XmlAttribute TryGetAttribute(XmlElement element, string attributeName)
		{
			if (element == null) throw new NullException(() => element);

			XmlAttribute attribute = element.GetAttributeNode(attributeName);
			return attribute;
		}

		/// <summary>
		/// Gets the value of an attribute with a given name.
		/// If the attribute is not found or the value is left empty,
		/// an exception is thrown with a descriptive error message.
		/// </summary>
		public static string GetAttributeValue(XmlElement element, string attributeName)
		{
			string attributeValue = TryGetAttributeValue(element, attributeName);
			if (string.IsNullOrEmpty(attributeValue))
			{
				throw new Exception($"Element '{element.Name}' does not specify attribute '{attributeName}'.");
			}
			return attributeValue;
		}

		/// <summary>
		/// Tries to get the value of an attribute with a given name.
		/// Null or empty string is returned if the attribute is not present or no value is filled in.
		/// </summary>
		public static string TryGetAttributeValue(XmlElement element, string attributeName)
		{
			if (element == null) throw new NullException(() => element);

			string attributeValue = element.GetAttribute(attributeName);
			return attributeValue;
		}

		public static void SetAttributeValue(XmlElement element, string attributeName, string value)
		{
			XmlAttribute xmlAttribute = GetAttribute(element, attributeName);
			xmlAttribute.Value = value;
		}
	}
}
