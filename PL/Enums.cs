using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace PL;

internal class EngineerExperienceCollection : IEnumerable// the enum that connect between the bl and pl
{
    static readonly IEnumerable<BO.EngineerExperience> e_enums = (Enum.GetValues(typeof(BO.EngineerExperience)) as IEnumerable<BO.EngineerExperience>)!;

    public IEnumerator GetEnumerator() => e_enums.GetEnumerator();
}

internal class StatusCollection : IEnumerable// the enum that connect between the bl and pl
{
    static readonly IEnumerable<BO.Status> e_enums = (Enum.GetValues(typeof(BO.Status)) as IEnumerable<BO.Status>)!;

    public IEnumerator GetEnumerator() => e_enums.GetEnumerator();
}

internal class EngineerCollection : IEnumerable// Bring all of the engineer's id as enum
{
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
    static readonly IEnumerable<int> idEngineers = (from engineer in s_bl.Engineer.ReadAll()// create IEnumerable of the engineers id                     
                                                    select engineer.Id);   
    static readonly IEnumerable<int> e_enums = idEngineers;// 

    public IEnumerator GetEnumerator() => e_enums.GetEnumerator();
}

internal class DependenceListCollection : IEnumerable// Bring all of the engineer's id as enum
{
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
    static readonly IEnumerable<string> aliasEngineers = (from task in s_bl.Task.ReadAll()// create IEnumerable of the engineers id                     
                                                    select task.Alias);
    static readonly IEnumerable<string> e_enums = aliasEngineers;// 

    public IEnumerator GetEnumerator() => e_enums.GetEnumerator();
}