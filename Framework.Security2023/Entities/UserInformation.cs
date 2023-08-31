using System;

namespace Framework.Security2023.Entities
{
	public class UserInformation
	{
		public Guid IdUser { get; private set; }
		public string Name { get; private set; }
		public string LastName { get; private set; }
		public int Age { get; private set; }
		public DateTime DateCreated { get; private set; }
		public string Address { get; set; }
		public string Email { get; set; }
		public Guid UserCreated { get; set; }

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

		internal void SetIdUser(Guid idUser)
        {
			IdUser = idUser;
        }
	}
}
