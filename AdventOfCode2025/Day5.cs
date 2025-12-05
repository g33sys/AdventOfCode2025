namespace AdventOfCode2025;

using InputFormat = ((ulong from,ulong to)[] ranges,ulong[] ingredients);

public class Day5:Day<InputFormat>
{
    private InputFormat? _input;

    protected override InputFormat Input
    {
        get
        {
            if (_input == null)
            {
                var lines = Lines.ToList();
                var part1 = 
                    Lines
                        .TakeWhile(s => !string.IsNullOrEmpty(s))
                        .Select(l => l
                            .Split('-')
                            .Select(ulong.Parse)
                            .ToList()
                        )
                        .Select(l=>
                            (l[0],l[1])
                        )
                        .ToArray();
                var part2 =
                    lines
                        .Skip(part1.Length + 1)
                        .Select(ulong.Parse)
                        .ToArray();
                _input = (part1, part2);
            }
            return _input.Value;
        }
    }

    protected override string RunAInternal()
    {
        var result = 
            Input
                .ingredients
                .Count(ingredient => 
                    Input
                        .ranges
                        .Any(r => IsGood(r, ingredient))
                );
        return result.ToString();
    }

    private static bool IsGood((ulong from, ulong to) r, ulong ingredient)
    {
        if (r.from <= ingredient && ingredient <= r.to)
            return true;
        return false;
    }

    protected override string RunBInternal()
    {
        throw new NotImplementedException();
    }
}

