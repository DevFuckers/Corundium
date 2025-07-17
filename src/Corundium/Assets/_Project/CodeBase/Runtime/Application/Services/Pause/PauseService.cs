using System;

public class PauseService : IPauseService
{
    public Action PauseActivated { get; set; }
    public Action PauseDeActivated { get; set; }
}
