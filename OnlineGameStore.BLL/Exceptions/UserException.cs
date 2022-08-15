using System;

namespace OnlineGameStore.BLL.Exceptions
{
    public class UserException : InvalidOperationException
    {
        public string[] Errors { get; }
        
        public UserException(params string[] errors)
        {
            Errors = errors;
        }
    }
}