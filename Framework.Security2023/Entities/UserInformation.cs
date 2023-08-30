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

		public static UserInformation Create(Guid idUser, string name, string lastName, int age, DateTime dateCreated, string address, string email, Guid userCreated)
		{
			return new UserInformation( idUser,  name,  lastName,  age,  dateCreated,  address,  email,userCreated);
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
