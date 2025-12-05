namespace AdventOfCode2025;

public class Day3 : Day<(int b, int i)[][]>
{
    protected override (int b, int i)[][] Input =>
        Lines
            .Select(l => l
                .Select((b, i) => (b: b - 48, i))
                .ToArray()
            )
            .ToArray();

    protected override string RunAInternal()
    {
        var sum = 0;
        foreach (var line in Input)
        {
            var lineOrdered = line.OrderByDescending(b => b.b).ToList();
            var pos1 = lineOrdered.First(p => p.i < line.Length - 1);
            var pos2 = lineOrdered.First(p => p.i > pos1.i);
            sum += pos1.b * 10 + pos2.b;
        }

        return sum.ToString();
    }

    protected override string RunBInternal()
    {
        
        long sum = 0;
        foreach (var line in Input)
        {
            var filtered = line.ToList();
            IEnumerable<(int b, int i)> kickCandidate;
            do
            {
                kickCandidate =
                    filtered
                        .Take(filtered.Count - 1)
                        .Where((item, i) => item.b < filtered[i + 1].b)
                        .Take(1)
                        .ToList();
                filtered = filtered.Except(kickCandidate).ToList();
            } while (filtered.Count > 12 && kickCandidate.Any());
            var numAsString = string.Join("", filtered.Take(12).Select(p => p.b.ToString()));
            sum += long.Parse(numAsString);
        }

        return sum.ToString();
    }
}