using JJ.Models.SetText.Persistence.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace JJ.Models.SetText.Persistence.Xml
{
    public class EntityRepository : IEntityRepository
    {
        // TODO: Create something reusable to easily use an XML files as the data store for entities.

        private string _location;
        private XmlDocument _document;
        
        public EntityRepository(string location)
        {
            _location = location;
            _document = new XmlDocument();
            _document.Load(location);
        }

        public Entity TryGet(int id)
        {
            XmlElement element = TryGetEntityXml(id);
            if (element == null)
            {
                return null;
            }

            return ConvertEntityXmlToEntity(element);
        }

        public Entity Get(int id)
        {
            Entity entity = TryGet(id);
            if (entity == null)
            {
                throw new Exception(String.Format("Entity with ID '{0}' not found.", id));
            }
            return entity;
        }

        public Entity Create()
        {
            XmlElement element = CreateEntityXml();
            int id = GetNewID();
            SetID(element, id);
            return ConvertEntityXmlToEntity(element);
        }

        public void Delete(Entity entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");

            DeleteEntityXml(entity.ID);
        }

        public void Update(Entity entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");

            UpdateXmlEntity(entity);
        }

        public void Commit()
        {
            _document.Save(_location);
        }

        // Helpers

        private XmlElement TryGetEntityXml(int id)
        {
            XmlElement root = GetRoot();
            string xpath = String.Format("entity[@id='{0}']", id);
            return (XmlElement)TryGetNode(root, xpath);
        }

        private XmlElement GetEntityXml(int id)
        {
            XmlElement element = TryGetEntityXml(id);
            if (element == null)
            {
                throw new Exception(String.Format("XML element '{0}' with ID '{1}' not found.", "entity", id));
            }
            return element;
        }

        private XmlElement CreateEntityXml()
        {
            XmlElement root = GetRoot();

            XmlElement element = _document.CreateElement("entity");
            root.AppendChild(element);

            XmlAttribute idAttribute = _document.CreateAttribute("id");
            element.Attributes.Append(idAttribute);

            XmlElement textElement = _document.CreateElement("text");
            element.AppendChild(textElement);

            return element;
        }

        private XmlElement GetRoot()
        {
            string xpath = "root";
            XmlElement element = (XmlElement)GetNode(_document, xpath);
            return element;
        }

        private Entity ConvertEntityXmlToEntity(XmlElement element)
        {
            return new Entity
            {
                ID = GetID(element),
                Text = GetText(element)
            };
        }

        private XmlElement UpdateXmlEntity(Entity entity)
        {
            XmlElement element = GetEntityXml(entity.ID);
            SetID(element, entity.ID);
            SetText(element, entity.Text);
            return element;
        }

        private void DeleteEntityXml(int id)
        {
            XmlElement element = GetEntityXml(id);
            _document.RemoveChild(element);
        }

        private int GetID(XmlElement element)
        {
            string xpath = "@id";
            XmlAttribute attribute = (XmlAttribute)GetNode(element, xpath);
            string text = attribute.InnerText;
            int id;
            if (!Int32.TryParse(text, out id))
            {
                throw new Exception(String.Format("ID attribute of XmlElement '{0}' is not an integer number.", element.Name));
            }
            return id;
        }

        private void SetID(XmlElement element, int value)
        {
            string xpath = "@id";
            XmlAttribute attribute = (XmlAttribute)GetNode(element, xpath);
            attribute.Value = value.ToString();
        }

        private string GetText(XmlElement element)
        {
            string xpath = "text";
            XmlElement element2 = (XmlElement)GetNode(element, xpath);
            return element2.InnerText;
        }

        private void SetText(XmlElement element, string value)
        {
            string xpath = "text";
            XmlElement element2 = (XmlElement)GetNode(element, xpath);
            element2.InnerText = value;
        }

        // ID's

        private int GetNewID()
        {
            _maxID = GetMaxID();
            _maxID++;
            return _maxID;
        }

        private int _maxID = 0;

        private int GetMaxID()
        {
            if (_maxID != 0)
            {
                return _maxID;
            }

            string xpath = "entity[not(@id > ../@id)]";
            XmlElement elementWithMaxID = (XmlElement)TryGetNode(_document, xpath);
            if (elementWithMaxID != null)
            {
                return GetID(elementWithMaxID);

            }

            return 0;
        }

        // GetNode

        private XmlNode GetNode(XmlNode parentNode, string xpath)
        {
            XmlNode node = TryGetNode(parentNode, xpath);
            if (node == null)
            {
                throw new Exception(String.Format("Node '{0}' does not exist in parent node '{1}'.", xpath, parentNode.Name));
            }
            return node;
        }

        private XmlNode TryGetNode(XmlNode parentNode, string xpath)
        {
            XmlNodeList nodes = parentNode.SelectNodes(xpath);
            switch (nodes.Count)
            {
                case 0:
                    return null;

                case 1:
                    return nodes[0];

                default:
                    throw new Exception(String.Format("Node '{0}' is not unique.", xpath));
            }
        }
    }
}
