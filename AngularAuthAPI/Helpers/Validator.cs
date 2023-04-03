using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using System.Text;

namespace AuthProjectAPI.Helpers
{
    public class Validator
    {

        public static string CheckPasswordValid(string password)
        {
            StringBuilder invalidMessage = new StringBuilder();

            if (password.Length < 8)
                invalidMessage.Append("Password length should be greater than 8. " + Environment.NewLine);

            if (!(Regex.IsMatch(password, "[a-z]") && Regex.IsMatch(password, "[A-Z]") && Regex.IsMatch(password, "[0-9]")))
                invalidMessage.Append("Password should be Alphanumeric. " + Environment.NewLine);
            return invalidMessage.ToString();
        }


    }
}
