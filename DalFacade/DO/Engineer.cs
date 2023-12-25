
namespace DO;
///The engineer is responsible for all tasks
public record Engineer
(
    int Id,///personal id of engineer
    string Name,///engineer's name
    string Email,///engineer's email
    EngineerExperience? Level,///The difficulty level of the task he is responsible for
    double? Cost,///How much do you get per hour of work?
    bool Active = true
)
{
    public Engineer() : this(0, "", "", null, null, true) { }
}