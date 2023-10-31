

namespace DO;
///There are tasks that depend on the previous task to which the dependency applies this
public record Dependence
(
    int Id,
    int DependentTask,///A task that cannot be performed without the current task
    int DependentOnTask///A task previous to the current task
);
