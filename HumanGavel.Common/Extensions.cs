using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HumanGavel.Common
{
    public static class Extensions
    {
        public static String HashString(this string value)
        {            
            var hasher = MD5.Create();
            Guid userHash = new Guid(hasher.ComputeHash(Encoding.Unicode.GetBytes(value)));
            return userHash.ToString();
        }

        public static Guid HashBytes(this byte[] bytes)
        {
            var hasher = MD5.Create();
            Guid hash = new Guid(hasher.ComputeHash(bytes));
            return hash;
        }

        public static String ToHGDateTime(this DateTime dt)
        {
            return dt.ToString("MM/dd/yyyy hh:mm:ss.ms");
        }
    }
}
