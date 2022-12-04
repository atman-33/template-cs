using Template.Domain.Entities;

namespace Template.Domain.Repositories
{
    public interface ISampleMasterRepository
    {
        IReadOnlyList<SampleMasterEntity> GetData();
    }
}
