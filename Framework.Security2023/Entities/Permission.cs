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
        private DateTime _DateCreated;
        private Guid _userCreated;
        private bool _active;

        public Guid Id { get => _id; set => _id = value; }
        public Guid RolId { get => _rolId; set => _rolId = value; }
        public string Permision { get => _permision; set => _permision = value; }
        public string Description { get => _description; set => _description = value; }
        public string Module { get => _module; set => _module = value; }
        public DateTime DateCreated { get => _DateCreated; set => _DateCreated = value; }
        public Guid UserCreated { get => _userCreated; set => _userCreated = value; }
        public bool Active { get => _active; set => _active = value; }

        private Permission(string perrmision, Guid id, Guid rolId, string description, string module,
            DateTime dateCreated, Guid userCreated, bool active)
        {
            Permision = perrmision;
            Id = id;
            Description = description;
            Module = module;
            RolId = rolId;
            DateCreated = dateCreated;
            UserCreated = userCreated;
            Active = active;
        }

        public static Permission Create(string permision, Guid id, Guid rolId, string description, string module, DateTime dateCreated, Guid userCreated, bool active)
        {
            return new Permission(permision, id, rolId, description, module, dateCreated, userCreated, active);
        }
    }
}
