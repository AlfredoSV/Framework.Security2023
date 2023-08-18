
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Security2023.Entities
{
    public class Permission
    {
        private string _perrmision;
        private Guid _id;
        private Guid _rolId;
        private string _description;
        private string _module;

        private Permission(string perrmision, Guid id, Guid rolId,string description, string module)
        {
            _perrmision = perrmision;
            _id = id;
            _description = description;
            _module = module;
            _rolId = rolId;
        }

        public static Permission Create(string permision, Guid id, Guid rolId, string description, string module)
        {
            return new Permission(permision, id, rolId , description,module);
        }
    }
}
