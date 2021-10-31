using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aptek.Models
{
    class User
    {
        private string _login;

        public string _password;

        public string Login
        {
            get
            {
                return _login;
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
                _login = value;
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
                int isUpper = 0, isLower = 0;
                
                foreach (char item in value)
                {
                    if (!char.IsLetter(item) && !char.IsDigit(item))
                    {
                        return;
                    }
                    if (char.IsUpper(item))
                    {
                        isUpper++;
                    }
                    if (char.IsLower(item))
                    {
                        isLower++;
                    }
                }
                if (isUpper > 0 && isLower > 0)
                {
                    _login = value;
                }
                else return;
            }
        }
    }
}
