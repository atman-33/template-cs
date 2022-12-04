using Template.Domain.Entities;

namespace Template.Domain.Repositories
{
    public interface ISampleItemTimeRepository
    {
        IReadOnlyList<SampleItemTimeEntity> GetData();
        void Save(SampleItemTimeEntity entity);
    }
}
