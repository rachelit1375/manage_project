

namespace DO;

public record Task
(

    string? Description,
    ///Task description
    string? Alias,///The name of the task
    bool MileStone,
    DateTime? CreateAt,///Task creation date
    DateTime? Start,///The date of the start of the execution of the task
    DateTime? ScheduledDate,//Estimated date for completion of the task
    DateTime? ForecastDate,///Final date for completion
    DateTime? Deadline,///end date
    string? Complete,///the result of the task
    string? Remarks,///Notes on the task
    int EngineerId,///The ID of the engineer responsible for the task
    EngineerExperience ComplexityLevel//difficulty level
);
