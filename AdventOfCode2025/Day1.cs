public class Day1 : Day<int[]>
{
    protected override int[] Input => Lines.Select(l => int.Parse(l[1..]) * (l[0] == 'L' ? -1 : 1)).ToArray();

    protected override string RunAInternal()
    {
        var current = 50;
        var end = 100;
        var hits = 0;
        foreach (var i in Input)
        {
            current = (current + i) % end;
            if (current == 0)
            {
                hits++;
            }
        }

        return hits.ToString();
    }

    protected override string RunBInternal()
    {
        var current = 50;
        var range = 100;
        var hits = 0;
        foreach (var step in Input)
        {
            if (step == 0)
            {
                continue;
            }

            var dist = current == 0 ? 100 : step > 0 ? range - current : current;

            var absStep = Math.Abs(step);
            if (absStep >= dist)
            {
                hits++;
                hits += (absStep - dist) / range;
            }

            current = ((current + step) % range + range) % range;
        }

        return hits.ToString();
    }
}