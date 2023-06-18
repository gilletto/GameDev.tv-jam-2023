using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level
{
    // cost number of gems
    // number of deaths
    private int _max_gems;
    private int _max_deaths;

    public Level(int delta)
    {
        _max_gems = 1 * delta;
        _max_deaths = 1 * delta;
    }
    public int Deaths { get {return _max_deaths;} }
    public int Gems {get{return _max_gems;}}
    //from LevelManager script instantiate an array of predefined level eg. 10 and foreach call init method


    // method : init, checkCompletion
}
