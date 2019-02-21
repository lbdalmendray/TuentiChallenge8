using System;
using System.Collections.Generic;
using System.Linq;

public class InfiniteNumber
{
    #region Static definition 

    public static string[] digits;
    private static Dictionary<int, string> intDigitRelation;
    private static Dictionary<string, int> DigitIntRelation;

    public static String[] Digits
    {
        get
        {
            return digits;
        }
        set
        {
            digits = value;
            intDigitRelation = new Dictionary<int, string>(value.Length);
            DigitIntRelation = new Dictionary<string, int>(value.Length);
            for (int i = 0; i < value.Length; i++)
            {
                intDigitRelation.Add(i, value[i]);
                DigitIntRelation.Add(value[i], i);
            }
        }
    }
    public static string NumberSeparator { get; set; } = "";

    static InfiniteNumber()
    {
        Digits = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ".Select(e => e.ToString()).ToArray();
    }

    #endregion

    #region Operators
    public static InfiniteNumber operator +(InfiniteNumber a, InfiniteNumber b)
    {
        if (b == null)
            throw new Exception("number is null");
        //if (b.Base != a.Base)
        //    b = b.ToBase10(a.Base);
        if (a.IntArrayValue.Length < b.IntArrayValue.Length)
        {
            var auxNumber = a;
            a = b;
            b = auxNumber;
        }
        int baseNum = a.Base;
        LinkedList<int> result = new LinkedList<int>();
        int accumulate = 0;
        int aLength = a.IntArrayValue.Length;
        int bLength = b.IntArrayValue.Length;
        int i = 0;
        for (; i < b.IntArrayValue.Length; i++)
        {
            var newValue = a.IntArrayValue[aLength - 1 - i] + b.IntArrayValue[bLength - 1 - i] + accumulate;
            accumulate = newValue >= baseNum ? 1 : 0;
            int newIntDigit = newValue % baseNum;
            result.AddFirst(newIntDigit);
        }

        if (aLength == bLength)
        {
            if (accumulate > 0)
                result.AddFirst(accumulate);
        }
        else
        {
            for (; i < a.IntArrayValue.Length; i++)
            {
                var newValue = a.IntArrayValue[aLength - 1 - i] + accumulate;
                accumulate = newValue >= baseNum ? 1 : 0;
                int newIntDigit = newValue % baseNum;
                result.AddFirst(newIntDigit);
            }

            if (accumulate > 0)
                result.AddFirst(accumulate);
        }

        return new InfiniteNumber(result.ToArray(), baseNum);
    }

    public static InfiniteNumber operator -(InfiniteNumber a, InfiniteNumber b)
    {
        if (b == null)
            throw new Exception("number is null");
        //if (b.Base != a.Base)
        //    b = b.ToBase10(a.Base);
        if (a.IntArrayValue.Length < b.IntArrayValue.Length)
        {
            var auxNumber = a;
            a = b;
            b = auxNumber;
        }

        int baseNum = a.Base;
        LinkedList<int> result = new LinkedList<int>();
        int accumulate = 0;
        int aLength = a.IntArrayValue.Length;
        int bLength = b.IntArrayValue.Length;
        int i = 0;
        for (; i < b.IntArrayValue.Length; i++)
        {
            var differece = a.IntArrayValue[aLength - 1 - i] - b.IntArrayValue[bLength - 1 - i] + accumulate;
            int newIntDigit = differece >= 0 ? differece : baseNum + differece;
            accumulate = differece >= 0 ? 0 : -1;
            result.AddFirst(newIntDigit);
        }

        if (aLength > bLength)
        {
            for (; i < a.IntArrayValue.Length; i++)
            {
                var differece = a.IntArrayValue[aLength - 1 - i] + accumulate;
                int newIntDigit = differece >= 0 ? differece : baseNum + differece;
                accumulate = differece >= 0 ? 0 : -1;
                result.AddFirst(newIntDigit);
            }
        }

        var firstNotZeroIndex = result.Select((v, index) => new { v, index }).FirstOrDefault(v => v.v != 0);
        if (firstNotZeroIndex == null)
            return new InfiniteNumber(new int[1] { 0 }, baseNum);
        else
            return new InfiniteNumber(result.Where((v, ind) => ind >= firstNotZeroIndex.index).ToArray(), baseNum);
    }

    public static InfiniteNumber operator *(InfiniteNumber a, InfiniteNumber b)
    {
        if (b == null)
            throw new Exception("number is null");
        //if (b.Base != a.Base)
        //    b = b.ToBase10(a.Base);
        if (a.IntArrayValue.Length == 1 && a.IntArrayValue[0] == 0 ||
            b.IntArrayValue.Length == 1 && b.IntArrayValue[0] == 0)
            return new InfiniteNumber(new string[] { "0" }, a.Base);

        var result = new InfiniteNumber(new string[] { "0" }, a.Base);

        var number1 = new InfiniteNumber(new string[] { "1" }, a.Base);

        for (; !(b.IntArrayValue.Length == 1 && b.IntArrayValue[0] == 0); b = b - number1)
        {
            result = result + a;
        }

        return result;
    }
    #endregion

    int[] intArrayValue;
    string[] stringArrayValue;

    public InfiniteNumber(string[] value, int numberBase)
    {
        this.StringArrayValue = value;
        this.Base = numberBase;
        if (Base > digits.Length)
            throw new Exception("Digits is not too long for this base.Please change Digits");
    }

    public InfiniteNumber(int[] valueInt, int numberBase)
    {
        IntArrayValue = valueInt;
        Base = numberBase;
    }

    public InfiniteNumber ToBase10()
    {
        if (Base == 10)
            return this;

        int length = this.IntArrayValue.Length;

        if (length == 1)
        {
            return new InfiniteNumber(IntArrayValue[0].ToString().Select(c => c.ToString()).ToArray(), 10);
        }
        else
        {
            var firstDigitNumber = new InfiniteNumber(IntArrayValue[IntArrayValue.Length - 1].ToString().Select(c => c.ToString()).ToArray(), 10);
            var restProd1 = new InfiniteNumber(StringArrayValue.Take(IntArrayValue.Length - 1).ToArray(), Base).ToBase10();
            var restNumber = restProd1 * new InfiniteNumber(Base.ToString().Select(c => c.ToString()).ToArray(), 10);
            return firstDigitNumber + restNumber;
        }
    }

    public int[] IntArrayValue
    {
        get
        {
            if (intArrayValue == null)
            {
                this.intArrayValue = StringArrayValue.Select(e => DigitIntRelation[e]).ToArray();
            }

            return intArrayValue;
        }

        protected set
        {
            intArrayValue = value;
        }
    }

    public string[] StringArrayValue
    {
        get
        {
            if (stringArrayValue == null)
            {
                stringArrayValue = IntArrayValue.Select(n => intDigitRelation[n]).ToArray();
            }

            return stringArrayValue;
        }

        protected set
        {
            stringArrayValue = value;
        }
    }

    public int Base { get; }
    public override string ToString()
    {
        string result = StringArrayValue[0];
        for (int i = 1; i < StringArrayValue.Length; i++)
        {
            result += NumberSeparator + StringArrayValue[i];
        }
        return result;
    }
}