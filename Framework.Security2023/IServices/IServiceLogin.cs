﻿using Framework.Security2023.Entities;

namespace Framework.Security2023.IServices
{
    public interface IServiceLogin
    {
        Login Login(Login user);
        Login LoginDummy(Login userLogin);
    }
}