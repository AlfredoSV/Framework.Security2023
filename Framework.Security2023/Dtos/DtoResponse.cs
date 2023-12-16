using System;


namespace Framework.Security2023.Dtos
{
    public class DtoResponse<T> where T : class
    {
        private T _data;
        public T Data
        {
            get {
                if (_data == null)
                    throw new NullReferenceException($"{Data.GetType().Name} is null.");
                return _data;  
            }
            set
            {
                if (value == null)              
                    throw new NullReferenceException($"What you are trying to assign to {Data.GetType().Name} is null.");
                _data = value;
            }
        }
        public bool HasData => this._data != null;
    }

}
