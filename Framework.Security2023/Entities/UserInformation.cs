using System;

namespace Framework.Security2023.Entities
{
	public class UserInformation
	{
        private Guid idUser;
        private string name;
        private string lastName;
        private int age;
        private DateTime dateCreated;
        private string address;
        private string email;
        private Guid userCreated;

        public Guid IdUser { get => idUser; set => idUser = value; }
        public string Name { get => name; set => name = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public int Age { get => age; set => age = value; }
        public DateTime DateCreated { get => dateCreated; set => dateCreated = value; }
        public string Address { get => address; set => address = value; }
        public string Email { get => email; set => email = value; }
        public Guid UserCreated { get => userCreated; set => userCreated = value; }

        internal static UserInformation Create(Guid idUser, string name, string lastName, int age, DateTime dateCreated, string address, string email, Guid userCreated)
		{
			return new UserInformation( idUser,  name,  lastName,  age,  dateCreated,  address,  email,userCreated);
		}

		public static UserInformation Create( string name, string lastName, int age, string address, string email, Guid userCreated)
		{
			if (string.IsNullOrEmpty(name) && string.IsNullOrEmpty(lastName) 
				&& userCreated == Guid.Empty && string.IsNullOrEmpty(address) && string.IsNullOrEmpty(email))
				throw new ArgumentNullException("You cannot initialize the object with \"null\" or \"empty\" values.");

			if (age > 100 & age < 18)
				throw new ArgumentException("The prop \"age\" is not valid.");

			return new UserInformation( name, lastName, age, address, email, userCreated);
		}

		private UserInformation(string name, string lastName, int age, string address, string email, Guid userCreated)
		{
			Name = name;
			LastName = lastName;
			Age = age;
			DateCreated = DateTime.Now;
			Address = address;
			Email = email;
			UserCreated = userCreated;
		}

		private UserInformation(Guid idUser, string name, string lastName, int age, DateTime dateCreated, string address, string email, Guid userCreated)
		{
			IdUser = idUser;
			Name = name;
			LastName = lastName;
			Age = age;
			DateCreated = dateCreated;
			Address = address;
			Email = email;
			UserCreated = userCreated;
		}


	}
}
