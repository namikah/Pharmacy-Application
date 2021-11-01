using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aptek.Models
{
    class User
    {
        private string _userName;

        public string _password;

        private string _status;

        public string UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                foreach (char item in value)
                {
                    if(!char.IsLetter(item) && !char.IsDigit(item))
                    {
                        return;
                    }
                }
                _userName = value;
            }
        }

        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                int isUpper = 0, isLower = 0, isDigit = 0, isLetter = 0;

                foreach (char item in value)
                {
                    if (char.IsDigit(item))
                    {
                        isDigit++;
                    }
                    else if (char.IsLetter(item))
                    {
                        isLetter++;

                        if (char.IsUpper(item))
                        {
                            isUpper++;
                        }
                        else if (char.IsLower(item))
                        {
                            isLower++;
                        }
                    }
                }
                if (isUpper > 0 && isLower > 0 && isDigit > 0 && isLetter > 0)
                {
                    _password = value;
                }
                else return;
            }
        }

        public int Id { get; }

        private static int _idCounter;

        public string Status
        {
            get
            {
                return _status;
            }
            set
            {
                if(value == "Admin" || value == "User")
                {
                    _status = value;
                }
                return;
            }
        }

        public User(string userName, string passWord, string status)
        {
            UserName = userName;
            Password = passWord;
            Status = status;
            _idCounter++;
            Id = _idCounter;
        }

        public override string ToString()
        {
            return $"[{Id}] - [{_userName}] - [{_password}] - [{_status}]";
        }
    }
}
