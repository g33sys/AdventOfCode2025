using System.Runtime.CompilerServices;
using AdventOfCode2025;
using Newtonsoft.Json;

public abstract class Day<TInput>
{
    private string _day;
    private bool _test;
    
    protected abstract TInput Input { get; }

    public Day()
    {
        _day = GetType().Name;
    }

    public Day<TInput> Test()
    {
        _test = true;
        return this;
    }

    protected string[] Lines { get; private set; }

    private void ReadLines(string day)
    {
        var folder = (_test ? "Test" : "") + "Input";
        var path = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", folder, $"{day}.txt");
        Lines = File.ReadAllLines(path);
    }

    public string RunA()
    {
        InitRun();
        return RunAInternal();
    }
    
    public string RunB()
    {
        InitRun();
        return RunBInternal();
    }

    private void InitRun([CallerMemberName] string run = null)
    {
        ($"Day {_day[3..]} RUN {run[^1]}"+(_test?" (TEST MODE)":"")).Out();
        ReadLines(GetType().Name);
    }


    protected abstract string RunAInternal();
    protected abstract string RunBInternal();
}