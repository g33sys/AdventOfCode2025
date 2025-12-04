using System.Runtime.CompilerServices;
using AdventOfCode2025;
using Newtonsoft.Json;

public abstract class Day
{
    protected string[] Lines { get; private set; }

    protected Day()
    {
        Lines = ReadLines(GetType().Name);
    }

    private static string[] ReadLines(string day)
    {
        var path = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..","input", $"{day}.txt");
        return File.ReadAllLines(path);
    }

    public string RunA()
    {
        (GetType().Name + " Run A").Out();
        return RunAInternal();
    }
    public string RunB()
    {
        "Run B".Out();
        return RunBInternal();
    }

    protected abstract string RunAInternal();
    protected abstract string RunBInternal();
}