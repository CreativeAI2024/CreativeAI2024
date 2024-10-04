using System;

public class FlagManager : DontDestroySingleton<FlagManager>
{
    private int progressFlag = 0;
    private int experienceFlag = 0;
    
    public void AddProgressFlag(int number) => AddFlag(ref progressFlag, number);
    public void DeleteProgressFlag(int number) => DeleteFlag(ref progressFlag, number);
    public bool HasProgressFlag(int number) => HasFlag(ref progressFlag, number);
    
    public void AddExperienceFlag(int number) => AddFlag(ref experienceFlag, number);
    public void DeleteExperienceFlag(int number) => DeleteFlag(ref experienceFlag, number);
    public bool HasExperienceFlag(int number) => HasFlag(ref experienceFlag, number);
    
    private void AddFlag(ref int bitFlag, int number)
    {
        bitFlag |= 1 << number;
    }
    
    private void DeleteFlag(ref int bitFlag, int number)
    {
        bitFlag &= ~(1 << number);
    }
    
    private bool HasFlag(ref int bitFlag, int number)
    {
        return Convert.ToBoolean(bitFlag & (1 << number));
    }
}
