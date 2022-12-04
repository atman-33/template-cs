using System.Collections;
using System.Collections.ObjectModel;
using Template.Domain.Entities;
using Template.Domain.Repositories;

namespace Template.Domain.StaticValues
{
    public static class StaticSampleTable
    {
        private static ObservableCollection<SampleTableEntity> _entities = new ObservableCollection<SampleTableEntity>();

        /// <summary>
        /// エンティティコレクションを作成
        /// </summary>
        /// <param name="repository"></param>
        public static void Create(ISampleTableRepository repository)
        {
            //// 処理途中に値が変わらないようにロックする
            lock (((ICollection)_entities).SyncRoot)
            {
                _entities.Clear();

                foreach (var entity in repository.GetData())
                {
                    //// ViewModelEntityを生成しながらItemSourceに追加
                    _entities.Add(entity);
                }
            }
        }

        /// <summary>
        /// エンティティコレクションを取得
        /// </summary>
        /// <returns></returns>
        public static ObservableCollection<SampleTableEntity> Get()
        {
            lock (((ICollection)_entities).SyncRoot)
            {
                return _entities;
            }
        }
    }
}
