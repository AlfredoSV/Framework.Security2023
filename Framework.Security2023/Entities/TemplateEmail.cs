using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Security2023.Entities
{
    public class TemplateEmail
    {
        private Guid _id;

        public Guid Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _nameTemplate;

        public string NameTemplate
        {
            get { return _nameTemplate; }
            set { _nameTemplate = value; }
        }

        private string _bodyTemplate;

        public string BodyTemplate
        {
            get { return _bodyTemplate; }
            set { _bodyTemplate = value; }
        }

        private DateTime _dateCreated;

        public DateTime DateCreated
        {
            get { return _dateCreated; }
            set { _dateCreated = value; }
        }


        private TemplateEmail(Guid id, string nameTemplate, string bodyTemplate, DateTime dateCreated)
        {
            _id = id;
            _nameTemplate = nameTemplate;
            _bodyTemplate = bodyTemplate;
            _dateCreated = dateCreated;
        }

        public static TemplateEmail Create(Guid id, string nameTemplate, string bodyTemplate, DateTime dateCreated)
        {
            return new TemplateEmail(id, nameTemplate, bodyTemplate, dateCreated);
        }
    }
}
