using System;

public interface IPauseService
{
    Action PauseActivated { get; set; }
    Action PauseDeActivated { get; set; }
}
