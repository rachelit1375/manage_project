﻿

namespace DO;
///There are tasks that depend on the previous task to which the dependency applies this
public record Dependence
(
    int Id,//a run number
    int DependentOnTask,///A previous task  1
    int DependentTask///A task that cannot be performed without the DependentOnTask-previous task 2
)
{
    public Dependence() : this(0, 0, 0) { }
}
