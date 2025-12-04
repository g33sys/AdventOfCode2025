namespace AdventOfCode2025;

public class Day2 : Day<(string from, string to)[]>
{
    protected override (string from, string to)[] Input => Lines.Select(l
            =>
        {
            var fromTo = l.Split('-');
            return (fromTo[0], fromTo[1]);
        }
    ).ToArray();


    protected override string RunBInternal()
        => Input.Sum(range =>
            Enumerable
                .Range(2, range.to.Length - 1)
                .Sum(parts => GetDoublesSum(range, parts))
        ).ToString();

    protected override string RunAInternal()
        => Input.Sum(range => GetDoublesSum(range)).ToString();

    private long GetDoublesSum((string from, string to) range, int parts = 2)
    {
        var from = range.from;
        var to = range.to;
        var toUnevenLength = to.Length % parts != 0;
        var fromUnevenLength = from.Length % parts != 0;
        if (toUnevenLength && fromUnevenLength && to.Length == from.Length)
        {
            return 0;
        }
        var fromNum = from.Num();
        var toNum = to.Num();
        if (toUnevenLength)
        {
            var length = (to.Length / parts) * parts;
            toNum = (Math.Pow(10, length) - 1).ToString().Cut(parts).Mult(parts);
            to = toNum.ToString();
        }
        else if (to.Cut(parts).Mult(parts) > toNum)
        {
            toNum = to.Cut(parts).Add(-1).Mult(parts);
            to = toNum.ToString();
        }

        if (fromUnevenLength)
        {
            var length = (from.Length / parts + 1) * parts;
            fromNum = Math.Pow(10, length).ToString().Cut(parts).Mult(parts);
            from = fromNum.ToString();
        }
        else if (from.Cut(parts).Mult(parts) < fromNum)
        {
            fromNum = from.Cut(parts).Add(1).Mult(parts);
            from = fromNum.ToString();
        }

        if (toNum < fromNum)
        {
            return 0;
        }
        if (parts == 4 && to.Length == 8)
        {
            // repeating childs
            return 0;
        }
        var fromHalf = from.Cut(parts);
        var toHalf = to.Cut(parts);
        long sum = 0;
        for (var i = fromHalf.Num(); i <= toHalf.Num(); i++)
        {
            var currSeg = i.ToString();
            var segLength = currSeg.Length;
            if (segLength > 1 && currSeg.All(c => c == currSeg[0]))
            {
                // all the same number -> only to be used for single segs
                continue;
            }
            var candidate = i.ToString().Mult(parts);
            sum += candidate;
        }

        return sum;
    }
}

public static class StringExtensions
{
    public static long Num(this string s) => long.Parse(s);
    public static string Cut(this string s, int factor = 2) => s[0..(s.Length / factor)];
    public static string Add(this string s, int n) => (s.Num() + n).ToString();
    public static long Mult(this string s, int factor = 2) => string.Concat(Enumerable.Repeat(s, factor)).Num();
}