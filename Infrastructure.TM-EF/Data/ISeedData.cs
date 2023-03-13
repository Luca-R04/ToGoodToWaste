namespace Infrastructure.TGTW_EF.Data;

public interface ISeedData
{
    Task EnsurePopulated(bool dropExisting = false);
}