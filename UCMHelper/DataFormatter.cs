using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;

namespace UCMHelper
{
    public static class DataFormatter
    {
        public static DateTime SafeDate(object value)
        {
            DateTime retValue = Convert.ToDateTime("1/1/1900");
            try
            {
                retValue = Convert.ToDateTime(value);
            }
            catch (Exception) { }

            return retValue;
        }

        public static int SafeInt(object value)
        {
            int retValue = default(int);
            try
            {
                retValue = Convert.ToInt32(value);
            }
            catch (Exception) { }

            return retValue;
        }

        public static double SafeDouble(object value)
        {
            double retValue = default(double);
            try
            {
                retValue = Convert.ToDouble(value);
            }
            catch (Exception) { }

            return retValue;
        }

        public static bool SafeBoolean(object value)
        {
            bool retValue = default(bool);
            try
            {
                retValue = Convert.ToBoolean(value);
            }
            catch (Exception) { }
            return retValue;
        }


        public static string SafeString(object value)
        {
            string retValue = string.Empty;
            try
            {
                retValue = Convert.ToString(value);
            }
            catch (Exception) { }
            return retValue;
        }

        public static bool IsDate(string value)
        {
            bool isDate = true;

            try
            {
                DateTime dt = DateTime.Parse(value);
            }
            catch (Exception)
            {

                isDate = false;
            }

            return isDate;
        }

        public static string Encrypt(string stringToEncrypt)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(stringToEncrypt));
        }

        public static string Decrypt(string stringToDecrypt)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(stringToDecrypt));
        }

        public static bool isNumeric(string value)
        {
            int i; float f; decimal d;
            return int.TryParse(value, out i) ||
                float.TryParse(value, out f) ||
                decimal.TryParse(value, out d);
        }

        
    }
}
