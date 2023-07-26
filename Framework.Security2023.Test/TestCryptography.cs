using Framework.Security2023.Cryptography;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Security2023.Test
{
    [TestClass]
    public class TestCryptography
    {
        [TestMethod]
        public void Test_EncryptAndDescryptString()
        {
            ServiceCryptography serviceCryptography = new ServiceCryptography();

            Guid key = Guid.Parse("147CD742-52BB-4080-AAC4-8B82BB0B2571");

            string password = "123%%$";

            string passwordEncrypt = serviceCryptography.
                Encrypt(password, key.ToString());

            string passwordDescrypt = serviceCryptography.
                Descrypt(passwordEncrypt, key.ToString());

            Assert.IsTrue(password.Equals(passwordDescrypt));


        }
    }
}
