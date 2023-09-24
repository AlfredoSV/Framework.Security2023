using System;

namespace Framework.Security2023.Entities
{
    public class Permission
    {
        private string _permision;
        private Guid _id;
        private Guid _rolId;
        private string _description;
        private string _module;


        public string Permision { get => _permision; set => _permision = value; }
        public Guid Id { get => _id; set => _id = value; }
        public Guid RolId { get => _rolId; set => _rolId = value; }
        public string Description { get => _description; set => _description = value; }
        public string Module { get => _module; set => _module = value; }


        private Permission(string perrmision, Guid id, Guid rolId,string description, string module)
        {
            Permision = perrmision;
            Id = id;
            Description = description;
            Module = module;
            RolId = rolId;
        }

        public static Permission Create(string permision, Guid id, Guid rolId, string description, string module)
        {
            return new Permission(permision, id, rolId , description,module);
        }
    }
}
