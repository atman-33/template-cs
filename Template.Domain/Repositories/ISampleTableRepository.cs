using Template.Domain.Entities;

namespace Template.Domain.Repositories
{
    /// <summary>
    /// ISampleMasterRepository
    /// </summary>
    public interface ISampleTableRepository
    {
        IReadOnlyList<SampleTableEntity> GetData();
        IReadOnlyList<SampleTableEntity> GetData2();

        void Save(SampleTableEntity entity);
        void Save2(SampleTableEntity entity);
        void Delete(SampleTableEntity entity);
    }
}
