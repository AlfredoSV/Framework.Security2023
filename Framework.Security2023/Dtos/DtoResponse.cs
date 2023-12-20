using System;


namespace Framework.Security2023.Dtos
{
    public class DtoResponse<T>
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
                _data = value;
            }
        }
        public bool HasData => this._data != null;

        public static DtoResponse<T> Create(T data)
        {
            return new DtoResponse<T>() { Data = data};
        }
    }

}
