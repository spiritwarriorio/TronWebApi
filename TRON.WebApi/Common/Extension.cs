
namespace TRON.WebApi.Common
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Globalization;
    using System.Linq;
    using System.Linq.Expressions;

    using Org.BouncyCastle.Math;
    using Org.BouncyCastle.Math.EC;
    using Org.BouncyCastle.Crypto.Parameters;
    using Org.BouncyCastle.Asn1.Sec;
    using Org.BouncyCastle.Asn1.X9;
    using Org.BouncyCastle.Security;
    using Tron.Net.Common;
    using Org.BouncyCastle.Crypto.Digests;
    using System.Text;
    using System.IO;

    public static class Extension
    {
        private const string CurveName = "secp256k1";
        private static ECDomainParameters Curve;
        private static X9ECParameters Params;
        private static ECPoint Public;
        private const string addressPrefix = "41";   //41 + address

        public static string EstandarGuionFecha(this DateTime d) => (d == DateTime.MinValue
                                                         ? "-"
                                                         : d.ToString(CultureInfo.CurrentCulture));

        public static void SelectItemsList(this string s, Action<int> fnSeleccionar, char sep = '|')
        {
            if (!string.IsNullOrEmpty(s))
            {
                var lst = Array.ConvertAll(s.Split(sep), int.Parse).ToList();
                if (lst.IsNotNull()) { foreach (var item in lst) { fnSeleccionar(item); } }
            }
        }

        public static List<int> SelectItemsList(this string s, char sep = '|')
        {
            if (!string.IsNullOrEmpty(s))
            {
                var lst = Array.ConvertAll(s.Split(sep), int.Parse).ToList();
                if (lst.IsNotNull()) { return lst; }
            }

            return null;
        }

        public static bool IsNotNull<T>(this List<T> lstValid) => (lstValid != default(List<T>) && lstValid.Count > 0);

        public static bool IsNotNull<T>(this Collection<T> lstValid) => (lstValid != default(Collection<T>) && lstValid.Count > 0);

        public static bool IsNotNull<T>(this ICollection<T> lstValid) => (lstValid != null && lstValid.Count > 0);

        public static bool IsNotNull<T>(this T valid) where T : class => (valid != default(T));

        public static bool IsValid(this string s) => (s.IsNotNull() && s.Trim().Length > 0);

        public static bool IsNotNull<T>(this T[] valid) => (valid != default(T[]) && valid.Length > 0);

        public enum Order
        {
            Asc,
            Desc
        }

        public static IQueryable<T> OrderByDynamic<T>(
            this IQueryable<T> query,
            string orderByMember,
            Order direction)
        {
            var queryElementTypeParam = Expression.Parameter(typeof(T));
            var memberAccess = Expression.PropertyOrField(queryElementTypeParam, orderByMember);
            var keySelector = Expression.Lambda(memberAccess, queryElementTypeParam);

            var orderBy = Expression.Call(
                typeof(Queryable),
                direction == Order.Asc ? "OrderBy" : "OrderByDescending",
                new Type[] { typeof(T), memberAccess.Type },
                query.Expression,
                Expression.Quote(keySelector));

            return query.Provider.CreateQuery<T>(orderBy);
        }

        public static string privKey2PubKey(string privateKey)
        {
            string pubKeyStr = "";

            var privKeyBytes = Utils.FromHexToByteArray(privateKey);
            Params = SecNamedCurves.GetByName(CurveName);
            Curve = new ECDomainParameters(Params.Curve, Params.G, Params.N, Params.H);
            BigInteger privKey = new BigInteger(1, privKeyBytes);
            Public = Curve.G.Multiply(privKey);
            pubKeyStr = Utils.ToHexString(Public.GetEncoded());
            pubKeyStr = ToB58(Public);
            return pubKeyStr;

        }

        public static string ToB58(ECPoint publicKey)
        {
            var pubKeyBytes = Utils.ToHexString(Public.GetEncoded()).Substring(2, 128).FromHexToByteArray();
            var sha3 = CalculateHash(pubKeyBytes);
            var sha3HashBytes = new byte[20];
            Array.Copy(sha3, sha3.Length - 20, sha3HashBytes, 0, 20);
            var address = addressPrefix + sha3HashBytes.ToHexString();
            var addr = address.FromHexToByteArray();
            var sha256_1 = HashTwice(addr);
            var bytes = new byte[4];
            Array.Copy(sha256_1, bytes, 4);
            var checksum = bytes.ToHexString();
            var addChecksum = (address + checksum).FromHexToByteArray();
            Array.Copy(addr, addChecksum, addr.Length);
            Console.WriteLine("CheckS: " + Utils.ToHexString(addChecksum));
            return Encode(addChecksum);
        }

        public static byte[] CalculateHash(byte[] value)
        {
            var digest = new KeccakDigest(256);
            var output = new byte[digest.GetDigestSize()];
            digest.BlockUpdate(value, 0, value.Length);
            digest.DoFinal(output, 0);
            return output;
        }
        public static byte[] HashTwice(byte[] input)
        {
            return Hash(Hash(input));
        }
        public static byte[] Hash(byte[] input)
        {
            byte[] hashBytes;
            var hash = new System.Security.Cryptography.SHA256Managed();
            using (var stream = new MemoryStream(input))
            {
                try
                {
                    hashBytes = hash.ComputeHash(stream);
                }
                finally
                {
                    hash.Clear();
                }
            }
            return hashBytes;
        }

        // ===================
        /// Base58 functions
        // ===================
        public static readonly char[] Alphabet = "123456789ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz".ToCharArray();
        private static byte[] CopyOfRange(byte[] source, int from, int to)
        {
            var range = new byte[to - from];
            Array.Copy(source, from, range, 0, range.Length);
            return range;
        }
        private static byte Divmod58(byte[] number, int startAt)
        {
            var remainder = 0;
            for (var i = startAt; i < number.Length; i++)
            {
                var digit256 = (int)number[i] & 0xFF;
                var temp = remainder * 256 + digit256;
                number[i] = (byte)(temp / 58);
                remainder = temp % 58;
            }
            return (byte)remainder;
        }
        public static string Encode(byte[] input)
        {
            if (input.Length == 0)
            {
                return "";
            }
            input = CopyOfRange(input, 0, input.Length);
            var zeroCount = 0;
            while (zeroCount < input.Length && input[zeroCount] == 0)
            {
                ++zeroCount;
            }
            var temp = new byte[input.Length * 2];
            var j = temp.Length;
            var startAt = zeroCount;
            while (startAt < input.Length)
            {
                var mod = Divmod58(input, startAt);
                if (input[startAt] == 0)
                {
                    ++startAt;
                }
                temp[--j] = (byte)Alphabet[mod];
            }
            while (j < temp.Length && temp[j] == Alphabet[0])
            {
                ++j;
            }
            while (--zeroCount >= 0)
            {
                temp[--j] = (byte)Alphabet[0];
            }
            var output = CopyOfRange(temp, j, temp.Length);
            return Encoding.ASCII.GetString(output);
        }
    }
}