namespace AdventOfCode2025;

public class Day4 : Day<Place[]>
{
    private Place[]? _input;
    protected override Place[] Input =>
        _input ??= Lines.SelectMany(l => l.Select(b => new Place(b == '@')).ToArray()).ToArray();

    protected override string RunAInternal()
    {
        CollectNeightbours();
        var test = Input.Where(p => p.IsOccupied && p.NeighboursCount < 4);
        return Input.Count(p => p.IsOccupied && p.NeighboursCount < 4).ToString();
    }

    private void CollectNeightbours()
    {
        var width = Lines[0].Length;
        var height = Lines.Length;
        for (var i = 0; i < Input.Length; i++)
        {
            var x = i % width + 1;
            var y = i / width + 1;
            if (x < width)
            {
                Input[i].TalkTo(Input[i + 1]);
            }

            if (y < height)
            {
                Input[i].TalkTo(Input[i + width]);
            }

            if (x < width && y < height)
            {
                Input[i].TalkTo(Input[i + width + 1]);
            }

            if (x > 1 && y < height)
            {
                Input[i].TalkTo(Input[i + width - 1]);
            }
        }
    }

    protected override string RunBInternal()
    {
        var sum = 0;

        List<Place> hits;
        do
        {
            CollectNeightbours();
            hits = Input.Where(p => p.IsOccupied && p.NeighboursCount < 4).ToList();
            sum += hits.Count;
            hits.ForEach(h => h.IsOccupied = false);
            Input.ToList().ForEach(h => h.NeighboursCount = 0);
        } while (hits.Any());
        return sum.ToString();
    }
}

public class Place
{
    public Place(bool isOccupied)
    {
        IsOccupied = isOccupied;
    }

    public bool IsOccupied { get; set; }
    public int NeighboursCount { get; set; }

    public void TalkTo(Place place)
    {
        if (IsOccupied)
        {
            place.NeighboursCount++;
        }

        if (place.IsOccupied)
        {
            NeighboursCount++;
        }
    }
}