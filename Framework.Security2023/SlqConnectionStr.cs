﻿namespace Framework.Security2023
{
    public class SlqConnectionStr
    {
        private static SlqConnectionStr _instance;

        public static SlqConnectionStr Instance {

            get {
                if (_instance == null)               
                    _instance = new SlqConnectionStr();
                    
                return _instance;
            }

            private set { }
        }

        public string SqlConnectionString
        {
            get;

            set;
        
        }     
        private SlqConnectionStr() { }  

    }
}
