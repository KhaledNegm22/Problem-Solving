using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problem
{

    public static class IntegerMultiplication
    {

        static public byte[] IntegerMultiply(byte[] X, byte[] Y, int N)
        {
            byte[] ans = new byte[2 * N];
            if (X.Length < Y.Length)
            {
                Array.Resize(ref X, Y.Length);
            }

            if (X.Length > Y.Length)
            {
                Array.Resize(ref Y, X.Length);
                N = X.Length;
            }
            else
            {
                N = Y.Length;
            }
            if (N == 1) // base case
            {
                ans[0] = (byte)(X[0] * Y[0] % 10);
                ans[1] = (byte)(X[0] * (int)Y[0] / 10);
                return ans;
            }
            if (N % 2 != 0)
            {
                X = padding(X, 1, X.Length, 0);
                Y = padding(Y, 1, Y.Length, 0);
                N++;
            }

            int k = N / 2;
            byte[] a = new byte[k];
            byte[] b = new byte[k];
            byte[] c = new byte[k];
            byte[] d = new byte[k];

            for (int i = 0; i < k; i++)
            {
                b[i] = X[i];
                d[i] = Y[i];
            }
            int cnt = k;
            for (int i = 0; i < k; i++)
            {
                a[i] = X[cnt];
                c[i] = Y[cnt];
                cnt++;
            }

            byte[] ac = IntegerMultiply(a, c, k);
            byte[] bd = IntegerMultiply(b, d, k);
            byte[] AplusB = AddByteArrays(a, b);
            byte[] CplusD = AddByteArrays(c, d);
            //PadToEqualEvenLength(ref AplusB, ref CplusD);
            byte[] z = IntegerMultiply(AplusB, CplusD, k + 1);

            /*byte[] ZminusM1minusM2 = SubtractByteArrays(SubtractByteArrays(z, m1), m2);

            byte[] ddd = PaddingRight(m2, N);
            byte[] sss = PaddingRight(ZminusM1minusM2, k);

            byte[] result = AddByteArrays(ddd, AddByteArrays(sss, m1));
            Array.Resize(ref result, N * 2);
            return result;*/
            byte[] tot = AddByteArrays(bd, ac);
            byte[] q = SubtractByteArrays(z, tot);
            ac = padding(ac, N, ac.Length, 1);
            q = padding(q, N / 2, q.Length, 1);
            ans = AddByteArrays(ac, q);
            ans = AddByteArrays(ans, bd);
            Array.Resize(ref ans, N * 2);
            return ans;
        }
        private static byte[] padding(byte[] s, int offset, int originalArraySize, int Left)
        {
            // 1 for left   0 for right
            // left will be at the end of array NPC zeroes
            // right will be beginning of array (Multiply by 10 ^ offset)
            int len = originalArraySize + offset;
            byte[] res = new byte[len];
            if (Left == 0)
            {
                for (int i = 0; i < originalArraySize; i++)
                {
                    res[i] = (byte)s[i];
                }
                int cnt = originalArraySize;
                for (int i = 0; i < offset; i++)
                {
                    res[cnt] = (byte)0;
                    cnt++;
                }
                return res;
            }
            else
            {
                for (int i = 0; i < offset; i++)
                {
                    res[i] = (byte)0;
                }
                int cnt = offset;
                for (int i = 0; i < originalArraySize; i++)
                {
                    res[cnt] = (byte)s[i];
                    cnt++;
                }
                return res;
            }
        }

        public static void PadToEqualEvenLength(ref byte[] a, ref byte[] b)
        {
            int maxLength = Math.Max(a.Length, b.Length);

            // If maxLength is odd, add 1 to make it even
            if (maxLength % 2 != 0)
            {
                maxLength++;
            }

            // Pad a with zeros
            int numZerosToAdd = maxLength - a.Length;
            if (numZerosToAdd > 0)
            {
                byte[] padded = new byte[maxLength];
                Array.Copy(a, 0, padded, 0, a.Length);
                a = padded;
            }

            // Pad b with zeros
            numZerosToAdd = maxLength - b.Length;
            if (numZerosToAdd > 0)
            {
                byte[] padded = new byte[maxLength];
                Array.Copy(b, 0, padded, 0, b.Length);
                b = padded;
            }
        }

        static byte[] AddByteArrays(byte[] a, byte[] b)
        {
            int n = Math.Max(a.Length, b.Length);
            PadToEqualEvenLength(ref a, ref b);

            byte[] result = new byte[n + 1];
            byte carry = 0;
            for (int i = 0; i < n; i++)
            {
                byte sum = (byte)(a[i] + b[i] + carry);
                carry = (byte)(sum / 10);
                result[i] = (byte)(sum % 10);
            }
            result[n] = carry;
            return result;
        }
        static byte[] SubtractByteArrays(byte[] a, byte[] b)
        {
            PadToEqualEvenLength(ref a, ref b);

            int n = Math.Max(a.Length, b.Length);
            // we assume a > b
            byte[] result = new byte[n];
            int borrow = 0;
            for (int i = 0; i < n; i++)
            {
                int A = a[i], B = b[i];
                int d = A - B - borrow;
                if (d < 0)
                {
                    borrow = 1;
                    d += 10;
                }
                else
                {
                    borrow = 0;
                }

                result[i] = (byte)d;

            }
            return result;
        }
    }

}

